using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrocaCena : MonoBehaviour
{
    public Image fadeImage;
    public float duracaoFade = 1f;

    private void Start()
    {
        // Começa com fade-in
        StartCoroutine(FadeIn());
    }

    public void TrocarCena(string nomeCena)
    {
        StartCoroutine(FadeOutAsync(nomeCena));
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;
        Color cor = fadeImage.color;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime / duracaoFade; // unscaledDeltaTime ignora Time.timeScale
            cor.a = t;
            fadeImage.color = cor;
            yield return null;
        }

        cor.a = 0f;
        fadeImage.color = cor;
    }

    private IEnumerator FadeOutAsync(string nomeCena)
    {
        float t = 0f;
        Color cor = fadeImage.color;

        // Fade-out
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duracaoFade;
            cor.a = t;
            fadeImage.color = cor;
            yield return null;
        }

        cor.a = 1f;
        fadeImage.color = cor;

        // Carregamento assíncrono da cena
        AsyncOperation carregamento = SceneManager.LoadSceneAsync(nomeCena);
        carregamento.allowSceneActivation = false;

        // Espera até que a cena esteja carregada pelo menos 90%
        while (carregamento.progress < 0.9f)
        {
            yield return null;
        }

        // Ativa a cena
        carregamento.allowSceneActivation = true;
    }
}
