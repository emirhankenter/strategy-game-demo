using UnityEngine;

namespace Scripts
{
    public class BuildableArea
    {
        public static BuildableArea Instance { get; set; }

        public BuildableArea()
        {
            Instance = this;
        }
        public bool CheckBuildableArea(float width, float height, Vector3 mousePos)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!PathFinding.Instance.IsGridWalkable(mousePos + new Vector3(i, j)))
                    {
                        return false;
                    }

                    //if (grid.GetGridObject(mousePos + new Vector3(i, j)))
                    //{
                    //    return false;
                    //}
                }
            }
            return true;
        }
    }
}
