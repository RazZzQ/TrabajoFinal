using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //Velocidad player
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;
    public float clampAngle = 80f;
    public float speed = 5f;
    //movimiento camara
    private Vector3 movement;
    private Vector3 camForward;
    private Vector3 camRight; 
    //referencias
    public PlayerMovement player;
    public CinemachineVirtualCamera Camera;
    public Objeto[] ObjetosNecesario;
    public ObjetoEspecial[] ObjetosEspeciales;
    private Rigidbody playerRigidbody;

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

        movement = movementInput.x * camRight + movementInput.y * camForward;

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

    public void OnRuning(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = 7f;
        }
        else if (context.canceled)
        {
            speed = 5f;
        }
    }
    public void OnPressE(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
}
