using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SlidingSweetness
{
    public class BoardController : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Cell cellPrefab;


        [Header("References")]
        [SerializeField] private BoardState boardState;
        [SerializeField] private Transform placePrepare;


        private Board board;
        private Transform tfmCells;
        private Transform tfmBlocks;

        public BoardState BoardState => boardState;



        private void Awake()
        {
            InitializeNew().Forget();
        }

        async UniTask InitializeNew()
        {
            SetBoardState(BoardState.Handling);
            await GenBoard();
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
                    var _cell = Instantiate(cellPrefab, tfmCells);
                    var _workPos = board.gridBoard.GetWorldPositionCenter(x, y);
                    _cell.transform.SetPositionAndRotation(_workPos, Quaternion.identity);
                    board.gridBoard.SetValue(x, y, _cell);
                }
            }

            await UniTask.Yield();
        }


    }
}