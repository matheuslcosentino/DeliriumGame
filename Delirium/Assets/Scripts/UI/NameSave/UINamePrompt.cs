using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UINamePrompt : MonoBehaviour
{
    [Header("Referências")]
    public TMP_InputField input;

    [Header("Fluxo")]
    public bool pauseTimeWhilePrompt = true;
    public UnityEvent onStartGame; // eventos a disparar quando confirmar (ex.: habilitar controles)

    private void Start()
    {
        // Se já tem nome salvo, pula o prompt
        if (NameManager.Instance != null && NameManager.Instance.HasName())
        {
            gameObject.SetActive(false);
            onStartGame?.Invoke();
            return;
        }

        // Mostra o prompt
        gameObject.SetActive(true);
        input.text = "";
        input.ActivateInputField();
        if (pauseTimeWhilePrompt) Time.timeScale = 0f;
    }

public void Confirm()
{
    string name = input.text.Trim();
    if (string.IsNullOrEmpty(name))
    {
        input.Select();
        input.ActivateInputField();
        return;
    }

    NameManager.Instance.SetName(name);
    if (pauseTimeWhilePrompt) Time.timeScale = 1f;

    gameObject.SetActive(false); // só esconde esse painel
    // removi o onStartGame.Invoke(); para não disparar nada
}


    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        // Permite confirmar com Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Confirm();
        }
    }
}
