using System.Collections.Generic;
using UnityEngine;

public class TriggerInteração : MonoBehaviour
{
    [Header("Objetos que ficam ATIVOS enquanto o player estiver dentro")]
    public List<GameObject> objetosAtivosDentro = new List<GameObject>();

    [Header("Objetos que ficam DESATIVADOS enquanto o player estiver dentro")]
    public List<GameObject> objetosDesativadosDentro = new List<GameObject>();

    [Header("Objetos que são ATIVADOS com tecla E dentro do collider")]
    public List<GameObject> objetosAtivadosComE = new List<GameObject>();

    [Header("Objetos que são DESATIVADOS com tecla E dentro do collider")]
    public List<GameObject> objetosDesativadosComE = new List<GameObject>();

    [Header("Configurações")]
    public bool pararTempoComE = false; // Pausa o tempo e mostra cursor ao apertar E

    private bool playerDentro = false;
    private MeshRenderer meuRenderer;

    private void Awake()
    {
        meuRenderer = GetComponent<MeshRenderer>();
        if (meuRenderer == null)
            Debug.LogWarning("Nenhum MeshRenderer encontrado neste objeto!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;

            foreach (GameObject obj in objetosAtivosDentro)
                if (obj != null) obj.SetActive(true);

            foreach (GameObject obj in objetosDesativadosDentro)
                if (obj != null) obj.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;

            foreach (GameObject obj in objetosAtivosDentro)
                if (obj != null) obj.SetActive(false);

            foreach (GameObject obj in objetosDesativadosDentro)
                if (obj != null) obj.SetActive(true);

            // Se o tempo estava pausado, retoma
            if (pararTempoComE)
                RetomarTempo();
        }
    }

    private void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject obj in objetosAtivadosComE)
                if (obj != null) obj.SetActive(true);

            foreach (GameObject obj in objetosDesativadosComE)
                if (obj != null) obj.SetActive(false);

            if (pararTempoComE)
            {
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        // Detecta se o MeshRenderer foi desativado e retoma o tempo
        if (pararTempoComE && meuRenderer != null && !meuRenderer.enabled)
        {
            RetomarTempo();
        }
    }

    private void RetomarTempo()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
