using UnityEngine;

namespace Scripts
{
    public class Barrack : Construction, IProductive
    {
        public Barrack(string name, int height, int width)
        {
            Name = name;
            Height = height;
            Width = width;
        }
        public Barrack(string name)
        {
            Name = name;
            Height = 4;
            Width = 4;
        }
        public Barrack()
        {
            Name = "Barrack";
            Height = 4;
            Width = 4;
        }
        public override string DisplayInfo()
        {
            string infoText = Name + " can spawn soldier";
            return infoText;
        }

        public void SpawnSoldier()
        {
            Debug.Log("Soldier has spawned");
        }
    }
}

