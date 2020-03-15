using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class InformationPanelController : MonoBehaviour
    {
        public GameObject InformationPanel;
        public Image ObjectImage;
        public Text ObjectName;
        public Text ObjectInfo;
        public Button EventButton;

        private void Update()
        {
            if (SelectionManager.Instance.SelectedObject != null)
            {
            
                var obj = SelectionManager.Instance.SelectedObject;
                InformationPanel.SetActive(true);
                ObjectName.text = obj.GetComponent<IObject>().Name;
                ObjectInfo.text = obj.GetComponent<IObject>().DisplayInfo();
                ObjectImage.sprite = obj.GetComponent<IObject>().Icon;

                var productive = obj.GetComponent<IProductive>();
                if (productive != null)
                {
                    EventButton.gameObject.SetActive(true);
                    Text eventButtonText = EventButton.GetComponentInChildren<Text>();
                    eventButtonText.text = "Spawn Soldier";
                }
                else
                {
                    EventButton.gameObject.SetActive(false);
                }
            }
            else
            {
                InformationPanel.SetActive(false);

            }


        }
    }
}
