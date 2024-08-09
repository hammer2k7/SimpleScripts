using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float verticalSpeed = 3f;
    public float mouseSensitivity = 2f;
    public float yawLimit = 360f;
    public float pitchLimit = 89f;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseLook();
    }

    private void HandleKeyboardInput()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;

        // Vertical movement
        if (Input.GetKey(KeyCode.Q))
        {
            movement += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            movement += Vector3.down * verticalSpeed * Time.deltaTime;
        }

        // Apply movement
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust yaw and pitch
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp pitch to avoid over-rotation
        pitch = Mathf.Clamp(pitch, -pitchLimit, pitchLimit);

        // Apply rotation
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}