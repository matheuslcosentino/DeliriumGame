using UnityEngine;
using TMPro;
using System;

public class SystemClock : MonoBehaviour
{
    public TMP_Text clockText; // Arraste o TextMeshPro aqui

    void Update()
    {
        DateTime now = DateTime.Now;

        // Formato estilo Windows
        string time = now.ToString("HH:mm");       // 16:33
        string date = now.ToString("dd/MM/yyyy"); // 27/08/2025

        clockText.text = time + "\n" + date;
    }
}
