using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
[RequireComponent(typeof(AudioSource))]
public class InputSound : MonoBehaviour
{
    [Header("Som ao digitar")]
    public AudioClip keySound; // arraste o som no Inspector

    private TMP_InputField input;
    private AudioSource audioSource;
    private int lastLength = 0;

    private void Awake()
    {
        input = GetComponent<TMP_InputField>();
        audioSource = GetComponent<AudioSource>();

        // garante que o áudio não fique em loop
        audioSource.loop = false;

        // registra callback
        input.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string text)
    {
        // só toca se realmente houve acréscimo de caractere
        if (text.Length > lastLength)
        {
            if (keySound != null)
            {
                // define pitch aleatório entre 1 e 3
                audioSource.pitch = Random.Range(1f, 3f);
                audioSource.PlayOneShot(keySound);
            }
        }

        lastLength = text.Length;
    }
}
