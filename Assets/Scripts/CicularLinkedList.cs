using UnityEngine;

public class CircularListNode
{
    public GameObject LaberintoPrefab;
    public GameObject CurrentLaberintoInstance;
    public CircularListNode Next;

    public CircularListNode(GameObject laberintoPrefab)
    {
        LaberintoPrefab = laberintoPrefab;
        Next = this;
    }
}
public class CircularList
{
    private CircularListNode head;

    public CircularList()
    {
        head = null;
    }

    public void AddNode(GameObject laberintoPrefab)
    {
        CircularListNode newNode = new CircularListNode(laberintoPrefab);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            CircularListNode lastNode = GetLastNode();
            newNode.Next = head;
            lastNode.Next = newNode;
        }
    }

    private CircularListNode GetLastNode()
    {
        CircularListNode temp = head;
        while (temp.Next != head)
        {
            temp = temp.Next;
        }
        return temp;
    }

    public void RotateList()
    {
        if (head != null)
        {
            head = head.Next;
        }
    }

    public GameObject GetCurrentLaberintoPrefab()
    {
        return head != null ? head.LaberintoPrefab : null;
    }
    public CircularListNode GetCurrentNode()
    {
        return head;
    }
}