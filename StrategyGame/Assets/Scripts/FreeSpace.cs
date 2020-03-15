using UnityEngine;

namespace Scripts
{
    public class FreeSpace : MonoBehaviour
    {
        private int _width;
        private int _height;
        private int _freeSpace;

        private void Update()
        {
            _width = PathFinding.Instance.GetGrid().GetWidth();
            _height = PathFinding.Instance.GetGrid().GetHeight();
            _freeSpace = _width * _height;
            CalculateFreeSpace();
        }

        private void CalculateFreeSpace()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (!PathFinding.Instance.GetNode(i, j).IsWalkable)
                    {
                        _freeSpace--;
                    }
                }
            }

            if (_freeSpace == 0)
            {
                ErrorEvents.Current.OutOfSpaceTriggerEnter();
            }
        }
    }
}
