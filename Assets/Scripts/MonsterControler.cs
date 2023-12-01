using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MonsterControler : MonoBehaviour
{
    //variables
    public float distanciaAtaque = 2f;
    public float distanciaVision = 5f;
    public float tiempoStunMaximo = 120f;
    public float tiempoparallegar;
    public int numRayos = 10;
    public float anguloTotal = 180f;

    public Vector3 velocidad;
    public LayerMask layer;
    
    public int damage = 5;

    //boleanos
    private bool isStunned = false;

    private float stunTimer = 0f;


    public ControlerLinterna linterna;
    public PlayerMovement player;
    public event Action OnStun;
    private void Update()
    {
        VisionPlayer();
    }

    public void VisionPlayer()
    {
        float anguloEntreRayos = anguloTotal / numRayos;

        for (int i = 0; i < numRayos; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * anguloEntreRayos - anguloTotal / 2f, transform.up);
            Vector3 direccionRayo = rotation * transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionRayo, out hit, distanciaVision, layer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Colisión con el jugador");
                    // Lógica adicional aquí
                    Vector3 directionToPlayer = player.transform.position - transform.position;
                    directionToPlayer.y = 0f; // Mantener la rotación solo en el plano horizontal
                    transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);

                }
                // Dibuja el rayo de colisión
                Debug.DrawRay(transform.position, direccionRayo * hit.distance, Color.red);
            }
            else
            {
                // Si no hay colisión, dibuja el rayo hasta la distanciaVision
                Debug.DrawRay(transform.position, direccionRayo * distanciaVision, Color.green);
            }
        }
        /*RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaVision, layer))
        {
            Debug.DrawRay(transform.position, Vector3.forward * distanciaVision, Color.red);

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Colisión con el jugador");

                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);
            }
        }*/
    }
}
