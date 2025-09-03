using UnityEngine;
using System.Collections;

public class DutoTutorial : MonoBehaviour
{
    [Header("Configurações")]
    public float duracaoRotacao = 1f; // tempo para completar a rotação
    private bool playerDentro = false;
    private bool rotacionando = false;

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

    private void Update()
    {
        if (playerDentro && !rotacionando && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Rotacionar90X());
        }
    }

    private IEnumerator Rotacionar90X()
    {
        rotacionando = true;

        Quaternion rotInicial = transform.rotation;
        Quaternion rotFinal = rotInicial * Quaternion.Euler(90f, 0f, 0f);

        float tempo = 0f;

        while (tempo < duracaoRotacao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoRotacao;
            transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);
            yield return null;
        }

        transform.rotation = rotFinal;
        rotacionando = false;
    }
}
