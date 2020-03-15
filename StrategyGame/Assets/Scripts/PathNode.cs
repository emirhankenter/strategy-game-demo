namespace Scripts
{
    public class PathNode
    {
        private GridSystem<PathNode> _grid;
        public int X;
        public int Y;

        public int GCost;
        public int HCost;
        public int FCost;

        public bool IsWalkable;
        public PathNode CameFromNode;

        public PathNode(GridSystem<PathNode> grid, int x, int y)
        {
            this._grid = grid;
            this.X = x;
            this.Y = y;
            IsWalkable = true;
        }

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }
        public override string ToString()
        {
            return X + "," + Y;
        }

        public void ChangeWalkableValue()
        {
            IsWalkable = !IsWalkable;
        }
    }
}
