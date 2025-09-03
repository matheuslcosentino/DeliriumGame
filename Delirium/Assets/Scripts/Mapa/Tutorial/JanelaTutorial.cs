using UnityEngine;
using System.Collections;

public class JanelaTutorial : MonoBehaviour
{
    [Header("Configurações")]
    public Transform pivot;           // O ponto em torno do qual a janela vai girar
    public Transform janela;          // O objeto real que será rotacionado
    public float anguloRotacao = 90f; // Quanto rotacionar no eixo X
    public float duracaoRotacao = 1f; // Tempo da rotação

    private bool playerDentro = false;
    private bool rotacionando = false;
    private Collider janelaCollider;  // Armazena o collider da janela

    private void Start()
    {
        if (janela != null)
            janelaCollider = janela.GetComponent<Collider>();
        else
            Debug.LogWarning("Janela não atribuída no JanelaTutorial!");
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

    private void Update()
    {
        // Só permite rotacionar se o Player estiver dentro, não estiver rotacionando e o collider estiver ativo
        if (playerDentro && !rotacionando && janelaCollider != null && janelaCollider.enabled && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RotacionarJanela());
        }
    }

    private IEnumerator RotacionarJanela()
    {
        rotacionando = true;

        float tempo = 0f;
        float anguloAtual = 0f;

        while (tempo < duracaoRotacao)
        {
            float delta = (anguloRotacao / duracaoRotacao) * Time.deltaTime;
            janela.RotateAround(pivot.position, Vector3.right, delta);
            anguloAtual += delta;
            tempo += Time.deltaTime;
            yield return null;
        }

        // Corrige a rotação final exata
        float resto = anguloRotacao - anguloAtual;
        if (resto != 0)
            janela.RotateAround(pivot.position, Vector3.right, resto);

        // Desativa o Collider da janela depois da rotação
        if (janelaCollider != null)
            janelaCollider.enabled = false;

        rotacionando = false;
    }
}
    