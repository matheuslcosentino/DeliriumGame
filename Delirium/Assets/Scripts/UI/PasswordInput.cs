using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class PasswordInput : MonoBehaviour
{
    private void Awake()
    {
        TMP_InputField input = GetComponent<TMP_InputField>();

        // Faz com que todos os caracteres digitados apareçam como '*'
        input.inputType = TMP_InputField.InputType.Password;
        input.asteriskChar = '*';
    }
}
