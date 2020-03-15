using UnityEngine;

namespace Scripts
{
    public class SoldierSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject SoldierUnit;
        public void SpawnSoldier()
        {
            var nodeList = PathFinding.Instance.GetWalkableNodes();
            if (nodeList.Count > 0)
            {
                var randomIndex = UnityEngine.Random.Range(0, nodeList.Count);
                var clone = Instantiate(SoldierUnit, new Vector3(nodeList[randomIndex].X + .5f, nodeList[randomIndex].Y + .5f, 0), Quaternion.identity);
                nodeList[randomIndex].IsWalkable = false;
            }
            else
            {
                Debug.Log("OUT OF SPACE");
            }
        

        }
    }
}
