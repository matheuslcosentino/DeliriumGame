using UnityEngine;

public class UIMove : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float distancia;
    public bool moverParaDireita = true;
    public float velocidade = 200f;

    private RectTransform rectTransform;
    private Vector2 posicaoInicial;
    private Vector2 posicaoDestino;
    private bool movendo = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        posicaoInicial = rectTransform.anchoredPosition;

        float direcao = moverParaDireita ? 1f : -1f;
        posicaoDestino = posicaoInicial + new Vector2(distancia * direcao, 0f);

        movendo = true;
    }

    private void Update()
    {
        if (movendo)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                posicaoDestino,
                velocidade * Time.deltaTime
            );

            if (Vector2.Distance(rectTransform.anchoredPosition, posicaoDestino) < 0.01f)
            {
                rectTransform.anchoredPosition = posicaoDestino;
                movendo = false;
            }
        }
    }
}
