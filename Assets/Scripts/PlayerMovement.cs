using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;
    public float clampAngle = 80f;
    public float speed = 5f;

    public PlayerMovement player;
    public CinemachineVirtualCamera Camera;
    private Vector3 camForward;
    private Vector3 camRight; 

    private Rigidbody playerRigidbody;

    private Vector3 movement;
    private Vector3 Rotation;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement * speed;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0f, movementInput.y);
        
        movement.Normalize();
        camDirection();

        movement = movementInput.x * camRight +movementInput.y * camForward;

        player.transform.LookAt(player.transform.position + movement);
    }
    public void camDirection()
    {
        camForward = Camera.transform.forward;
        camRight = Camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }
}
