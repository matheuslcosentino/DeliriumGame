using UnityEngine;

public class PhoneDisable : MonoBehaviour
{
    private bool playerDentro = false; // controla se o player está dentro do collider
    private Light luz;
    private AudioSource audioSource;

    private void Awake()
    {
        luz = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();

        if (luz == null)
            Debug.LogWarning("Não há Light no objeto " + gameObject.name);
        if (audioSource == null)
            Debug.LogWarning("Não há AudioSource no objeto " + gameObject.name);
    }

    private void Update()
    {
        // Se o player estiver dentro do collider e apertar E
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (luz != null)
                luz.enabled = false;

            if (audioSource != null)
                audioSource.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerDentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerDentro = false;
    }
}
