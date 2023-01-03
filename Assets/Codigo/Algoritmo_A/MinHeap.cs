using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MinHeap<T> where T : IHeapItem<T>
{
    T[] items;
    private int contador;

    public MinHeap(int tamaño)
    {
        items = new T[tamaño];
    }

    public void Agregar(T item)
    {
        item.IndiceHeap = contador;
        items[contador] = item;
        ParaArriba(item);
        contador++;
    }  

    public T EliminarPrimero()
    {
        T primerItem = items[0];
        items[0] = items[contador -1];
        items[0].IndiceHeap = 0;
        ParaAbajo(items[0]);
        contador--;
        return primerItem;
    }

    public T Primero()
    {
        return items[0];
    }

    public bool Contiene(T item)
    {
        return Equals(item, items[item.IndiceHeap]);
    }

    public void ActualizarItem(T item)
    {
        ParaArriba(item);
    }

    public int GetContador()
    {
        return contador;
    }

   private void Intercambiar(T itemA, T itemB)
   {   
        int indiceA = itemA.IndiceHeap;
        int indiceB = itemB.IndiceHeap;

        items[indiceA] = itemB;
        items[indiceB] = itemA;
        itemA.IndiceHeap = indiceB;
        itemB.IndiceHeap = indiceA;
   }

    private void ParaArriba(T item)
    {
        //item < padre     = 1
        //item > padre     = -1
        //item == padre    = 0
        while(TienePadre(item) && item.CompareTo(GetPadre(item)) > 0)
        { 
            Intercambiar(item, GetPadre(item));
        }
    }

    private void ParaAbajo(T item)
    {
        //solo controlamos si tiene un hijo izquierdo porque si no lo tiene tampoco va a tener el derecho
        while(TieneHijoIzq(item))
        {
            int indiceHijoChico = GetHijoIzq(item).IndiceHeap;
            if(TieneHijoDrcho(item))
            {
                if(GetHijoIzq(item).CompareTo(GetHijoDercho(item)) < 0)
                {
                    indiceHijoChico = GetHijoDercho(item).IndiceHeap;
                }
            }

            if(item.CompareTo(items[indiceHijoChico]) < 0)
            {
                Intercambiar(item, items[indiceHijoChico]);
            }
            else
            {
                break;
            }
        }
    }

    private T GetPadre(T item)
    {   
        return items[(item.IndiceHeap - 1) / 2];
    }

    private T GetHijoIzq(T item)
    {
        return items[(item.IndiceHeap * 2) + 1];
    }

    private T GetHijoDercho(T item)
    {
        return items[(item.IndiceHeap * 2) + 2];
    }

    private bool TienePadre(T item)
    {
        if(GetPadre(item) != null)
            return true;

        return false;
    }

    private bool TieneHijoIzq(T item)
    {
        if(GetHijoIzq(item) != null)
            return true;

        return false;
    }

    private bool TieneHijoDrcho(T item)
    {
        if(GetHijoDercho(item) != null)
            return true;

        return false;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int IndiceHeap
    {
        get;
        set;
    }
}