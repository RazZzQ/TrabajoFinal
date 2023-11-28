using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;
    public float clampAngle = 80f;
    public float speed = 5f;
    CameraState state;
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
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        if(Rotation == null)
        {
            Rotation = transform.localRotation.eulerAngles;
        }
        Rotation.x += mouseDelta.x * Time.deltaTime;
        Rotation.y += mouseDelta.y * Time.deltaTime;
        Rotation.y = Mathf.Clamp(Rotation.y, -clampAngle, clampAngle);
        state.RawOrientation = Quaternion.Euler(Rotation.y, Rotation.x, 0f);
        Debug.Log(mouseDelta);
    }
}
