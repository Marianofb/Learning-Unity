using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_AlgoritmoA : MonoBehaviour
{
    Grid grid;

    
    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    public void EncontrarCamino(Vector2 posicionInicio, Vector2 posicionDestino)
    {
        Nodo nodoInicio = grid.GetNodo(posicionInicio);
        Nodo nodoDestino = grid.GetNodo(posicionDestino);

        List<Nodo> abierto = new List<Nodo>();
        List<Nodo> cerrado = new List<Nodo>();

    }
}
