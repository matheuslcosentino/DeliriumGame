using UnityEngine;

public class EspelhoCheck : MonoBehaviour
{
    [Header("Objetos do espelho")]
    public GameObject PanoCobrindo; // Objeto 1
    public GameObject PanoCaido;    // Objeto 2

    private Collider meuCollider;

    private void Awake()
    {
        meuCollider = GetComponent<Collider>();
        if (meuCollider == null)
            Debug.LogWarning("EspelhoCheck precisa de um Collider no mesmo objeto!");
    }

    private void Update()
    {
        // Se o PanoCobrindo estiver ativo, não faz nada
        if (PanoCobrindo != null && PanoCobrindo.activeInHierarchy)
        {
            if (meuCollider != null && meuCollider.isTrigger)
                meuCollider.enabled = false; // desativa o trigger caso esteja ativo
            return;
        }

        // Se o PanoCaido estiver ativo, ativa o trigger
        if (PanoCaido != null && PanoCaido.activeInHierarchy)
        {
            if (meuCollider != null)
            {
                meuCollider.enabled = true;    // ativa o collider
                meuCollider.isTrigger = true;  // garante que é trigger
            }
        }
        else
        {
            // Caso o PanoCaido esteja desativado, garante que o trigger também fique desativado
            if (meuCollider != null)
                meuCollider.enabled = false;
        }
    }
}
