using UnityEngine;

namespace Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private const string SelectableTag = "Selectable";
        [HideInInspector] public GameObject SelectedObject;

        public static SelectionManager Instance { get; private set; }

        public SelectionManager()
        {
            Instance = this;
        }

        private Transform _selection;

        private void Update()
        {
            // ReSharper disable once RedundantCheckBeforeAssignment
            if (_selection != null)
            {
                _selection = null;
            }

            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit.collider && Input.GetMouseButtonDown(0) && !Builder.Instance.BuildMode)
            {
            
                var selection = hit.transform;

                if (selection.CompareTag(SelectableTag))
                {
                    var selectionRenderer = selection.GetComponent<SpriteRenderer>();
                    if (selectionRenderer != null)
                    {
                        SelectedObject = selection.gameObject;
                        Debug.Log("SELECTED OBJECT: " + selection.gameObject.name);
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.Escape) && SelectedObject != null)
            {
                SelectedObject = null;
                Debug.Log(SelectedObject == null);
            }
        }
    }
}
