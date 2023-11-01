using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class MinHeap
{
    Nodo[] items;
    int contador;

    public MinHeap(int tamaño)
    {
        items = new Nodo[tamaño];
        contador = 0;
    }

    public void Intercambiar(int indice, int indice2)
    {
        Nodo aux = items[indice];
        items[indice] = items[indice2];
        items[indice2] = aux;
    }

    public Nodo Raiz()
    {
        if (Vacio())
        {
            throw new InvalidOperationException("El Heap esta vacio");
        }

        return items[0];
    }

    public Nodo Eliminar()
    {
        Nodo raiz = Raiz();
        items[0] = items[contador - 1];
        items[0].SetIndiceHeap(0);
        contador--;
        OrdenarParaAbajo();
        return raiz;
    }

    public void Agregar(Nodo nodo)
    {
        nodo.SetIndiceHeap(contador);
        items[contador] = nodo;
        contador++;
        OrdenarParaArriba();
    }

    public void OrdenarParaArriba()
    {
        int indice = contador - 1;
        while (TienePadre(indice) &&
                GetPadre(indice).CompareTo(items[indice]) > 0)
        {
            Intercambiar(indice, GetIndicePadre(indice));
            indice = GetIndicePadre(indice);
        }
    }

    public void OrdenarParaAbajo()
    {
        int indice = 0;
        while (TieneHijoIzq(indice))
        {
            int indiceNodoMasChico = GetIndiceHijoIzquierdo(indice);
            if (TieneHijoDrch(indice) && GetHijoIzquierdo(indice).CompareTo(GetHijoDerecho(indice)) > 0)
            {
                indiceNodoMasChico = GetIndiceHijoDerecho(indice);
            }

            if (items[indice].CompareTo(items[indiceNodoMasChico]) < 0)
            {
                break;
            }
            else
            {
                Intercambiar(indice, indiceNodoMasChico);
            }

            indice = indiceNodoMasChico;
        }
    }

    public bool Contiene(Nodo n)
    {
        return Equals(n, items[n.GetIndiceHeap()]);
    }

    //GETTERS
    int GetIndiceHijoIzquierdo(int indicePadre)
    {
        return 2 * indicePadre + 1;
    }

    int GetIndiceHijoDerecho(int indicePadre)
    {
        return 2 * indicePadre + 2;
    }

    int GetIndicePadre(int indicePadre)
    {
        return (indicePadre - 1) / 2;
    }

    Nodo GetHijoIzquierdo(int indice)
    {
        return items[GetIndiceHijoIzquierdo(indice)];
    }

    Nodo GetHijoDerecho(int indice)
    {
        return items[GetIndiceHijoDerecho(indice)];
    }

    Nodo GetPadre(int indice)
    {
        return items[GetIndicePadre(indice)];
    }

    //BOLEANS
    bool TieneHijoIzq(int indice)
    {
        return GetIndiceHijoIzquierdo(indice) < contador;
    }

    bool TieneHijoDrch(int indice)
    {
        return GetIndiceHijoDerecho(indice) < contador;
    }

    bool TienePadre(int indice)
    {
        return GetIndicePadre(indice) >= 0;
    }

    public bool Vacio()
    {
        if (contador == 0)
        {
            return true;
        }

        return false;
    }

}
