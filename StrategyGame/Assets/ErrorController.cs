using System.Collections;
using Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ErrorController : MonoBehaviour
{
    private Text _errorText;
    private bool _isTextShown;
    private void Start()
    {
        ErrorEvents.Current.OnOutOfSpaceTriggerEnter += OutOfSpace;
    }

    private void OutOfSpace()
    {
        if (_isTextShown) return;
        _errorText = gameObject.GetComponent<Text>();
        _errorText.text = "Out of Space";
        _errorText.enabled = true;
        _isTextShown = true;
        StartCoroutine(PopUpErrorMessage());

    }

    IEnumerator PopUpErrorMessage()
    {
        yield return new WaitForSeconds(2f);
        _errorText.enabled = false;
    }
}
