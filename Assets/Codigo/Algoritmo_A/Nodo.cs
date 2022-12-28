using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    public bool caminable;
    public Vector3 posicionMundo;
    public int gridX;
    public int gridY;
    
    //Algoritmo A*
    public int costoG;
    public int costoH;
    public Nodo padre;

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
}
