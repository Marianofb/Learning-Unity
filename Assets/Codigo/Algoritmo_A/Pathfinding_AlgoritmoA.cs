using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_AlgoritmoA : MonoBehaviour
{
    public Transform buscador, objetivo;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {   
        BusquedaCamino(buscador.position, objetivo.position);
    }

    private void BusquedaCamino(Vector2 posicionInicio, Vector2 posicionDestino)
    {
        Debug.Log(10);
        Nodo nodoInicio = grid.GetNodo(posicionInicio);
        Nodo nodoDestino = grid.GetNodo(posicionDestino);

        //List<Nodo> abierto = new List<Nodo>(grid.GetTamaño());
        MinHeap<Nodo> abierto = new MinHeap<Nodo>(grid.GetTamaño());
        HashSet<Nodo> cerrado = new HashSet<Nodo>();
        abierto.Agregar(nodoInicio);

        while(abierto.GetContador() > 0)
        {
            Debug.Log(20);
            Nodo nodoActual = abierto.EliminarPrimero();
            cerrado.Add(nodoActual);

            //cuando era con lista
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
                grid.camino = RastrearCamino(nodoInicio, nodoDestino);
                return;
            }
            Debug.Log(25);
            foreach(Nodo vecino in grid.GetVecinos(nodoActual))
            {
                Debug.Log(30);
                if(!vecino.caminable || cerrado.Contains(vecino))
                {
                    continue;
                }
                
                int costoMovimientoAVecino =  nodoActual.costoG + GetDistancia(nodoActual, vecino);
                
                if(costoMovimientoAVecino == vecino.costoG || !abierto.Contiene(vecino))
                {
                    Debug.Log(40);
                    vecino.costoG = costoMovimientoAVecino;
                    vecino.costoH = GetDistancia(vecino, nodoDestino);
                    vecino.padre = nodoActual;
                    
                    if(!abierto.Contiene(vecino))
                    {
                        Debug.Log(50);
                        abierto.Agregar(vecino);
                    }
                    
                }
            }
        }
    }

    private List<Nodo> RastrearCamino(Nodo inicio, Nodo final)
    {
        List<Nodo> camino = new List<Nodo>();
        Nodo nodoActual = final;

        while(nodoActual != inicio)
        {
            camino.Add(nodoActual);
            nodoActual = nodoActual.padre;
        }

        camino.Reverse();

        return camino;
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
