using Naux.GridSystem;
using UnityEngine;

namespace SlidingSweetness
{
    public class Board
    {
        public GridSystem<Cell> grid;

        public Board(Vector2Int gridSize, float cellSize, GridType gridType, Vector3 origin, bool drawGridLines)
        {
            grid = GridSystem<Cell>.GenerateGrid(gridSize, cellSize, gridType, origin, drawGridLines);
        }
    }
}