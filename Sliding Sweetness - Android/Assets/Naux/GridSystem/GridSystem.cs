using System;
using UnityEngine;

namespace N.GridSystem
{
    /// <summary>
    /// Enum representing the type of the grid (XY or XZ plane).
    /// </summary>
    public enum GridType { XY_Plane, XZ_Plane, }
    public class GridSystem<T>
    {
        private readonly Vector2Int gridSize;
        private readonly float cellSize;
        private readonly float spacing;
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
        internal static GridSystem<T> GenerateGrid(Vector2Int gridSize, float cellSize, float spacing, GridType gridType, Vector3 origin, bool drawGridLines = false)
        {
            origin = CalculateOrigin(gridSize, cellSize, spacing, gridType, origin);

            var _converter = gridType switch
            {
                GridType.XY_Plane => new XYConverter() as CoordinatesConverter,
                GridType.XZ_Plane => new XZConverter() as CoordinatesConverter,
                _ => throw new ArgumentException("Invalid GridType provided"),
            };

            var _gridSystem = new GridSystem<T>(gridSize, cellSize, spacing, origin, _converter);

            if (drawGridLines)
                _gridSystem.DrawGridLines();

            return _gridSystem;
        }

        private static Vector3 CalculateOrigin(Vector2Int gridSize, float cellSize, float spacing, GridType gridType, Vector3 origin)
        {
            var _offset = cellSize + spacing;
            return gridType switch
            {
                GridType.XY_Plane => new Vector3(origin.x - (gridSize.x * _offset * 0.5f), origin.y - (gridSize.y * _offset * 0.5f), origin.z),
                GridType.XZ_Plane => new Vector3(origin.x - (gridSize.x * _offset * 0.5f), origin.y, origin.z - (gridSize.y * _offset * 0.5f)),
                _ => throw new ArgumentException("Invalid GridType provided"),
            };
        }

        private GridSystem(Vector2Int gridSize, float cellSize, float spacing, Vector3 origin, CoordinatesConverter converter)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.origin = origin;
            this.spacing = spacing;
            this.coordinatesConverter = converter;
            this.gridArray = new T[gridSize.x, gridSize.y];
        }

        private void DrawGridLines()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    this.coordinatesConverter.DrawGridCornerPosition(x, y, cellSize, spacing, origin, Color.red, 120f);
                }
            }
        }

        internal Vector3 GetWorldPositionCenter(Vector2Int grid) => GetWorldPositionCenter(grid.x, grid.y);
        internal Vector3 GetWorldPositionCenter(int x, int y) => coordinatesConverter.GetGridCenterPosition(x, y, cellSize, spacing, origin);
        internal Vector2Int GetXYCoordinates(Vector3 worldPosition) => coordinatesConverter.WorldPositionToGridCoordinates(worldPosition, cellSize, spacing, origin);

        /// <summary>
        /// Retrieves the value stored in the grid at the specified grid coordinates.
        /// </summary>
        internal T GetValue(Vector3 worldPosition) => GetValue(GetXYCoordinates(worldPosition));
        internal T GetValue(Vector2Int grid) => GetValue(grid.x, grid.y);
        internal T GetValue(int x, int y) => IsValid(x, y) ? gridArray[x, y] : default;

        /// <summary>
        /// Sets the value in the grid at the specified grid coordinates.
        /// </summary>
        /// internal void SetValue(Vector2Int grid, T value) => SetValue(grid.x, grid.y, value);
        internal void SetValue(Vector2Int grid, T value) => SetValue(grid.x, grid.y, value);
        internal void SetValue(int x, int y, T value)
        {
            if (IsValid(x, y)) gridArray[x, y] = value;
        }

        /// <summary>
        /// Validates whether the specified grid coordinates are within the bounds of the grid.
        /// </summary>
        private bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y;

        /// <summary>
        /// A coordinate converter base class for converting between world position and grid coordinates.
        /// </summary>
        internal abstract class CoordinatesConverter
        {
            internal abstract void DrawGridCornerPosition(int x, int y, float cellSize, float spacing, Vector3 origin, Color red, float duration);
            protected abstract Vector3[] GetCorners(int x, int y, float cellSize, float spacing, Vector3 origin);
            internal abstract Vector3 GetGridCenterPosition(int x, int y, float cellSize, float spacing, Vector3 origin);
            internal abstract Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, float spacing, Vector3 origin);
        }

        /// <summary>
        /// A coordinate converter for 2D grids, where the grid lies on the X-Y plane.
        /// </summary>
        internal class XYConverter : CoordinatesConverter
        {
            internal override void DrawGridCornerPosition(int x, int y, float cellSize, float spacing, Vector3 origin, Color color, float duration)
            {
                var corners = GetCorners(x, y, cellSize, spacing, origin);
                for (int i = 0; i < corners.Length; i++)
                {
                    Debug.DrawLine(corners[i], corners[(i + 1) % corners.Length], color, duration);
                }
            }

            protected override Vector3[] GetCorners(int x, int y, float cellSize, float spacing, Vector3 origin)
            {
                var center = GetGridCenterPosition(x, y, cellSize, spacing, origin);
                return new[]
                {
                    center + new Vector3(-0.5f, -0.5f) * cellSize,
                    center + new Vector3(-0.5f, 0.5f) * cellSize,
                    center + new Vector3(0.5f, 0.5f) * cellSize,
                    center + new Vector3(0.5f, -0.5f) * cellSize
                };
            }

            internal override Vector3 GetGridCenterPosition(int x, int y, float cellSize, float spacing, Vector3 origin)
            {
                var offset = cellSize + spacing;
                return new Vector3(x * offset + offset * 0.5f, y * offset + offset * 0.5f, 0) + origin;
            }

            internal override Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, float spacing, Vector3 origin)
            {
                var gridPosition = (worldPosition - origin) / (cellSize + spacing);
                return new Vector2Int(Mathf.FloorToInt(gridPosition.x), Mathf.FloorToInt(gridPosition.y));
            }
        }

        /// <summary>
        /// A coordinate converter for 3D grids, where the grid lies on the X-Z plane.
        /// </summary>
        internal class XZConverter : CoordinatesConverter
        {
            internal override void DrawGridCornerPosition(int x, int y, float cellSize, float spacing, Vector3 origin, Color color, float duration)
            {
                var corners = GetCorners(x, y, cellSize, spacing, origin);
                for (int i = 0; i < corners.Length; i++)
                {
                    Debug.DrawLine(corners[i], corners[(i + 1) % corners.Length], color, duration);
                }
            }

            protected override Vector3[] GetCorners(int x, int y, float cellSize, float spacing, Vector3 origin)
            {
                var center = GetGridCenterPosition(x, y, cellSize, spacing, origin);
                return new[]
                {
                    center + new Vector3(-0.5f, 0) * cellSize,
                    center + new Vector3(-0.5f, 0) * cellSize,
                    center + new Vector3(0.5f, 0) * cellSize,
                    center + new Vector3(0.5f, 0) * cellSize
                };
            }
            internal override Vector3 GetGridCenterPosition(int x, int y, float cellSize, float spacing, Vector3 origin)
            {
                var offset = cellSize + spacing;
                return new Vector3(x * offset + offset * 0.5f, 0, y * offset + offset * 0.5f) + origin;
            }

            internal override Vector2Int WorldPositionToGridCoordinates(Vector3 worldPosition, float cellSize, float spacing, Vector3 origin)
            {
                var gridPosition = (worldPosition - origin) / (cellSize + spacing);
                return new Vector2Int(Mathf.FloorToInt(gridPosition.x), Mathf.FloorToInt(gridPosition.z));
            }
        }
    }
}