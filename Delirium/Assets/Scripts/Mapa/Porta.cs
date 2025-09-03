using UnityEngine;

public class Porta : MonoBehaviour
{
    [Header("Configurações da Porta")]
    public Transform pivot;
    public float anguloMax = 90f;
    public float velocidade = 90f;
    public bool abreParaTras = false;

    [Header("Interação")]
    public bool precisaDeChave = false; // setar no inspector
    public GameObject textoInteração;
    public GameObject crosshairPadrao;
    public GameObject crosshairInteração;

    [Header("Objetos extras ao tentar abrir trancada")]
    public GameObject[] objetosAtivarSeTrancada;   // lista de objetos para ativar
    public GameObject[] objetosDesativarSeTrancada; // lista de objetos para desativar

    [Header("Áudio")]
    public AudioSource audioPorta;
    public AudioClip somAbrir;
    public AudioClip somFechar;

    private bool aberta = false;
    private bool playerNaRange = false;
    private float anguloAtual = 0f;
    private PlayerController player; // referência ao player

    void Update()
    {
        if (playerNaRange && Input.GetKeyDown(KeyCode.E))
        {
            // Porta trancada e player não tem chave
            if (precisaDeChave && (player == null || !player.temChave))
            {
                Debug.Log("A porta está trancada. Precisa de uma chave!");

                // Ativa os objetos configurados no Inspector
                foreach (GameObject go in objetosAtivarSeTrancada)
                {
                    if (go != null) go.SetActive(true);
                }

                // Desativa os objetos configurados no Inspector
                foreach (GameObject go in objetosDesativarSeTrancada)
                {
                    if (go != null) go.SetActive(false);
                }

                return; // não abre a porta
            }

            // Se tiver chave ou não precisar de chave, abre/fecha
            aberta = !aberta;

            if (audioPorta != null)
            {
                audioPorta.clip = aberta ? somAbrir : somFechar;
                audioPorta.Play();
            }
        }

        // Animação da porta
        if (aberta && anguloAtual < anguloMax)
        {
            float delta = velocidade * Time.deltaTime;
            if (abreParaTras) delta = -delta;

            if (Mathf.Abs(anguloAtual + delta) > anguloMax)
                delta = Mathf.Sign(delta) * (anguloMax - Mathf.Abs(anguloAtual));

            transform.RotateAround(pivot.position, Vector3.up, delta);
            anguloAtual += Mathf.Abs(delta);
        }
        else if (!aberta && anguloAtual > 0f)
        {
            float delta = velocidade * Time.deltaTime;
            if (abreParaTras) delta = -delta;

            if (Mathf.Abs(anguloAtual - delta) < 0f)
                delta = Mathf.Sign(delta) * anguloAtual;

            transform.RotateAround(pivot.position, Vector3.up, -delta);
            anguloAtual -= Mathf.Abs(delta);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = true;
            player = other.GetComponent<PlayerController>();

            if (textoInteração != null)
                textoInteração.SetActive(true);

            if (crosshairPadrao != null)
                crosshairPadrao.SetActive(false);

            if (crosshairInteração != null)
                crosshairInteração.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaRange = false;
            player = null;

            if (textoInteração != null)
                textoInteração.SetActive(false);

            if (crosshairPadrao != null)
                crosshairPadrao.SetActive(true);

            if (crosshairInteração != null)
                crosshairInteração.SetActive(false);
        }
    }
}
