using UnityEngine;
namespace Scripts
{
    public interface IMoveable
    {
        float Speed { get; set; }
        Vector3 Position { get; }
        void Move(GameObject moveableObject, Vector3 direction);
    }
}