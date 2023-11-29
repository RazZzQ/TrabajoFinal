using UnityEngine;

public class Node<T>
{
    public T data;
    public Node<T> next;

    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}

public class SimpleLinkedList<T> : MonoBehaviour
{
    private Node<T> head;
    private int length;

    // Añadir un elemento al final de la lista
    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newNode;
        }

        length++;
    }

    // Añadir un elemento al inicio de la lista
    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);

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

    // Añadir un elemento en una posición específica de la lista
    public void AddAtPosition(T data, int position)
    {
        if (position < 0 || position > length)
        {
            Debug.LogError("Invalid position");
            return;
        }

        Node<T> newNode = new Node<T>(data);

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
            Node<T> current = head;

            for (int i = 0; i < position - 1; i++)
            {
                current = current.next;
            }

            newNode.next = current.next;
            current.next = newNode;

            length++;
        }
    }

    // Eliminar el primer elemento de la lista
    public void RemoveFirst()
    {
        if (head != null)
        {
            head = head.next;
            length--;
        }
    }

    // Eliminar el último elemento de la lista
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
            Node<T> current = head;
            while (current.next.next != null)
            {
                current = current.next;
            }
            current.next = null;
        }

        length--;
    }

    // Eliminar un elemento en una posición específica de la lista
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
            Node<T> current = head;

            for (int i = 0; i < position - 1; i++)
            {
                current = current.next;
            }

            current.next = current.next.next;

            length--;
        }
    }

    // Modificar el valor de un elemento en una posición específica de la lista
    public void ModifyAtPosition(T newData, int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Invalid position");
            return;
        }

        Node<T> current = head;

        for (int i = 0; i < position; i++)
        {
            current = current.next;
        }

        current.data = newData;
    }

    // Buscar el valor de un elemento en una posición específica de la lista
    public T FindAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            Debug.LogError("Invalid position");
            return default(T);
        }

        Node<T> current = head;

        for (int i = 0; i < position; i++)
        {
            current = current.next;
        }

        return current.data;
    }
}
