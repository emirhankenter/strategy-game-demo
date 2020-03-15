using System;
using UnityEngine;

namespace Scripts
{
    public class GridSystem<TGridObject>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        private int width;
        private int height;
        private float cellSize;
        private TGridObject[,] gridArray;
        private Vector3 originPosition;
        private TextMesh[,] debugTextArray;
        bool showDebug = true;

        public GridSystem(int width, int height, float cellSize, Vector3 originPosition, Func<GridSystem<TGridObject>,int, int, TGridObject> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new TGridObject[width,height];
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            if (showDebug)
            {
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridArray.GetLength(1); y++)
                    {
                        //if (false)
                        //{
                        //    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 6,
                        //        Color.white, TextAnchor.MiddleCenter);
                        //}
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
            }
        }
        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }
        public float GetCellSize()
        {
            return cellSize;
        }
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x,y) * cellSize + originPosition;
        }
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
            //Debug.Log("Grid position: " + x +", " + y);
        }
        public void SetGridObject(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                if (showDebug)
                {
                    if (OnGridValueChanged != null)
                    {
                        OnGridValueChanged(this, new OnGridValueChangedEventArgs{x = x, y= y});
                    }
                }
            }
        }
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            GetXY(worldPosition, out int x,out int y);
            SetGridObject(x, y, value);
        }
        public void SetGridObject(Vector3 worldPosition, TGridObject value, int _width, int _height)
        {
            GetXY(worldPosition, out int x, out int y);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    SetGridObject(x+i, y+j, value);
                }
            }
        }
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            return default(TGridObject);
        }
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            GetXY(worldPosition,out var x,out var y);
            return GetGridObject(x, y);
        }
    }

    //public bool CheckBuildableArea(float width, float height, Vector3 mousePos)
    //{
    //for (int i = 0; i < width; i++)
    //{
    //    for (int j = 0; j < height; j++)
    //    {
    //        if (grid.GetGridObject(mousePos + new Vector3(i, j)))
    //        {
    //            return false;
    //        }
    //    }
    //}
    //return true;
    //}
}
