using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador

    private Rigidbody playerRigidbody;

    void Start()
    {
        // Obtener la referencia al componente Rigidbody del jugador
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener las entradas de teclado para el movimiento
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calcular el vector de movimiento
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Aplicar la fuerza al Rigidbody para el movimiento
        playerRigidbody.AddForce(movement * speed);
    }
}
