using Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class Soldier : ObjectClass, IMoveable
    {
        protected float _speed;
        public Soldier(string name, float speed)
        {
            Name = name;
            Speed = speed;
        }
        public Soldier(string name)
        {
            Name = name;
            Height = 4;
            Width = 4;
        }
        public Soldier()
        {
            Name = "Soldier";
            Height = 4;
            Width = 4;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value > 6f ? 3f: value;
        }

        public Vector3 Position => throw new System.NotImplementedException();

        public void Move(GameObject soldier, Vector3 direction)
        {
            soldier.transform.position = soldier.transform.position + direction * Speed * Time.deltaTime;
        }
        public override string DisplayInfo()
        {
            string infoText = Name + " moves "+ Speed + " per second";
            return infoText;
        }
    }
}

