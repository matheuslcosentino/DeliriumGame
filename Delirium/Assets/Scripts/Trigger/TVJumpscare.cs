using UnityEngine;
using UnityEngine.Video;
public class TVJumpscare : MonoBehaviour
{
    [Header("Objetos do Jumpscare")]
    public GameObject objetoComVideo;
    public GameObject objetoImagemVideo;
    public GameObject objetoCollider;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        if (objetoComVideo != null)
            videoPlayer = objetoComVideo.GetComponent<VideoPlayer>();

        if (videoPlayer != null)
            videoPlayer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (videoPlayer != null)
                videoPlayer.enabled = true;

            if (objetoImagemVideo != null)
                objetoImagemVideo.SetActive(true);

            if (objetoCollider != null)
            {
                BoxCollider col = objetoCollider.GetComponent<BoxCollider>();
                if (col != null)
                    col.enabled = true;
                else
                    Debug.LogWarning("O objeto 3 não tem BoxCollider!");
            }
        }
    }
}
