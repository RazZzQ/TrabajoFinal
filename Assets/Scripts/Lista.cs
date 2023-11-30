using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lista<T> : MonoBehaviour
{
    public class Node
    {
        public T Value;
        public Node next;

        public Node(T data)
        {
            this.Value = data;
            this.next = null;
        }
    }
    private Node head;
    public int length;

    public void AddLast(T data)
    {
        Node newNode = new Node(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newNode;
        }

        length++;
    }
    public void AddFirst(T data)
    {
        Node newNode = new Node(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            newNode.next = head;
            head = newNode;
        }

        length++;
    }
    public void AddAtPosition(T data, int position)
    {
        if (position < 0 || position > length)
        {
            Debug.LogError("Invalid position");
            return;
        }

        Node newNode = new Node(data);

        if (position == 0)
        {
            AddFirst(data);
        }
        else if (position == length)
        {
            AddLast(data);
        }
        else
        {
            Node current = head;

            for (int i = 0; i < position - 1; i++)
            {
                current = current.next;
            }

            newNode.next = current.next;
            current.next = newNode;

            length++;
        }
    }
    public void RemoveFirst()
    {
        if (head != null)
        {
            head = head.next;
            length--;
        }
    }
    public void RemoveLast()
    {
        if (head == null)
        {
            return;
        }

        if (head.next == null)
        {
            head = null;
        }
        else
        {
            Node current = head;
            while (current.next.next != null)
            {
                current = current.next;
            }
            current.next = null;
        }

        length--;
    }
    public void RemoveAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Invalid position");
            return;
        }

        if (position == 0)
        {
            RemoveFirst();
        }
        else if (position == length - 1)
        {
            RemoveLast();
        }
        else
        {
            Node current = head;

            for (int i = 0; i < position - 1; i++)
            {
                current = current.next;
            }

            current.next = current.next.next;

            length--;
        }
    }
    public void ModifyAtPosition(T newData, int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Invalid position");
            return;
        }

        Node current = head;

        for (int i = 0; i < position; i++)
        {
            current = current.next;
        }

        current.Value = newData;
    }
    public T FindAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Invalid position");
            return default(T);
        }

        Node current = head;

        for (int i = 0; i < position; i++)
        {
            current = current.next;
        }

        return current.Value;
    }
    public Node GetNextNode(int index)
    {
        Node current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.next;
        }

        return current;
    }
}
