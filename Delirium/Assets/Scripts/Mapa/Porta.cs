using UnityEngine;

public class Porta : MonoBehaviour
{
    [Header("Configurações da Porta")]
    public Transform pivot;
    public float anguloMax = 90f;
    public float velocidade = 90f;

    [Header("Interação")]
    public bool precisaDeChave = false; // setar no inspector
    public GameObject textoInteracao;
    public GameObject crosshairPadrao;
    public GameObject crosshairInteracao;
    public GameObject avisoTrancada; // aparece quando tentar abrir trancada

    [Header("Áudio")]
    public AudioSource audioPorta;
    public AudioClip somAbrir;
    public AudioClip somFechar;
    public AudioClip somTrancada; // som quando precisa de chave e não pode abrir

    private bool aberta = false;
    private bool playerNaRange = false;
    private float anguloAtual = 0f;
    private PlayerController player; // referência ao player

    void Start()
    {
        if (avisoTrancada != null)
            avisoTrancada.SetActive(false); // começa desativado
    }

    void Update()
    {
        if (playerNaRange && Input.GetKeyDown(KeyCode.E))
        {
            if (precisaDeChave && (player == null || !player.temChave))
            {
                Debug.Log("A porta está trancada. Precisa de uma chave!");

                // 🔊 toca som de trancada apenas se não estiver tocando
                if (audioPorta != null && somTrancada != null && !audioPorta.isPlaying)
                {
                    audioPorta.clip = somTrancada;
                    audioPorta.Play();
                }

                // 🔔 mostra o aviso
                if (avisoTrancada != null)
                {
                    avisoTrancada.SetActive(true);
                    CancelInvoke(nameof(EsconderAvisoTrancada));
                    Invoke(nameof(EsconderAvisoTrancada), 2f); // some depois de 2s
                }

                return; // não abre se não tiver chave
            }

            aberta = !aberta;

            if (audioPorta != null)
            {
                audioPorta.clip = aberta ? somAbrir : somFechar;
                audioPorta.Play();
            }
        }

        if (aberta && anguloAtual < anguloMax)
        {
            float delta = velocidade * Time.deltaTime;

            if (anguloAtual + delta > anguloMax)
                delta = anguloMax - anguloAtual;

            transform.RotateAround(pivot.position, Vector3.up, delta);
            anguloAtual += delta;
        }
        else if (!aberta && anguloAtual > 0f)
        {
            float delta = velocidade * Time.deltaTime;

            if (anguloAtual - delta < 0f)
                delta = anguloAtual;

            transform.RotateAround(pivot.position, Vector3.up, -delta);
            anguloAtual -= delta;
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

            if (crosshairPadrao != null)
                crosshairPadrao.SetActive(false);

            if (crosshairInteracao != null)
                crosshairInteracao.SetActive(true);
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

            if (crosshairPadrao != null)
                crosshairPadrao.SetActive(true);

            if (crosshairInteracao != null)
                crosshairInteracao.SetActive(false);
        }
    }

    private void EsconderAvisoTrancada()
    {
        if (avisoTrancada != null)
            avisoTrancada.SetActive(false);
    }
}
