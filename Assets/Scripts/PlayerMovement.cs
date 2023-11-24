using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f; // Ajusta la velocidad de rotaci�n del jugador al girar la c�mara.

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

        // Obt�n la rotaci�n actual del jugador.
        Vector3 currentRotation = transform.eulerAngles;

        // Calcula la nueva rotaci�n del jugador en funci�n del input del rat�n.
        currentRotation.y += mouseDelta.x * lookSpeed;

        // Aplica la nueva rotaci�n al jugador.
        transform.eulerAngles = currentRotation;

        // Accede al componente CinemachinePOV para ajustar la rotaci�n de la c�mara.
        CinemachinePOV pov = GetComponentInChildren<CinemachinePOV>();
        if (pov != null)
        {
            // Ajusta la rotaci�n de la c�mara bas�ndote en el input del rat�n.
            pov.m_Rig.Rotate(Vector3.up, mouseDelta.x * lookSpeed);
        }
    }
}
