using System.Threading;
using Cysharp.Threading.Tasks;
using N.Patterns;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    public class BoardController : Singleton<BoardController>
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

        public BoardState BoardState => boardState;



        private CancellationTokenSource boardCTS;
        void InitCancelToken()
        {
            boardCTS?.Cancel();
            boardCTS = new CancellationTokenSource();
        }

        public async UniTask InitializeNew(CancellationToken token)
        {
            SetBoardState(BoardState.Handling);
            await GenBoard(token);
            await GenerateStartGame(token);
        }

        void SetBoardState(BoardState state) => boardState = state;

        async UniTask GenBoard(CancellationToken token)
        {
            tfmCells = new GameObject("Cells").GetComponent<Transform>();
            tfmCells.SetParent(this.transform);
            tfmBlocks = new GameObject("Blocks").GetComponent<Transform>();
            tfmBlocks.SetParent(this.transform);
            board = new Board(transform.position, placePrepare.position, true);
            for (int y = 0; y < Board.boardSize.y; y++)
            {
                for (int x = 0; x < Board.boardSize.x; x++)
                {
                    var _workPos = Board.gridBoard.GetWorldPositionCenter(x, y);
                    var _cell = CreateCell($"Cell[{x},{y}]");
                    _cell.transform.SetPositionAndRotation(_workPos, Quaternion.identity);
                    Board.gridBoard.SetValue(x, y, _cell);
                }
            }

            await UniTask.Yield(token);
        }


        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        GenerateStartGame().Forget();
        //    }
        //}

        async UniTask GenerateStartGame(CancellationToken token)
        {
            for (int i = 0; i < tfmBlocks.childCount; i++)
            {
                Destroy(tfmBlocks.GetChild(i).gameObject);
            }
            //board.ClearPreBoard();
            board.ClearBoard();

            for (int y = 0; y < 3; y++)
            {
                bool _wasGen = false;
                var _spawns = LevelCalculator.GenBlockCalculator(SOContainer.DifficultLevel);
                for (int i = 0; i < _spawns.Count; i++)
                {
                    _spawns[i].GridPos.y = y;
                    if (!board.IsBlockOccupiedBellow(_spawns[i].GridPos, _spawns[i].SizeInt)) continue;

                    var _workPos = Board.gridBoard.GetWorldPositionCenter(_spawns[i].GridPos);
                    var _block = CreateBlock($"{_spawns[i].BlockType}-{_spawns[i].BlockSizeType}");
                    _block.Initialize(_spawns[i]);
                    _block.transform.SetPositionAndRotation(_workPos, Quaternion.identity);
                    board.SetBlockToCell(_block, _spawns[i].GridPos, _spawns[i].SizeInt);

                    _wasGen = true;
                }

                if (!_wasGen) y--;
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