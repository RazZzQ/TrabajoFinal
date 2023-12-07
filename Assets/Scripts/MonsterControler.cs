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
    public float tiempoStunMaximo = 120f;
    public float tiempoparallegar;
    public int numRayos = 10;
    public float anguloTotal = 180f;
    private float stunTimer = 0f;
    public Vector3 velocidad;
    public LayerMask layer;
    
    public int damage = 5;

    //boleanos
    private bool isStunned = false;
    private bool isPlayerInVision = false;

    //referencias
    public ControlerLinterna linterna;
    public PlayerMovement player;

    //Eventos
    public event Action OnStun;
    
    //lista y nodos
    private Node currentNode;

    public LinkedList linkedList;

    public Transform nodo;
    public Transform nodo1;
    public Transform nodo2;
    public Transform nodo3;
    private void Start()
    {
        linkedList = new LinkedList();

        // Agrega tus nodos aqu�
        linkedList.AddNode(nodo);
        linkedList.AddNode(nodo1);
        linkedList.AddNode(nodo2);
        linkedList.AddNode(nodo3);
    }
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
                    // L�gica de seguimiento
                    Vector3 directionToPlayer = player.transform.position - transform.position;
                    directionToPlayer.y = 0f; // Mantener la rotaci�n solo en el plano horizontal
                    transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocidad, tiempoparallegar);

                }
                // Dibuja el rayo de colisi�n
                Debug.DrawRay(transform.position, direccionRayo * hit.distance, Color.red);
            }
            else
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

                    float distanceToNode = Vector3.Distance(transform.position, currentNode.point.transform.position);
                    if (distanceToNode < 0.1f)
                    {
                        currentNode = currentNode.nextNode;
                    }
                }
                Debug.DrawRay(transform.position, direccionRayo * distanciaVision, Color.green);
            }
        }
    }
}
