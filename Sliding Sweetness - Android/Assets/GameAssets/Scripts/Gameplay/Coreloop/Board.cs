using System;
using Naux.GridSystem;
using UnityEngine;

namespace SlidingSweetness
{
    public class Board
    {
        public readonly Vector2Int boardSize = new(8, 10);
        public readonly float cellSize = 1f;
        public readonly GridType gridType = GridType.XY_Plane;

        public GridSystem<Cell> gridBoard;
        public GridSystem<Cell> gridPrepare;

        public Board(Vector3 originBoard, Vector3 originPrepare, bool drawGridLines)
        {
            gridBoard = GridSystem<Cell>.GenerateGrid(boardSize, cellSize, gridType, originBoard, drawGridLines);
            gridPrepare = GridSystem<Cell>.GenerateGrid(new(boardSize.x, 1), cellSize, gridType, originPrepare, drawGridLines);
        }
    }
}