using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node
{
    public Transform point;
    public Node nextNode;
    public Node(Transform point)
    {
        this.point = point;
    }
}

public class LinkedList
{
    public Node head;
    public Node Head;

    public void AddNode(Transform point)
    {
        Node newNode = new Node(point);

        if (head == null)
        {
            head = newNode;
            Head = newNode;
        }
        else
        {
            Head.nextNode = newNode;
            Head = newNode;
        }
    }
}
public class MonsterControler : MonoBehaviour
{
    //variables
    public float distanciaAtaque = 2f;
    public float distanciaVision = 5f;
    public float tiempoparallegar;
    public Vector3 velocidad;
    public LayerMask layer;

    //Vision
    public int numRayos = 10;
    public float anguloTotal = 180f;
    
    //variablesStun
    public float stunTimer = 0f;
    public float stunForTime = 5f;
    public float tiempoStunMaximo = 120f;

    
    //damage
    public int damage = 5;

    //boleanos
    private bool onEnterLintern = false;
    private bool isStunned = false;
    private bool isPlayerInVision = false;

    //referencias
    public ControlerLinterna linterna;
    public PlayerMovement player;
    private Node currentNode;
    public LinkedList linkedList;

    //Eventos
    public event Action OnStun;
    
    //lista y nodos

    public Transform nodo;
    public Transform nodo1;
    public Transform nodo2;
    public Transform nodo3;
    private void Start()
    {
        linkedList = new LinkedList();

        // Agrega tus nodos aquí
        linkedList.AddNode(nodo);
        linkedList.AddNode(nodo1);
        linkedList.AddNode(nodo2);
        linkedList.AddNode(nodo3);
    }
    private void Update()
    {
        if (isPlayerInVision)
        {
            MoveToPlayer();
        }
        else
        {
            MoveToNode();
        }

        VisionPlayer();
    }
    private void MoveToPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0f;
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);
        }
    }
    public void VisionPlayer()
    {
        float anguloEntreRayos = anguloTotal / numRayos;
        bool playerDetected = false;

        for (int i = 0; i < numRayos; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * anguloEntreRayos - anguloTotal / 2f, transform.up);
            Vector3 direccionRayo = rotation * transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionRayo, out hit, distanciaVision, layer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Lógica de seguimiento
                    playerDetected = true;
                    break;
                    /*Vector3 directionToPlayer = player.transform.position - transform.position;
                    directionToPlayer.y = 0f; // Mantener la rotación solo en el plano horizontal
                    transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);*/

                }
                // Dibuja el rayo de colisión
                Debug.DrawRay(transform.position, direccionRayo * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, direccionRayo * distanciaVision, Color.green);
            }
        }
        isPlayerInVision = playerDetected;
    }
    private void MoveToNode()
    {
        if (currentNode == null)
        {
            currentNode = linkedList.head;
        }

        if (currentNode != null)
        {
            Vector3 directionToNode = currentNode.point.transform.position - transform.position;
            directionToNode.y = 0f;
            transform.rotation = Quaternion.LookRotation(directionToNode);
            transform.position = Vector3.SmoothDamp(transform.position, currentNode.point.transform.position, ref velocidad, tiempoparallegar);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            currentNode = currentNode.nextNode;
        }
        if (other.CompareTag("StunMonster"))
        {
            onEnterLintern = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("StunMonster"))
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StunMonster") == false)
        {

        }
    }
}
