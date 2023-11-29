using UnityEngine;

public class GenericQueue<T> : MonoBehaviour
{
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    private Node Head = null;
    private Node Tail = null;
    private int Length = 0;

    public void Enqueue(T value)
    {
        Node newNode = new Node(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
        }
        Length++;
    }

    public void Dequeue()
    {
        if (Head == null)
        {
            Debug.Log("La cola está vacía.");
            return;
        }

        Debug.Log("Desencolado: " + Head.Value);
        Head = Head.Next;

        if (Head != null)
        {
            Head.Previous = null;
        }

        Length--;
    }

    public void PrintAllNodes()
    {
        Node tmp = Head;
        while (tmp != null)
        {
            Debug.Log(tmp.Value + " ");
            tmp = tmp.Next;
        }
    }
}
