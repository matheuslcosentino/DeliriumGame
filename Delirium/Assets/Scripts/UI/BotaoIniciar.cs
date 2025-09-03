using UnityEngine;
using TMPro;

public class BotaoIniciar : MonoBehaviour
{
    public TMP_InputField inputField; 
    public GameObject button;

    void Start()
    {
        button.SetActive(false);
        inputField.onValueChanged.AddListener(OnInputChanged);
    }

    void OnInputChanged(string text)
    {
        button.SetActive(text.Length >= 3);
    }
}
