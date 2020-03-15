using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameController : MonoBehaviour
    {
        public int GridWidth = 16;
        public int GridHeight = 16;
        public PathFinding PathFinding;
        private GameObject _mySoldier;
        private SoldierController _soldierController;
        private Vector3 _currentPos;

        public static GameController Instance { get; private set; }
        public GameController()
        {
            Instance = this;
        }
        private void Start()
        {
            _currentPos = transform.position;
            PathFinding = new PathFinding(GridWidth, GridHeight, _currentPos);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                MovementController();
            }
        
        }

        private void MovementController()
        {
            Vector3 mouseWorldPosition = MouseController.GetMouseWorldPosition();
            if (Mathf.FloorToInt(mouseWorldPosition.x) < GridWidth && Mathf.FloorToInt(mouseWorldPosition.x) >= 0 &&
                Mathf.FloorToInt(mouseWorldPosition.y) < GridHeight && Mathf.FloorToInt(mouseWorldPosition.y) >= 0)
            {
                _mySoldier = SelectionManager.Instance.SelectedObject;
                var moveable = _mySoldier.GetComponent<IMoveable>();
                if (_mySoldier != null && moveable != null)
                {
                    _soldierController = _mySoldier.GetComponent<SoldierController>();
                    _soldierController.SetTargetPosition(mouseWorldPosition);
                    PathFinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                    List<PathNode> path = PathFinding.FindPath(Mathf.FloorToInt(_currentPos.x),
                        Mathf.FloorToInt(_currentPos.y), x, y);
                    if (path != null)
                    {
                        for (int i = 0; i < path.Count - 1; i++)
                        {
                            Debug.DrawLine(new Vector3(path[i].X, path[i].Y) * 1f + Vector3.one * .5f,
                                new Vector3(path[i + 1].X, path[i + 1].Y) * 1f + Vector3.one * .5f, Color.green, 1f);
                        }
                    }

                    PathFinding.Instance.GetGrid().GetXY(_mySoldier.transform.position, out x, out y);
                    PathFinding.Instance.GetNode(x, y).IsWalkable = true;
                    _soldierController.SetTargetPosition(mouseWorldPosition);
                    _currentPos = new Vector3(x, y, 0);
                }
            }
        }
    }
}
