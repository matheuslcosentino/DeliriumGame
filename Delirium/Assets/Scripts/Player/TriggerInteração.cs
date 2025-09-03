using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteração : MonoBehaviour
{
    public List<GameObject> objetosAtivosDentro = new List<GameObject>();
    public List<GameObject> objetosDesativadosDentro = new List<GameObject>();
    public List<GameObject> objetosAtivarComE = new List<GameObject>();
    public List<GameObject> objetosDesativarComE = new List<GameObject>();

    public bool PararTempo = false;
    public bool destravarMouse = false;
    public MonoBehaviour scriptMovimentoCamera;

    private bool playerDentro = false;

    private void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject obj in objetosAtivarComE)
                obj.SetActive(true);

            foreach (GameObject obj in objetosDesativarComE)
                obj.SetActive(false);
        }

        bool algumAtivo = false;
        foreach (GameObject obj in objetosAtivarComE)
        {
            if (obj.activeInHierarchy)
            {
                algumAtivo = true;
                break;
            }
        }

        if (PararTempo)
        {
            Time.timeScale = algumAtivo ? 0f : 1f;
            if (scriptMovimentoCamera != null)
                scriptMovimentoCamera.enabled = !algumAtivo;
        }

        if (destravarMouse)
        {
            Cursor.visible = algumAtivo;
            Cursor.lockState = algumAtivo ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in objetosAtivosDentro)
            obj.SetActive(false);

        foreach (GameObject obj in objetosDesativadosDentro)
            obj.SetActive(true);

        foreach (GameObject obj in objetosDesativadosDentro)
            obj.SetActive(true);

        foreach (GameObject obj in objetosDesativarComE)
            obj.SetActive(false);               
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;

            foreach (GameObject obj in objetosAtivosDentro)
                obj.SetActive(true);

            foreach (GameObject obj in objetosDesativadosDentro)
                obj.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;

            foreach (GameObject obj in objetosAtivosDentro)
                obj.SetActive(false);

            foreach (GameObject obj in objetosDesativadosDentro)
                obj.SetActive(true);
        }
    }
}
