using UnityEngine;

namespace Scripts
{
    public class Builder : MonoBehaviour
    {
        [HideInInspector] public bool BuildMode;

        private GameController _gameController;
        private GameObject _objClone;
        private Construction _building;
        private Vector3 _buildingPos;
        private Vector3 _mousePos;
        private bool _buildableArea;

        private void Awake()
        {
            Instance = this;
        }

        public static Builder Instance { get; private set; }

        private void Start()
        {
            _gameController = GameController.Instance;
        }
        private void Update()
        {
            if (BuildMode)
            {
                PreviewBuilding();
                _buildableArea = CheckBuildableArea(_building.Width, _building.Height, _buildingPos);
            
                if (Input.GetMouseButtonDown(0) && _buildableArea)
                {
                    Debug.Log(_building.Name + " has been built.");
                    BuildConstruction(_objClone);
                    BuildMode = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && BuildMode)
            {
                // Cancel building
                Destroy(_objClone);
                BuildMode = false;
            }
        }

        public void StartBuilding(GameObject obj)
        {
            if (_objClone == null)
            {
                BuildMode = true;
                CreateClone(obj);
                _building = _objClone.gameObject.GetComponent<Construction>();
            }
            else
            {
                Destroy(_objClone);
                BuildMode = true;
                CreateClone(obj);
                _building = _objClone.gameObject.GetComponent<Construction>();
            }
        }

        private void CreateClone(GameObject obj)
        {
            var buildable = obj.GetComponent<IBuildable>();
            if (buildable == null) { return; }
            _objClone = Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        public bool CheckBuildableArea(float width, float height, Vector3 mousePos)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (!PathFinding.Instance.IsGridWalkable(mousePos + new Vector3(i, j)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void BuildConstruction(GameObject obj)
        {
            var buildable = obj.GetComponent<IBuildable>();
            buildable.BuildConstruction(_objClone, _buildingPos);
            _gameController.PathFinding.SetGridWalkableValueObject(_buildingPos, _building.Width, _building.Height);
            _objClone = null;
        }
        private void PreviewBuilding()
        {
            _mousePos = MouseController.GetMouseWorldPosition();
            _buildingPos.x = Mathf.FloorToInt(((_mousePos.x / 0.67f) * 0.67f) - _building.Width / 2);
            _buildingPos.y = Mathf.FloorToInt(((_mousePos.y / 0.67f) * 0.67f) - _building.Height / 2);
            var tmpColor = _building.GetComponent<SpriteRenderer>().color;
            _building.transform.position = _buildingPos;

            HighlightArea(_buildableArea, out tmpColor);
        }

        private void HighlightArea(bool buildableArea, out Color tmpColor)
        {
            if (!buildableArea)
            {
                tmpColor = new Color(1f, 0f, 0f, .5f);
            }
            else
            {
                tmpColor = new Color(1f, 1f, 1f, 1f);
            }
            _building.GetComponent<SpriteRenderer>().color = tmpColor;
        }
    }
}
