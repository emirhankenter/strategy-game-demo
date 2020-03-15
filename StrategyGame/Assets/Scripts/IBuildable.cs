using UnityEngine;

namespace Scripts
{
    public interface IBuildable
    {
        void BuildConstruction(GameObject _obj, Vector3 pos);
    }
}