using UnityEngine;

namespace Scripts
{
    public class Construction : ObjectClass, IBuildable
    {
        public override string DisplayInfo()
        {
            var infoText = Name + " is just a construction";
            return infoText;
        }

        public void BuildConstruction(GameObject _obj, Vector3 pos)
        {
            _obj.transform.position = pos;
        }
    }
}