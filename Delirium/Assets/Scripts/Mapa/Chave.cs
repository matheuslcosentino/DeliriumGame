using UnityEngine;

public class Chave : MonoBehaviour
{
    private bool playerNaRange = false;
    private PlayerController player;

    void Update()
    {
        if (playerNaRange && Input.GetKeyDown(KeyCode.E))
        {
            if (player != null)
            {
                player.temChave = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = false;
            player = null;
        }
    }
}
