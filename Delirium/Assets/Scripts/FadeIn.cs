using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage; // arraste a Image preta do Canvas aqui
    public float fadeDuration = 2f; // tempo do fade em segundos

    void Start()
    {
        // Garante que a imagem está totalmente preta no começo
        fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false); // desliga depois do fade
    }
}
