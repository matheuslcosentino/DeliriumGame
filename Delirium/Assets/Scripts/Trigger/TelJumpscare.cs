using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelJumpscare : MonoBehaviour
{
    [Header("Lista de objetos possíveis")]
    public List<GameObject> objetosParaEscolher; // lista de objetos para ativar áudio e luz

    [Header("Configurações da luz")]
    public float intervaloPiscar = 0.3f; // tempo entre ligar/desligar a luz

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetosParaEscolher.Count == 0)
            {
                Debug.LogWarning("Lista de objetos vazia!");
                return;
            }

            // Escolhe aleatoriamente um objeto
            int indice = Random.Range(0, objetosParaEscolher.Count);
            GameObject objEscolhido = objetosParaEscolher[indice];

            // Ativa o AudioSource do objeto
            AudioSource audio = objEscolhido.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.enabled = true;
                audio.Play();
            }
            else
            {
                Debug.LogWarning("O objeto escolhido não tem AudioSource!");
            }

            // Inicia o piscar da luz
            Light luz = objEscolhido.GetComponent<Light>();
            if (luz != null)
            {
                StartCoroutine(PiscarLuz(luz));
            }
            else
            {
                Debug.LogWarning("O objeto escolhido não tem Light!");
            }
        }
    }

    private IEnumerator PiscarLuz(Light luz)
    {
        while (true) // ficará piscando até o objeto ser destruído ou desativado
        {
            luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(intervaloPiscar);
        }
    }
}
