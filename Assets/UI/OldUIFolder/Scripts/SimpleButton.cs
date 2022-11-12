using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Button button {
        get {
            return gameObject.GetComponent(typeof(Button)) as Button;
        }
    }
    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }
}
