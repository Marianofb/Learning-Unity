using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AEstrella : MonoBehaviour
{
    Grid grid;
    Matematica mate;

    public GameObject r;
    public GameObject t;
    public List<Nodo> camino = new List<Nodo>();

    void Awake()
    {
        grid = GetComponent<Grid>();
        mate = GetComponent<Matematica>();
    }

    void Update()
    {
        BusquedaCamino(r.transform.position, t.transform.position);
        camino = ConstruirCamino(grid.GetNodo(r.transform.position), grid.GetNodo(t.transform.position));
    }

    void BusquedaCamino(Vector3 i, Vector3 f)
    {
        Nodo inicio = grid.GetNodo(i);
        Nodo fin = grid.GetNodo(f);

        if (!inicio.GetObstruido() && !fin.GetObstruido())
        {
            MinHeap openSet = new MinHeap(grid.GetTamaño());
            HashSet<Nodo> closeSet = new HashSet<Nodo>();

            openSet.Agregar(inicio);
            while (!openSet.Vacio())
            {
                Nodo actual = openSet.Eliminar();
                closeSet.Add(actual);

                if (actual == fin)
                {
                    break;
                }

                foreach (Nodo vecino in grid.GetVecinos(actual.GetPosicionEscena()))
                {
                    if (closeSet.Contains(vecino) || vecino.GetObstruido())
                    {
                        continue;
                    }
                    else
                    {
                        float costoEstimado = actual.GetCostoG() + mate.Distancia(actual.GetPosicionEscena(), vecino.GetPosicionEscena());
                        if (costoEstimado <= vecino.GetCostoG() || !openSet.Contiene(vecino))
                        {
                            vecino.SetCostoG(costoEstimado);
                            vecino.SetHeuristica(mate.Distancia(vecino.GetPosicionEscena(), fin.GetPosicionEscena()));
                            vecino.SetPredecesor(actual);

                            if (!openSet.Contiene(vecino))
                            {
                                //Debug.Log("Agergo Nodo" + actual.GetPosicionEscena());
                                openSet.Agregar(vecino);
                            }
                        }
                    }
                }

            }
        }
        else
        {
            throw new InvalidOperationException("No se puede llegar al destino");
        }
    }

    List<Nodo> ConstruirCamino(Nodo i, Nodo f)
    {
        List<Nodo> posiciones = new List<Nodo>();
        Nodo actual = f;

        while (actual != null && actual != i)
        {
            //Debug.Log(nodoActual.GetIndiceHeap());
            posiciones.Add(actual);
            actual = actual.GetPredecesor();

        }

        if (actual == i)
        {
            // Agrega el nodo inicial al camino
            posiciones.Add(i);
            posiciones.Reverse();
        }
        else
        {
            // No se pudo construir un camino, devuelve una lista vacía o maneja el error de otra manera
            Debug.LogError("No se pudo construir un camino.");
        }

        return posiciones;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Nodo n in camino)
        {
            Gizmos.color = Color.green;
            if (n.GetObstruido())
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
        }
    }
}

