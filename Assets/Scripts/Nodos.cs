using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodos : MonoBehaviour
{
    public Lista<Nodos> ListaNodosVecinos;

    void Start()
    {
        Lista<Nodos> ListaNodosVecinos = new Lista<Nodos>();
        ListaNodosVecinos.AddFirst(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Nodos GetNextNode()
    {
        if (ListaNodosVecinos.length > 0)
        {
            int indiceAleatorio = Random.Range(0, ListaNodosVecinos.length);
            return ListaNodosVecinos.GetNextNode(indiceAleatorio).Value;
        }
        else
        {
            return null;
        }
    }
}
