using UnityEngine;

public class LiqJumpscare : MonoBehaviour
{
    [Header("Configurações do tremor")]
    public float intensidade = 0.1f; // Força do tremor
    public float velocidadeShake = 50f; // Velocidade das mudanças no tremor

    private Vector3 posicaoOriginal;
    private AudioSource audioSource;
    private Coroutine tremorCoroutine;
    private BoxCollider boxCollider;

    private bool playerDentro = false; // controla se o player está dentro do collider

    private void Awake()
    {
        // Pega o AudioSource anexado ao mesmo objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("Nenhum AudioSource encontrado no objeto " + gameObject.name);

        // Pega o BoxCollider anexado ao mesmo objeto
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
            Debug.LogWarning("Nenhum BoxCollider encontrado no objeto " + gameObject.name);
        else
            boxCollider.isTrigger = true; // garante que seja trigger
    }

    private void OnEnable()
    {
        // Salva posição inicial
        posicaoOriginal = transform.localPosition;

        // Inicia tremor
        tremorCoroutine = StartCoroutine(TremerObjeto());

        // Ativa e coloca o áudio no volume máximo imediatamente
        if (audioSource != null)
        {
            audioSource.volume = 1f;
            audioSource.Play();
        }

        // Ativa o BoxCollider
        if (boxCollider != null)
            boxCollider.enabled = true;
    }

    private void OnDisable()
    {
        // Resetar posição quando desativar
        transform.localPosition = posicaoOriginal;

        if (tremorCoroutine != null)
            StopCoroutine(tremorCoroutine);

        // Se quiser que o som pare ao desativar
        if (audioSource != null)
            audioSource.Stop();

        // Desativa o BoxCollider
        if (boxCollider != null)
            boxCollider.enabled = false;

        playerDentro = false;
    }

    private void Update()
    {
        // Se o player estiver dentro do collider e apertar E
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            this.enabled = false; // desativa o script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;
        }
    }

    private System.Collections.IEnumerator TremerObjeto()
    {
        while (true) // fica tremendo enquanto o script estiver ativo
        {
            float x = Mathf.PerlinNoise(Time.time * velocidadeShake, 0f) * 2 - 1;
            float y = Mathf.PerlinNoise(0f, Time.time * velocidadeShake) * 2 - 1;
            float z = Mathf.PerlinNoise(Time.time * velocidadeShake, Time.time * velocidadeShake) * 2 - 1;

            transform.localPosition = posicaoOriginal + new Vector3(x, y, z) * intensidade;

            yield return null;
        }
    }
}
