using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    [Header("Variables")]
    float costoG;
    float costoF;
    float heuristica;
    int indiceHeap;
    Nodo predecesor;

    [Header("Propiedades")]
    bool obstruido;
    Vector2 posicion;

    public Nodo(bool _obstruido, Vector2 _posicion)
    {
        obstruido = _obstruido;
        posicion = _posicion;
    }

    public int CompareTo(Nodo comparar)
    {
        int resultado = costoF.CompareTo(comparar.GetCostoF());
        if (resultado == 0)
        {
            resultado = heuristica.CompareTo(comparar.GetHeuristica());
        }

        return resultado;
    }

    //GETERS
    public bool GetObstruido()
    {
        return obstruido;
    }

    public Vector2 GetPosicionEscena()
    {
        return posicion;
    }

    public float GetCostoG()
    {
        return costoG;
    }

    public float GetCostoF()
    {
        return costoF;
    }

    public float GetHeuristica()
    {
        return heuristica;
    }

    public Nodo GetPredecesor()
    {
        return predecesor;
    }

    public int GetIndiceHeap()
    {
        return indiceHeap;
    }

    //SETERS
    public void SetCostoG(float x)
    {
        costoG = x;
    }

    public void SetCostoF(float x)
    {
        costoF = x;
    }

    public void SetHeuristica(float x)
    {
        heuristica = x;
    }

    public void SetPredecesor(Nodo n)
    {
        predecesor = n;
    }

    public void SetIndiceHeap(int x)
    {
        indiceHeap = x;
    }

}
