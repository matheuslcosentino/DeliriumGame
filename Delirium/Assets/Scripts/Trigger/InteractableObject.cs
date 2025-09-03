using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("UI de Interação")]
    public string mensagem = "Pressione E para interagir";
    public GameObject[] objetosAtivarComE;
    public GameObject[] objetosDesativarComE;

    [Header("Configurações")]
    public bool pararTempoComE = false;

    public virtual void Interagir()
    {
        // Ativa objetos
        foreach (GameObject go in objetosAtivarComE)
            if (go != null) go.SetActive(true);

        // Desativa objetos
        foreach (GameObject go in objetosDesativarComE)
            if (go != null) go.SetActive(false);

        // Pausa o tempo se configurado
        if (pararTempoComE)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        Debug.Log($"{gameObject.name} foi interagido!");
    }
}
