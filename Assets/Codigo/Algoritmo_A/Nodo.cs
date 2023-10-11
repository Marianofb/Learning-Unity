using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    bool obstruido;
    Vector2 posicion;

    public Nodo(bool _obstruido, Vector2 _posicion)
    {
        obstruido = _obstruido;
        posicion = _posicion;
    }

    public bool GetObstruido()
    {
        return obstruido;
    }

    public Vector2 GetPosicionEscena()
    {
        return posicion;
    }

}
