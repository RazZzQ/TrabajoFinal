using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/*public class Node
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
}*/
/*public class PruebaNodos : MonoBehaviour
{
    public Vector3 velocidad;
    public float tiempoparallegar;
    public LinkedList linkedList;

    public event Action onVisionPlayer;

    private Node currentNode;

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
        MoveToNode();
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

            float distanceToNode = Vector3.Distance(transform.position, currentNode.point.transform.position);
            if (distanceToNode < 0.1f)
            {
                currentNode = currentNode.nextNode;
            }
        }
    }
}*/


