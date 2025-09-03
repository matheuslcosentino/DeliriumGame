using UnityEngine;

public class Liquidificador : MonoBehaviour
{
    [Header("Objeto que contém o script LiqJumpscare")]
    public GameObject alvo; // Arraste aqui no Inspector o objeto que tem o LiqJumpscare

    private void OnTriggerEnter(Collider other)
    {
        // Só ativa se for o Player
        if (other.CompareTag("Player") && alvo != null)
        {
            LiqJumpscare script = alvo.GetComponent<LiqJumpscare>();
            if (script != null)
            {
                script.enabled = true; // Ativa o script
            }
            else
            {
                Debug.LogWarning("O objeto alvo não tem o script LiqJumpscare!");
            }
        }
    }
}
