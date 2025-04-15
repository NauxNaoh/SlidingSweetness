using System;
using UnityEngine;

namespace Naux.GridSystem
{
    /// <summary>
    /// Enum representing the type of the grid (XY or XZ plane).
    /// </summary>
    public enum GridType { XY_Plane, XZ_Plane, }
    public class GridSystem<T>
    {
        private readonly Vector2Int gridSize;
        private readonly float cellSize;
        private readonly T[,] gridArray;
        private readonly Vector3 origin;
        private readonly CoordinatesConverter coordinatesConverter;


        /// <summary>
        /// Generates a grid system based on the specified grid size, cell size, grid type, and origin position.
        /// </summary>
        /// <param name="gridSize">Size of the grid.</param>
        /// <param name="cellSize">Size of each cell in the grid.</param>
        /// <param name="gridType">Type of the grid (XY_Plane or XZ_Plane).</param>
        /// <param name="origin">The origin position in the world.</param>
        /// <param name="drawGridLines">Whether to draw the grid lines for visualization.</param>
        /// <returns>A new instance of the GridSystem.</returns>
        internal static GridSystem<T> GenerateGrid(Vector2Int gridSize, float cellSize, GridType gridType, Vector3 origin, bool drawGridLines = false)
        {
            origin = CalculateOrigin(gridSize, cellSize, gridType, origin);

            return gridType switch
            {
                GridType.XY_Plane => new GridSystem<T>(gridSize, cellSize, origin, new XYConverter(), drawGridLines),
                GridType.XZ_Plane => new GridSystem<T>(gridSize, cellSize, origin, new XZConverter(), drawGridLines),
                _ => throw new ArgumentException("Invalid GridType provided"),
            };
        }

        static Vector3 CalculateOrigin(Vector2Int gridSize, float cellSize, GridType gridType, Vector3 origin)
        {
            return gridType switch
            {
                GridType.XY_Plane => new Vector3(origin.x - (gridSize.x * cellSize / 2), origin.y - (gridSize.y * cellSize / 2), origin.z),
                GridType.XZ_Plane => new Vector3(origin.x - (gridSize.x * cellSize / 2), origin.y, origin.z - (gridSize.y * cellSize / 2)),
                _ => throw new ArgumentException("Invalid GridType provided"),
            };
        }

        GridSystem(Vector2Int gridSize, float cellSize, Vector3 origin, CoordinatesConverter converter, bool drawGridLines)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.origin = origin;
            this.coordinatesConverter = converter ?? new XYConverter();
            this.gridArray = new T[gridSize.x, gridSize.y];

            if (drawGridLines)
                DrawGridLines();
        }


        void DrawGridLines()
        {
            var _duration = 120f;
            var _color = Color.red;

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    var corner1 = GetWorldPositionCorner(x, y);
                    var corner2 = GetWorldPositionCorner(x, y + 1);
                    var corner3 = GetWorldPositionCorner(x + 1, y);

                    Debug.DrawLine(corner1, corner2, _color, _duration);
                    Debug.DrawLine(corner1, corner3, _color, _duration);
                }
            }

            Debug.DrawLine(GetWorldPositionCorner(0, gridSize.y), GetWorldPositionCorner(gridSize.x, gridSize.y), _color, _duration);
            Debug.DrawLine(GetWorldPositionCorner(gridSize.x, 0), GetWorldPositionCorner(gridSize.x, gridSize.y), _color, _duration);
        }

        Vector3 GetWorldPositionCorner(int x, int y) => coordinatesConverter.GetGridCornerPosition(x, y, cellSize, origin);
        internal Vector3 GetWorldPositionCenter(Vector2Int grid) => GetWorldPositionCenter(grid.x, grid.y);
        internal Vector3 GetWorldPositionCenter(int x, int y) => coordinatesConverter.GetGridCenterPosition(x, y, cellSize, origin);
        internal Vector2Int GetXYCoordinates(Vector3 worldPosition) => coordinatesConverter.WorldPositionToGridCoordinates(worldPosition, cellSize, origin);


        /// <summary>
        /// Retrieves the value stored in the grid at the specified grid coordinates.
        /// </summary>
        internal T GetValue(Vector3 worldPosition) => GetValue(GetXYCoordinates(worldPosition));
        internal T GetValue(Vector2Int grid) => GetValue(grid.x, grid.y);
        internal T GetValue(int x, int y) => IsValid(x, y) ? gridArray[x, y] : default;


        /// <summary>
        /// Sets the value in the grid at the specified grid coordinates.
        /// </summary>
        internal void SetValue(Vector2Int grid, T value) => SetValue(grid.x, grid.y, value);
        internal void SetValue(int x, int y, T value)
        {
            if (!IsValid(x, y)) return;
            gridArray[x, y] = value;
        }


        /// <summary>
        /// Validates whether the specified grid coordinates are within the bounds of the grid.
        /// </summary>
        bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y;



        /// <summary>
        /// A coordinate converter base class for converting between world position and grid coordinates.
        /// </summary>
        internal abstract class CoordinatesConverter
        {
            internal abstract Vector3 GetGridCornerPosition(int x, int y, float cellSize, Vector3 origin);
            internal abstract Vector3 GetGridCenterPosition(int x, int y, float cellSize, Vector3 origin);
            internal abstract Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, Vector3 origin);
        }

        /// <summary>
        /// A coordinate converter for 2D grids, where the grid lies on the X-Y plane.
        /// </summary>
        internal class XYConverter : CoordinatesConverter
        {
            internal override Vector3 GetGridCornerPosition(int x, int y, float cellSize, Vector3 origin)
            {
                return (new Vector3(x, y, 0) * cellSize) + origin;
            }

            internal override Vector3 GetGridCenterPosition(int x, int y, float cellSize, Vector3 origin)
            {
                return new Vector3((x * cellSize) + (cellSize * 0.5f), (y * cellSize) + (cellSize * 0.5f), 0) + origin;
            }

            internal override Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, Vector3 origin)
            {
                Vector3 gridPosition = (worldPosition - origin) / cellSize;
                int x = Mathf.FloorToInt(gridPosition.x);
                int y = Mathf.FloorToInt(gridPosition.y);
                return new Vector2Int(x, y);
            }
        }

        /// <summary>
        /// A coordinate converter for 3D grids, where the grid lies on the X-Z plane.
        /// </summary>
        internal class XZConverter : CoordinatesConverter
        {
            internal override Vector3 GetGridCornerPosition(int x, int y, float cellSize, Vector3 origin)
            {
                return (new Vector3(x, 0, y) * cellSize) + origin;
            }

            internal override Vector3 GetGridCenterPosition(int x, int y, float cellSize, Vector3 origin)
            {
                return new Vector3((x * cellSize) + (cellSize * 0.5f), 0, (y * cellSize) + (cellSize * 0.5f)) + origin;
            }

            internal override Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, Vector3 origin)
            {
                Vector3 gridPosition = (worldPosition - origin) / cellSize;
                var x = Mathf.FloorToInt(gridPosition.x);
                var y = Mathf.FloorToInt(gridPosition.z);
                return new Vector2Int(x, y);
            }
        }
    }
}