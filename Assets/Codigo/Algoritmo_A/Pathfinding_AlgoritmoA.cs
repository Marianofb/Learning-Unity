using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding_AlgoritmoA : MonoBehaviour
{
    public Transform stalker, objetivo;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {   
        BusquedaCamino(stalker.position, objetivo.position);
    }

    private void BusquedaCamino(Vector2 posicionInicio, Vector2 posicionDestino)
    {
        Nodo nodoInicio = grid.GetNodo(posicionInicio);
        Nodo nodoDestino = grid.GetNodo(posicionDestino);

        if(nodoInicio.caminable && nodoDestino.caminable)
        {
            //List<Nodo> abierto = new List<Nodo>(grid.GetTamaño());
            MinHeap<Nodo> abierto = new MinHeap<Nodo>(grid.GetTamaño());
            HashSet<Nodo> cerrado = new HashSet<Nodo>();
            abierto.Agregar(nodoInicio);

            while(abierto.GetContador() > 0)
            {
                Nodo nodoActual = abierto.EliminarPrimero();
                cerrado.Add(nodoActual);

                //cuando era con lista y no con MinHeap (abierto)
                /*for(int i = 1; i < abierto.Count; i++)
                {
                    if(abierto[i].GetCostoF() < nodoActual.GetCostoF() || 
                        abierto[i].GetCostoF() == nodoActual.GetCostoF() && 
                        abierto[i].costoH < nodoActual.costoH)
                    {
                        nodoActual = abierto[i];
                    }
                }

                abierto.Remove(nodoActual);
                cerrado.Add(nodoActual);*/
            
                if(nodoActual == nodoDestino)
                {   
                    if(grid.mostrarNodos == true)
                    {
                        grid.camino = RastrearCamino(nodoInicio, nodoDestino);
                        grid.SetStalker(nodoInicio.GetPosicionMundo());
                    }
                    return;
                }

                foreach(Nodo vecino in grid.GetVecinos(nodoActual))
                {
                    if(!vecino.caminable || cerrado.Contains(vecino))
                    {
                        continue;
                    }
                    
                    int costoMovimientoAVecino =  nodoActual.costoG + GetDistancia(nodoActual, vecino);
                    
                    if(costoMovimientoAVecino == vecino.costoG || !abierto.Contiene(vecino))
                    {
                        vecino.costoG = costoMovimientoAVecino;
                        vecino.costoH = GetDistancia(vecino, nodoDestino);
                        vecino.padre = nodoActual;
                        
                        if(!abierto.Contiene(vecino))
                        {
                            abierto.Agregar(vecino);
                        }
                        
                    }
                }
            }
        }
    }

    private Vector3[] RastrearCamino(Nodo inicio, Nodo final)
    {
        List<Nodo> nodos = new List<Nodo>();
        Nodo nodoActual = final;

        while(nodoActual != inicio)
        {
            nodos.Add(nodoActual);
            nodoActual = nodoActual.padre;
        }
        Vector3[] waypoints = PasarNodoVector3(nodos);
        waypoints.Reverse();

        return waypoints;
    }

    private Vector3[] PasarNodoVector3(List<Nodo> nodos)
    {
        List<Vector3> waypoints = new List<Vector3>();
        foreach(Nodo n in nodos)
        {
            waypoints.Add(n.GetPosicionMundo());
        }

        return waypoints.ToArray();
    }

    //seria el costo, luego definimos si es G o H, si miramos el nodo hacia el destino o del destino hacia el nodo
    private int GetDistancia(Nodo inicio, Nodo destino)
    {   
        int distanciaX = Mathf.Abs(inicio.gridX - destino.gridX);
        int distanciaY = Mathf.Abs(inicio.gridY - destino.gridY);

        if(distanciaX > distanciaY)
        {
            return 14 * distanciaY + 10 * (distanciaX - distanciaY);
        }
        else
        {
            return 14 * distanciaX + 10 * (distanciaY - distanciaX);
        }
    }
}
