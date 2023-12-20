using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

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
    public LineRenderer lineRenderer;
    public ControlerLinterna linterna;
    public LayerMask interactableLayer;
    public TextMeshProUGUI ObjetosNecesarios;
    public TextMeshProUGUI mensajePresionaE;
    public Material Cura;
    public Material Objeto;
    RaycastHit hit;

    //variables
    private int currentObjectForWin = 0;
    public int objectFull;
    private int currentEspecialObject = 0;
    public float TimeTextVisible = 3;
    public float MaxDistanceRay = 1;
    private bool hasWonBosque = false;
    public event Action GrabObject;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement * speed;
    }
    private void Update()
    {
 
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, MaxDistanceRay, interactableLayer))
        {
            Debug.DrawRay(transform.position, Camera.transform.forward * hit.distance, Color.red);

            // Mostrar mensaje y cambiar el material del LineRenderer
            mensajePresionaE.text = "Presiona E para agarrar";
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

        }
        else
        {
            Debug.DrawRay(transform.position, Camera.transform.forward * MaxDistanceRay, Color.white);

            // Ocultar el mensaje y desactivar el LineRenderer
            mensajePresionaE.text = "";
            lineRenderer.enabled = false;
        }
        if (currentObjectForWin == ObjetosNecesario.Length && !hasWonBosque)
        {
            // Cambia a la escena de "Ganar"
            SceneManager.LoadScene("Escuela");
            hasWonBosque = true; // Marca que ya ha ganado para evitar múltiples cambios de escena
        }
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
            speed = 9f;
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
            if(Input.GetKeyDown(KeyCode.E))
            {
                InteractWithObject(hit.collider.gameObject);
            }
        }
    }
    private void InteractWithObject(GameObject obj)
    {
        // Puedes usar el tag para determinar el tipo de objeto y realizar la acción correspondiente
        if (obj.CompareTag("Pila"))
        {
            Destroy(obj);
            lineRenderer.material = Cura;
            linterna.currentLife = 100;
            linterna.SliderLife.fillRect.gameObject.SetActive(true);
            linterna.SliderLife.value = linterna.currentLife;
            linterna.lifeReachedHalfEventTriggered = false;
            linterna.lifeReachedZeroEventTriggered = false;
        }
        else if (obj.CompareTag("Objeto"))
        {
            lineRenderer.material = Objeto;
            Destroy(obj);
            GrabObject?.Invoke();
            currentObjectForWin++;
            int objetosFaltantes = ObjetosNecesario.Length - currentObjectForWin;

            // Muestra el texto en el objeto TextMeshPro sin utilizar {$} ni {0}
            ObjetosNecesarios.text = "Objetos: " + currentObjectForWin + "/" + ObjetosNecesario.Length + "\nFaltantes: " + objetosFaltantes;

            // Después de unos segundos, borra el texto
            StartCoroutine(ClearTextAfterDelay(TimeTextVisible));
        }
        else if (obj.CompareTag("ObjetoEspecial"))
        {
            lineRenderer.material = Objeto;
            Destroy(obj);
            currentEspecialObject++;
            int ObjetosEspecialesRestantes = ObjetosEspeciales.Length - currentEspecialObject;
            ObjetosNecesarios.text = "Objetos: " + currentEspecialObject + "/" + ObjetosEspeciales.Length + "\nFaltantes: " + ObjetosEspecialesRestantes;

            StartCoroutine(ClearTextAfterDelay(TimeTextVisible));
        }
    }
    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Borra el texto
        ObjetosNecesarios.text = "";
    }
}
