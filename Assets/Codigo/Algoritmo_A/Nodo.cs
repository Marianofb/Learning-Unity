using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : IHeapItem<Nodo>
{
    public bool caminable;
    public Vector3 posicionMundo;
    public int gridX;
    public int gridY;
    
    //Algoritmo A*
    public int costoG;
    public int costoH;
    public Nodo padre;
    int indiceHeap;

    public Nodo(bool caminable, Vector3 posicionMundo, int x,  int y)
    {
        this.caminable = caminable;
        this.posicionMundo = posicionMundo;
        this.gridX = x;
        this.gridY = y;
    }

    public Vector3 GetPosicionMundo()
    {
        return posicionMundo;
    }

    public int GetCostoF()
    {
        return costoG + costoH;
    }

    public int IndiceHeap
    {
        get
        {
            return indiceHeap;
        }
        set
        {
            indiceHeap = value;
        }
    }

    public int CompareTo(Nodo nodoComparar)
    {
        int comparo = GetCostoF().CompareTo(nodoComparar.GetCostoF());
        if(comparo == 0)
        {
            comparo = costoH.CompareTo(nodoComparar.costoH);
        }

        return -comparo;
    }
}
