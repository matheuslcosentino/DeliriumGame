using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    [Header("Câmera")]
    public Transform cameraTransform;

    [Header("Objeto para Ser Ativado")]
    public GameObject objetoTab;
    public GameObject objDesativ;
    public GameObject objDesativ2;

    [Header("Inventário")]
    public bool temChave = false; // será ativado ao pegar a chave

    private CharacterController controller;

    void Start()
    {
        Mouse();
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * vertical + right * horizontal).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            objetoTab.SetActive(!objetoTab.activeSelf);
            objDesativ.SetActive(false);
            objDesativ2.SetActive(false);
        }
    }

    void Mouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
