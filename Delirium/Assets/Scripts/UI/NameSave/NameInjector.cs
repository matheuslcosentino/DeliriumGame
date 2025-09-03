using UnityEngine;
using TMPro;

public class NameInjector : MonoBehaviour
{
    public TMP_Text targetText;
    [TextArea]
    public string template = "Bem-vindo, {name}! Sua jornada começa agora...";

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (targetText == null) return;

        string n = NameManager.Instance != null ? NameManager.Instance.PlayerName : "";
        if (string.IsNullOrEmpty(n)) n = "Visitante";

        targetText.text = template.Replace("{name}", n);
    }
}
