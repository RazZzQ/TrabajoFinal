using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f; // Ajusta la velocidad de rotación del jugador al girar la cámara.

    private Rigidbody playerRigidbody;
    private Vector3 movement;

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

        // Obtén la rotación actual del jugador.
        Vector3 currentRotation = transform.eulerAngles;

        // Calcula la nueva rotación del jugador en función del input del ratón.
        currentRotation.y += mouseDelta.x * lookSpeed;

        // Aplica la nueva rotación al jugador.
        transform.eulerAngles = currentRotation;

        // Accede al componente CinemachinePOV para ajustar la rotación de la cámara.
        CinemachinePOV pov = GetComponentInChildren<CinemachinePOV>();
        if (pov != null)
        {
            // Ajusta la rotación de la cámara basándote en el input del ratón.
            pov.m_Rig.Rotate(Vector3.up, mouseDelta.x * lookSpeed);
        }
    }
}
