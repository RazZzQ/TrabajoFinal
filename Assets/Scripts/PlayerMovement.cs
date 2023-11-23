using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);

        movement.Normalize();

        playerRigidbody.velocity = movement * speed;
    }
}
