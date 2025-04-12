using Naux.GridSystem;
using UnityEngine;

namespace SlidingSweetness
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private Cell cellPrefab;


        Vector2Int boardSize = new(8, 10);

        public Board board;



        private void Awake()
        {
            board = new Board(boardSize, 1f, GridType.XY_Plane, this.transform.position, true);

            for (int x = 0; x < boardSize.x; x++)
            {
                for (int y = 0; y < boardSize.y; y++)
                {
                    var _cell = Instantiate(cellPrefab, this.transform);
                    var _workPos = board.grid.GetWorldPositionCenter(x, y);
                    _cell.transform.SetPositionAndRotation(_workPos, Quaternion.identity);
                    board.grid.SetValue(x, y, _cell);
                }
            }
        }
    }
}