using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Configuracion Grid")]
    public int altoGrid;
    public int anchoGrid;

    [Header("Configuracion Nodo")]
    public float ladoNodo;
    public LayerMask mascaraObstaculo;

    public Nodo[,] red;

    void Awake()
    {
        CrearRed();
    }

    private void CrearRed()
    {
        red = new Nodo[anchoGrid, altoGrid];
        Vector3 posicionEscena_SO = transform.position -
                                    Vector3.right * anchoGrid / 2 -
                                    Vector3.up * altoGrid / 2;
        Debug.Log("Posicion del SO: " + posicionEscena_SO / 2);

        for (int y = 0; y < altoGrid; y++)
        {
            for (int x = 0; x < anchoGrid; x++)
            {
                //divide por 2 porque en vez de estar empezando en SO, empieza desde el centro
                Vector3 posicionNodo = posicionEscena_SO / 2 +
                                                Vector3.right * x * ladoNodo +
                                                Vector3.up * y * ladoNodo;
                //0.05f funciona bien para detectar colisiones
                bool obstruido = Physics2D.OverlapCircle(posicionNodo, 0.05f, mascaraObstaculo);

                red[x, y] = new Nodo(obstruido, posicionNodo);
                //Debug.Log("Posicion del Nodo: " + posicionNodo + "X: " + x + " Y: " + y);
            }
        }
    }

    public Nodo GetNodo(Vector3 posicion)
    {
        Vector3 indice = GetIndice(posicion);

        return red[(int)indice.x, (int)indice.y];
    }

    public List<Nodo> GetVecinos(Vector3 posicion)
    {
        List<Nodo> vecinos = new List<Nodo>();
        Vector3 indice = GetIndice(posicion);

        int xIndice = (int)indice.x;
        int yIndice = (int)indice.y;

        //Empezamos en -1 y termiamos en 1 porque son 3 columnas en total las que pueden contener los vecinos del nodo
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                //No queremos agregar el mismo nodo del que estamos buscando los vecinos
                if (x == 0 & y == 0)
                {
                    continue;
                }
                else
                {
                    int xControl = xIndice + x;
                    int yControl = yIndice + y;
                    if (xControl >= 0 & xControl < anchoGrid & yControl < altoGrid & yControl >= 0)
                    {
                        vecinos.Add(red[xControl, yControl]);
                    }
                }
            }
        }

        return vecinos;
    }

    public Vector3 GetIndice(Vector3 posicion)
    {
        Vector3 posicionEscena_SO = transform.position -
                                    Vector3.right * anchoGrid / 2 -
                                    Vector3.up * altoGrid / 2;
        Vector3 indice = posicion - (posicionEscena_SO / 2);
        float x = indice.x / ladoNodo;
        float y = indice.y / ladoNodo;

        return new Vector3((int)x, (int)y, 0);
    }

    public int GetTamaño()
    {
        return altoGrid * anchoGrid;
    }

    private void OnDrawGizmosSelected()
    {
        if (red != null)
        {
            foreach (Nodo n in red)
            {
                Gizmos.color = Color.white;
                if (n.GetObstruido())
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * ladoNodo);
            }
        }
    }
}