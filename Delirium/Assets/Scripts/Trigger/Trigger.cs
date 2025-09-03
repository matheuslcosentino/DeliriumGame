using UnityEngine;

public class Trigger : MonoBehaviour
{
    [Header("Objeto que será ativado")]
    public GameObject objetoParaAtivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetoParaAtivar != null)
            {
                objetoParaAtivar.SetActive(true);
                Debug.Log("Objeto ativado pelo Player!");
            }
        }
    }
}
