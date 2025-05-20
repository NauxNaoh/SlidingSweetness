using N.GridSystem;
using UnityEngine;

namespace SlidingSweetness
{
    public class Board
    {
        public static readonly Vector2Int boardSize = new(8, 10);
        public static readonly float cellSize = 1f;
        public static readonly GridType gridType = GridType.XY_Plane;
        public static Vector3 ScaleFactor = Vector3.one * (cellSize / 1.28f);

        public static GridSystem<Cell> gridBoard;
        public static GridSystem<Cell> gridPreBoard;



        public Board(Vector3 originBoard, Vector3 originPrepare, bool drawGridLines)
        {
            gridBoard = GridSystem<Cell>.GenerateGrid(boardSize, cellSize, 0, gridType, originBoard, drawGridLines);
            gridPreBoard = GridSystem<Cell>.GenerateGrid(new(boardSize.x, 1), 0, cellSize, gridType, originPrepare, drawGridLines);
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

        public static Vector2 GetRangeXCanDrag(Block blockHandling)
        {
            float _min = 0f, _max = 0f;

            bool _findIngredient = false;
            for (int x = blockHandling.GridPlace.x - 1; x >= 0; x--)
            {
                var _cell = gridBoard.GetValue(x, blockHandling.GridPlace.y);
                if (!_cell.HasBlock) continue;

                _min = gridBoard.GetWorldPositionCenter(x + 1, blockHandling.GridPlace.y).x;
                _findIngredient = true;
                break;
            }

            if (!_findIngredient)
                _min = gridBoard.GetWorldPositionCenter(0, blockHandling.GridPlace.y).x;

            _findIngredient = false;
            for (int x = blockHandling.GridPlace.x + 1; x < boardSize.x; x++)
            {
                var _cell = gridBoard.GetValue(x, blockHandling.GridPlace.y);
                if (!_cell.HasBlock || blockHandling == _cell.Block) continue;

                _max = gridBoard.GetWorldPositionCenter(x - blockHandling.Size, blockHandling.GridPlace.y).x;
                _findIngredient = true;
                break;
            }
            if (!_findIngredient)
                _max = gridBoard.GetWorldPositionCenter(boardSize.x - blockHandling.Size, blockHandling.GridPlace.y).x;

            return new Vector2(_min, _max);
        }
    }
}