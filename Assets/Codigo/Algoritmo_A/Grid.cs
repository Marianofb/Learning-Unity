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

    private Nodo[,] red;

    void Start()
    {
        CrearRed();
    }

    private void CrearRed()
    {
        red = new Nodo[anchoGrid, altoGrid];
        Vector3 posicionEscena_SO = transform.position -
                                    Vector3.right * anchoGrid / 2 -
                                    Vector3.up * altoGrid / 2;

        for (int y = 0; y < altoGrid; y++)
        {
            for (int x = 0; x < anchoGrid; x++)
            {
                //divide por 2 porque en vez de estar empezando en SO, empieza desde el centro
                Vector3 posicionEscena = posicionEscena_SO / 2 +
                                                Vector3.right * x * ladoNodo +
                                                Vector3.up * y * ladoNodo;
                //0.05f funciona bien para detectar colisiones
                bool obstruido = Physics2D.OverlapCircle(posicionEscena, 0.05f, mascaraObstaculo);

                red[x, y] = new Nodo(obstruido, posicionEscena);
            }
        }
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