using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    public bool caminable;
    public Vector2 posicionMundo;

    public Nodo(bool caminable, Vector2 posicionMundo)
    {
        this.caminable = caminable;
        this.posicionMundo = posicionMundo;
    }

    public Vector2 GetPosicionMundo()
    {
        return posicionMundo;
    }
}
