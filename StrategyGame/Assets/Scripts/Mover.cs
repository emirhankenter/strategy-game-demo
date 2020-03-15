using UnityEngine;

namespace Scripts
{
    public class Mover
    {
        public static void Move(GameObject target, Vector3 direction)
        {
            var moveable = target.GetComponent<IMoveable>();

            if (moveable == null) { return; }

            moveable.Move(target, direction);

            Debug.Log(moveable.Position);
        }
    }
}