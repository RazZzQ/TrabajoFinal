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

    public Vector3 velocidad;
    public LayerMask layer;
    
    public int damage = 5;

    //boleanos
    private bool IsPlayerInVision = false;
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
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaVision, layer))
        {
            Debug.DrawRay(transform.position, Vector3.forward * distanciaVision, Color.red);

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Colisión con el jugador");

                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);
            }
        }
    }
}
