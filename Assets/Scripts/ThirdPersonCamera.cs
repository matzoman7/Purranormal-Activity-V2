using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform targetPlayer; // Player to follow
    public Vector3 offset = new Vector3(0f, 2f, -4f);// over the should effect
    public float mouseSensitivity = 100f;
    public float distance = 4f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;

    private float yaw = 0f;
    private float pitch = 10f;

    private Player playerScript; // Reference to the Player script

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        Cursor.visible = false;

        if (targetPlayer != null)
            playerScript = targetPlayer.GetComponent<Player>();
    }
    private void LateUpdate()
    {
        if (targetPlayer == null) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax); // limit up/down
        // Rotate camera around player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = targetPlayer.position - rotation * Vector3.forward * distance + Vector3.up * offset.y;

        transform.position = desiredPosition;
        transform.LookAt(targetPlayer.position + Vector3.up * 1.5f);

        // Only rotate player if not rolling or attacking
        if (playerScript != null && !playerScript.isRolling && !playerScript.isAttacking)
        {
            targetPlayer.rotation = Quaternion.Euler(0, yaw, 0);
        }
    }
}
