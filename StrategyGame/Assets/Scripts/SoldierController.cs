using System.Collections.Generic;
using Assets.Scripts;
using Scripts;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] private const float MovementSpeed = 6f;
    private int _currentPathIndex;
    private List<Vector3> _pathVectorList;
    [HideInInspector] public bool IsMovementCompleted = true;
    void Start()
    {
        //soldier = new Soldier("My Soldier", 1,1,MovementSpeed);

    }

    private void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        if (_pathVectorList != null)
        {
            PathFinding.Instance.GetGrid().GetXY(transform.position, out int x, out int y);
            if (IsMovementCompleted)
            {
                
                IsMovementCompleted = false;
            }
            
            Vector3 targetPosition = _pathVectorList[_currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > .05f)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                
                //soldier.Move(moveDirection);
                transform.position = transform.position + moveDirection * MovementSpeed * Time.deltaTime;
            }
            else
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _pathVectorList.Count)
                {
                    StopMoving();
                    //soldier.Move(Vector3.zero);
                    IsMovementCompleted = true;
                    PathFinding.Instance.GetNode(x, y).IsWalkable = false;
                }
            }
        }
    }

    public void StartMoving()
    {

    }

    public void StopMoving()
    {
        _pathVectorList = null;
        
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _currentPathIndex = 0;
        _pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);

        if (_pathVectorList != null && _pathVectorList.Count > 1)
        {
            _pathVectorList.RemoveAt(0);
        }
    }

}
