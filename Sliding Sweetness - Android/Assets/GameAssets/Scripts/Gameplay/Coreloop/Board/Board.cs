using Naux.GridSystem;
using UnityEngine;

namespace SlidingSweetness
{
    public class Board
    {
        public static readonly Vector2Int boardSize = new(8, 10);
        public static readonly float cellSize = 1f;
        public static readonly GridType gridType = GridType.XY_Plane;

        public GridSystem<Cell> gridBoard;
        public GridSystem<Cell> gridPreBoard;

        public Board(Vector3 originBoard, Vector3 originPrepare, bool drawGridLines)
        {
            gridBoard = GridSystem<Cell>.GenerateGrid(boardSize, cellSize, gridType, originBoard, drawGridLines);
            gridPreBoard = GridSystem<Cell>.GenerateGrid(new(boardSize.x, 1), cellSize, gridType, originPrepare, drawGridLines);
        }

        public void ClearBoard()
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                for (int y = 0; y < boardSize.y; y++)
                {
                    gridBoard.GetValue(x, y).SetBlockInCell(null);
                }
            }
        }

        public void ClearPreBoard()
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                gridPreBoard.GetValue(x, 0).SetBlockInCell(null);
            }
        }


        public void SetBlockToCell(Block block, Vector2Int grid, int size)
        {
            for (int i = 0; i < size; i++)
            {
                gridBoard.GetValue(grid.x + i, grid.y).SetBlockInCell(block);
            }
        }

        public bool IsBlockOccupiedBellow(Vector2Int grid, int size)
        {
            if (grid.y == 0) return true;
            for (int i = 0; i < size; i++)
            {
                if (gridBoard.GetValue(grid.x + i, grid.y - 1).HasBlock)
                    return true;
            }
            return false;
        }
    }
}