//using System.Collections.Generic;
//using UnityEngine;

//public class MovementController
//{
//    private GameController _gameController;

//    public MovementController(GameController gameController)
//    {
//        _gameController = gameController;
//    }

//    public void MovementController()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            Vector3 mouseWorldPosition = MouseController.GetMouseWorldPosition();

//            // Check if mouse click position in the map
//            if (Mathf.FloorToInt(mouseWorldPosition.x) < _gameController.gridWidth && Mathf.FloorToInt(mouseWorldPosition.x) >= 0 &&
//                Mathf.FloorToInt(mouseWorldPosition.y) < _gameController.gridHeight && Mathf.FloorToInt(mouseWorldPosition.y) >= 0)
//            {
//                _gameController.mySoldier = SelectionManager.Instance.selectedObject;
//                if (_gameController.mySoldier != null)
//                {
//                    _gameController.soldierController = _gameController.mySoldier.GetComponent<SoldierController>();
//                    _gameController.soldierController.SetTargetPosition(mouseWorldPosition);
//                    _gameController.pathFinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
//                    //pathFinding.GetNode(x, y).isWalkable = true;
//                    List<PathNode> path = _gameController.pathFinding.FindPath(Mathf.FloorToInt(_gameController.currentPos.x),
//                        Mathf.FloorToInt(_gameController.currentPos.y), x, y);
//                    if (path != null)
//                    {
//                        for (int i = 0; i < path.Count - 1; i++)
//                        {
//                            Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + Vector3.one * .5f,
//                                new Vector3(path[i + 1].x, path[i + 1].y) * 1f + Vector3.one * .5f, Color.green, 1f);
//                        }
//                    }

//                    //path.Last().ChangeWalkableValue();
//                    _gameController.soldierController.SetTargetPosition(mouseWorldPosition);
//                    //if (soldierController.isMovementCompleted)
//                    //{
//                    //    pathFinding.SetGridWalkableValueObject(mouseWorldPosition,1,1);
//                    //}
//                    _gameController.currentPos = new Vector3(x, y, 0);
//                    //Debug.Log("Buildable Area: "+CheckBuildableArea(4,4));
//                    //Debug.Log(CheckBuildableArea());
//                }
//            }
//        }
//    }
//}