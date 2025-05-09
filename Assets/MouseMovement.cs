using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // Mouse sensitivity
    public float verticalLookLimit = 80f;  // Clamp vertical look to avoid flipping

    private Camera playerCamera;
    private Transform playerBody;          // Reference to the player's body for rotation
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;                  // Reference to the main camera
        playerBody = transform;                      // Reference to the player body (the GameObject this script is attached to)

        // Lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
    }

    // Handle rotation based on mouse movement
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (look up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // Rotate the camera vertically

        // Horizontal rotation (rotate player body)
        playerBody.Rotate(Vector3.up * mouseX);  // Rotate the player horizontally based on mouse X
    }
}
