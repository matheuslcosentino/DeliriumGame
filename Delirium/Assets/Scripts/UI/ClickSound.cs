using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Graphic))] // garante que tem um componente de UI
public class ClickSound : MonoBehaviour, IPointerClickHandler
{
    [Header("Áudio")]
    public AudioClip clip;
    public AudioSource source; // pode ser o AudioSource do objeto ou de outro lugar

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
