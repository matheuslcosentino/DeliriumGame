using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    [Header("Câmera")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    private float xRotation = 0f; // Para controlar rotação vertical

    [Header("Objeto para Ser Ativado")]
    public GameObject objetoTab;
    public GameObject objDesativ;
    public GameObject objDesativ2;

    [Header("Inventário")]
    public bool temChave = false;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        Mouse();
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
        HandleTab();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward; // agora usa o transform do player
        Vector3 right = transform.right;

        Vector3 move = (forward * vertical + right * horizontal).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita rotação vertical

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX); // Rotação horizontal do player
    }

    void HandleTab()
    {
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
