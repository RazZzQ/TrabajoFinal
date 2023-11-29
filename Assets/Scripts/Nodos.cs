using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodos : MonoBehaviour
{
    public SimpleLinkedList<Nodos> ListaNodosVecinos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Nodos GetNextNode()
    {
        int index = Random.Range(0, ListaNodosVecinos.length);
        return ListaNodosVecinos.GetNextNode(index);
    }
}
