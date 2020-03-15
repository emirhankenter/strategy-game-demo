using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class PathFinding
    {
        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        public static PathFinding Instance { get; private set; }

        private GridSystem<PathNode> grid;
        private List<PathNode> _openList;    // To be searched list
        private List<PathNode> _closedList;  // Searched list

        public PathFinding(int width, int height, Vector3 pos)
        {
            Instance = this;
            grid = new GridSystem<PathNode>(width, height, 1f, pos, (GridSystem<PathNode> g, int x, int y) => new PathNode(g, x, y));
        }
        public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            grid.GetXY(startWorldPosition, out int startX, out int startY);
            grid.GetXY(endWorldPosition, out int endX, out int endY);

            List<PathNode> path = FindPath(startX, startY, endX, endY);
            if (path == null)
            {
                return null;
            }
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.X, pathNode.Y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);
            _openList = new List<PathNode>{ startNode };  
            _closedList = new List<PathNode>(){  };  
        

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PathNode pathNode = grid.GetGridObject(x, y);
                    pathNode.GCost = 999999;
                    pathNode.CalculateFCost();
                    pathNode.CameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();
            while (_openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(_openList);
                if (currentNode == endNode)
                {
                    // Reached the final node
                    Debug.Log("Reached the goal with fCost: " + endNode.FCost);
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);
                // Add current node to searched list

                foreach (PathNode neighborNode in GetNeighborList(currentNode))
                {
                    if (_closedList.Contains(neighborNode)) {continue;} // Skip previous node if it is already searched
                    if (!neighborNode.IsWalkable) // Check if the node is not walkable than skip it
                    {
                        _closedList.Add(neighborNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighborNode);
                    if (tentativeGCost < neighborNode.GCost)
                    {
                        neighborNode.CameFromNode = currentNode;
                        neighborNode.GCost = tentativeGCost;
                        neighborNode.HCost = CalculateDistanceCost(neighborNode, endNode);
                        neighborNode.CalculateFCost();

                        if (!_openList.Contains(neighborNode))
                        {
                            _openList.Add(neighborNode);
                            // Add neighbor to be searched list
                        }
                    }
                }
            }

            // Out of node on the openList
            Debug.Log("Could not find the path :(");
            return null;
        }

        private List<PathNode> GetNeighborList(PathNode currentNode)
        {
            List<PathNode> neighborList = new List<PathNode>();

            if (currentNode.X - 1 >= 0)
            {
                // Left node
                neighborList.Add(GetNode(currentNode.X - 1, currentNode.Y));
                if (currentNode.Y - 1 >= 0)
                {
                    if (GetNode(currentNode.X - 1, currentNode.Y).IsWalkable || GetNode(currentNode.X, currentNode.Y - 1).IsWalkable) // check 2 objects' intersection
                    {
                        // Left Down node
                        neighborList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
                    }
                
                }
                if (currentNode.Y + 1 < grid.GetHeight())
                {
                    if (GetNode(currentNode.X - 1, currentNode.Y).IsWalkable || GetNode(currentNode.X, currentNode.Y + 1).IsWalkable) // check 2 objects' intersection
                    {
                        // Left Up node
                        neighborList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
                    }
                }
            }

            if (currentNode.X + 1 < grid.GetWidth())
            {
                // Right node
                neighborList.Add(GetNode(currentNode.X + 1, currentNode.Y));
                if (currentNode.Y - 1 >= 0)
                {
                    if (GetNode(currentNode.X + 1, currentNode.Y).IsWalkable || GetNode(currentNode.X, currentNode.Y - 1).IsWalkable) // check 2 objects' intersection
                    {
                        // Right Down node
                        neighborList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
                    }
                }
                if (currentNode.Y + 1 < grid.GetHeight())
                {
                    if (GetNode(currentNode.X + 1, currentNode.Y).IsWalkable || GetNode(currentNode.X, currentNode.Y + 1).IsWalkable) // check 2 objects' intersection
                    {
                        // Right Up node
                        neighborList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
                    }
                
                }
            }
            if (currentNode.Y - 1 >= 0)
            {
                // Down node
                neighborList.Add(GetNode(currentNode.X, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < grid.GetHeight())
            {
                // Up node
                neighborList.Add(GetNode(currentNode.X, currentNode.Y + 1));
            }
            return neighborList;
        }

        public PathNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            path.Add(endNode);
            PathNode currentNode = endNode;
            while (currentNode.CameFromNode != null)
            {
                path.Add(currentNode.CameFromNode);
                currentNode = currentNode.CameFromNode;
            }
            path.Reverse();
            return path;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }

        public GridSystem<PathNode> GetGrid()
        {
            return this.grid;
        }
        public void SetGridWalkableValueObject(Vector3 worldPosition, int _width, int _height)
        {
            grid.GetXY(worldPosition, out int x, out int y);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    grid.GetGridObject(x + i, y + j).ChangeWalkableValue();
                }
            }
        }
        public bool IsGridWalkable(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < grid.GetWidth() && y < grid.GetHeight())
            {
                return GetNode(x,y).IsWalkable;
            }
            return default;
        }
        public bool IsGridWalkable(Vector3 worldPosition)
        {
            grid.GetXY(worldPosition, out var x, out var y);
            return IsGridWalkable(x, y);
        }

        public List<PathNode> GetWalkableNodes()
        {
            List<PathNode> walkableList = new List<PathNode>();
            for (int i = 0; i < grid.GetWidth(); i++)
            {
                for (int j = 0; j < grid.GetHeight(); j++)
                {
                    if (GetNode(i, j).IsWalkable)
                    {
                        walkableList.Add(GetNode(i, j));
                    }
                }
            }
            return walkableList;
        }
    }
}
