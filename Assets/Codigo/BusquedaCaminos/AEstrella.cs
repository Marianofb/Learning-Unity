using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AEstrella : MonoBehaviour
{
    [Header("LayerMask")]
    public LayerMask mascaraObstaculo;

    Grid grid;
    Matematica mate;
    PedidoCaminoManager pedidoCaminoManger;

    void Awake()
    {
        grid = GetComponent<Grid>();
        mate = GetComponent<Matematica>();
        pedidoCaminoManger = GetComponent<PedidoCaminoManager>();
    }


    public void ComenzarBusquedaCamino(Vector3 i, Vector3 f)
    {
        StartCoroutine(BusquedaCamino(i, f));
    }

    IEnumerator BusquedaCamino(Vector3 i, Vector3 f)
    {
        Nodo inicio = grid.GetNodo(i);
        Nodo fin = grid.GetNodo(f);

        bool encontramosCamino = false;
        List<Nodo> nuevoCamino = new List<Nodo>();

        if (!fin.GetObstruido())
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
                    encontramosCamino = true;
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
        //Esperar un frame
        yield return null;
        if (encontramosCamino)
        {
            nuevoCamino = ConstruirCamino(i, f);
        }
        pedidoCaminoManger.FinalizarProcesarPedido(nuevoCamino, encontramosCamino);
    }

    public List<Nodo> ConstruirCamino(Vector3 i, Vector3 f)
    {
        List<Nodo> posiciones = new List<Nodo>();
        Nodo actual = grid.GetNodo(f);
        Nodo origen = grid.GetNodo(i);

        while (actual != null && actual != origen)
        {
            //Debug.Log(nodoActual.GetIndiceHeap());
            posiciones.Add(actual);
            actual = actual.GetPredecesor();

        }

        if (actual == origen)
        {
            // Agrega el nodo inicial al camino
            posiciones.Add(origen);
            posiciones.Reverse();
        }
        else
        {
            // No se pudo construir un camino, devuelve una lista vacía o maneja el error de otra manera
            Debug.LogError("No se pudo construir un camino.");
        }

        return Refinar(posiciones);
    }

    List<Nodo> Refinar(List<Nodo> nodos)
    {
        List<Nodo> refinado = new List<Nodo>();
        HashSet<Nodo> visto = new HashSet<Nodo>();


        //Con esto descartamos los Nodos que no cambian de direccion a su predecesor
        Vector2 direccionAux = Vector2.zero;
        for (int j = 1; j < nodos.Count(); j++)
        {
            //Posiciones
            Vector3 poscionActual = nodos[j].GetPosicionEscena();
            Vector3 poscionPredecesor = nodos[j - 1].GetPosicionEscena();

            Vector2 direccionComparar = new Vector2(poscionActual.x - poscionPredecesor.x, poscionActual.y - poscionPredecesor.y);
            if (direccionComparar == direccionAux)
            {
                nodos.Remove(nodos[j]);
            }

            direccionAux = direccionComparar;
        }

        //En un caso de 3 nodos pegado en L
        //Solo se mantenga las dos puntas de esa L
        //Mientras no haya ni una colision en el medio
        //Asi no permitimos ningun cambio brusco y raro de la direccion que toma
        for (int i = 0; i + 2 < nodos.Count(); i++)
        {
            //Debug.Log("Nodo: " + i + "/// Pos:  " + nodos[i].GetPosicionEscena());
            Nodo padre = nodos[i];
            Nodo hijo = nodos[i + 1];
            Nodo hijoSegundo = nodos[i + 2];

            float distanciaActual = mate.Distancia(padre.GetPosicionEscena(), hijo.GetPosicionEscena()) + mate.Distancia(hijo.GetPosicionEscena(), hijoSegundo.GetPosicionEscena());
            float distanciaSegundoHijo = mate.Distancia(padre.GetPosicionEscena(), hijoSegundo.GetPosicionEscena());

            Vector3 direccionSegundoHijo = hijoSegundo.GetPosicionEscena() - padre.GetPosicionEscena();

            if (!visto.Contains(padre))
                refinado.Add(padre);

            if (distanciaActual > distanciaSegundoHijo &
                !Physics2D.Raycast(transform.position, direccionSegundoHijo, distanciaSegundoHijo, mascaraObstaculo))
            {
                //Debug.Log("Nodo: " + i + "/// Pos:  " + nodos[i].GetPosicionEscena());
                refinado.Add(hijoSegundo);
                visto.Add(hijo);
                visto.Add(hijoSegundo);
            }
            else
            {
                refinado.Add(hijo);
            }
        }

        if (!refinado.Contains(nodos[nodos.Count() - 1]))
        {
            refinado.Add(nodos[nodos.Count() - 1]);
        }

        return refinado;
    }
}

