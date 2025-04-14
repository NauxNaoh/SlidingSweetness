using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    public class BoardController : MonoBehaviour
    {
        [Header("Prefabs")]
        [Required]
        [SerializeField] private Cell cellPrefab;
        [Required]
        [SerializeField] private Block blockPrefab;


        [Header("References")]
        [SerializeField] private BoardState boardState;
        [SerializeField] private Transform placePrepare;


        private Board board;
        private Transform tfmCells;
        private Transform tfmBlocks;
        [Required]
        public DifficultLevelSetting DifficultLevelSetting;
        [Required]
        public BlockSkinSetting BlockSkinSetting;
        public BoardState BoardState => boardState;



        private CancellationTokenSource boardCTS;
        void InitCancelToken()
        {
            boardCTS?.Cancel();
            boardCTS = new CancellationTokenSource();
        }


        private void Awake()
        {
            InitializeNew().Forget();
        }

        async UniTask InitializeNew()
        {
            SetBoardState(BoardState.Handling);
            await GenBoard();
            await GenerateStartGame();
        }

        void SetBoardState(BoardState state) => boardState = state;

        async UniTask GenBoard()
        {
            tfmCells = new GameObject("Cells").GetComponent<Transform>();
            tfmCells.SetParent(this.transform);
            tfmBlocks = new GameObject("Blocks").GetComponent<Transform>();
            tfmBlocks.SetParent(this.transform);

            board = new Board(transform.position, placePrepare.position, true);
            for (int x = 0; x < board.boardSize.x; x++)
            {
                for (int y = 0; y < board.boardSize.y; y++)
                {
                    var _workPos = board.gridBoard.GetWorldPositionCenter(x, y);
                    var _cell = CreateCell($"Cell[{x},{y}]");
                    _cell.transform.SetPositionAndRotation(_workPos, Quaternion.identity);
                    board.gridBoard.SetValue(x, y, _cell);
                }
            }

            await UniTask.Yield();
        }

        [ContextMenu(nameof(GenerateStartGame))]
        async UniTask GenerateStartGame()
        {
            for (int i = 0; i < tfmBlocks.childCount; i++)
            {
                Destroy(tfmBlocks.GetChild(i).gameObject);
            }




            var _spawns = LevelCalculator.TestGene(DifficultLevelSetting);

            var _startPos = 0;

            for (int i = 0; i < _spawns.Count; i++)
            {
                var _workPos = board.gridBoard.GetWorldPositionCenter(_startPos, 0);
                var _block = CreateBlock($"{_spawns[i].BlockType}-{_spawns[i].BlockSizeType}");
                _block.transform.SetPositionAndRotation(_workPos, Quaternion.identity);

                _block.SetSpriteBlock(BlockSkinSetting.GetBlockSprite(_spawns[i].BlockType, _spawns[i].BlockSizeType));
                _block.SetLocalDeviation(_spawns[i].SizeInt, board.cellSize);
                //add to cell;
                Debug.Log($"{_spawns[i].BlockType}-{_spawns[i].BlockSizeType}");
                _startPos += _spawns[i].SizeInt;
            }

            await UniTask.Yield();

        }


        Cell CreateCell(string name)
        {
            var _result = Instantiate(cellPrefab, tfmCells);
            _result.name = name;
            return _result;
        }
        Block CreateBlock(string name)
        {
            var _result = Instantiate(blockPrefab, tfmBlocks);
            _result.name = name;
            return _result;
        }

    }
}