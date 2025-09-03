using UnityEngine;

public class Chave : MonoBehaviour
{
    [Header("Configuração da Chave")]
    public GameObject textoInteracao;

    private bool playerNaRange = false;
    private PlayerController player;

    void Update()
    {
        if (playerNaRange && Input.GetKeyDown(KeyCode.E))
        {
            if (player != null)
            {
                player.temChave = true;
                Debug.Log("Chave coletada!");
                Destroy(gameObject); // remove a chave da cena
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = true;
            player = other.GetComponent<PlayerController>();

            if (textoInteracao != null)
                textoInteracao.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = false;
            player = null;

            if (textoInteracao != null)
                textoInteracao.SetActive(false);
        }
    }
}
