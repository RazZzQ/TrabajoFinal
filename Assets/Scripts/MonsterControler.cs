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
    public Transform nodo4;
    public Transform nodo5;
    public Transform nodo6;
    public Transform nodo7;
    public Transform nodo8;
    public Transform nodo9;
    public Transform nodo10;
    public Transform nodo11;
    public Transform nodo12;
    public Transform nodo13;
    public Transform nodo14;
    public Transform nodo15;
    public Transform nodo16;
    public Transform nodo17;
    public Transform nodo18;
    public Transform nodo19;
    public Transform nodo20;
    public Transform nodo21;
    public Transform nodo22;
    public Transform nodo23;
    public Transform nodo24;
    public Transform nodo25;


    private void Start()
    {
        linkedList = new LinkedList();

        // Agrega tus nodos aquí
        linkedList.AddNode(nodo);
        linkedList.AddNode(nodo1);
        linkedList.AddNode(nodo2);
        linkedList.AddNode(nodo3);
        linkedList.AddNode(nodo4);
        linkedList.AddNode(nodo5);
        linkedList.AddNode(nodo6);
        linkedList.AddNode(nodo7);
        linkedList.AddNode(nodo8);
        linkedList.AddNode(nodo9);
        linkedList.AddNode(nodo10);
        linkedList.AddNode(nodo11);
        linkedList.AddNode(nodo12);
        linkedList.AddNode(nodo13);
        linkedList.AddNode(nodo14);
        linkedList.AddNode(nodo15);
        linkedList.AddNode(nodo16);
        linkedList.AddNode(nodo17);
        linkedList.AddNode(nodo18);
        linkedList.AddNode(nodo19);
        linkedList.AddNode(nodo20);
        linkedList.AddNode(nodo21);
        linkedList.AddNode(nodo22);
        linkedList.AddNode(nodo23);
        linkedList.AddNode(nodo24);
        linkedList.AddNode(nodo25);
    }
    private void Update()
    {
        if (isPlayerInVision)
        {
            if (!isStunned)
            {
                MoveToPlayer();
            }
        }
        else
        {
            if (!isStunned)
            {
                MoveToNode();
            }
        }

        VisionPlayer();

        if (isStunned)
        {
            HandleStun();
        }
    }
    private void HandleStun()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        else
        {
            isStunned = false;

            // Aquí puedes añadir la lógica para que el monstruo se quede parado durante un tiempo aleatorio
            float randomStunDuration = UnityEngine.Random.Range(10f, tiempoStunMaximo);
            StartCoroutine(StunCoroutine(randomStunDuration));

            OnStun?.Invoke();
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        // Lógica para cuando el monstruo deja de estar aturdido
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
            Debug.Log("Colision");
            currentNode = currentNode.nextNode;
        }
        if (other.CompareTag("StunMonster"))
        {
            StartStunTimer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StunMonster"))
        {
            ResetStunTimer();
        }
    }

    private void StartStunTimer()
    {
        stunTimer = stunForTime;
        isStunned = true;
    }

    private void ResetStunTimer()
    {
        stunTimer = 0f;
        isStunned = false;
    }
}
