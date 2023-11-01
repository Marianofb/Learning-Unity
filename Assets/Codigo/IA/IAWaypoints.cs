using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{
    //Componentes
    GameObject aEstrella;
    GameObject guerrero;
    AEstrella generadorCamino;
    Grid grid;
    Matematica mate;

    //Variables
    public bool seguirJugador = false;
    public float velocidad = 0.5f;
    List<Nodo> camino;
    int contador;
    int distanciaCamino;

    void Start()
    {
        aEstrella = GameObject.Find("AEstrella");
        guerrero = GameObject.Find("Guerrero");

        generadorCamino = aEstrella.GetComponent<AEstrella>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();
    }

    void Update()
    {
        GenerarCamino();
        SeguirCamino();
    }

    void SeguirCamino()
    {
        if (camino != null)
        {
            distanciaCamino = camino.Count;
            contador = 0;
            if (mate.Distancia(transform.position, camino[contador].GetPosicionEscena()) < 3f)
            {
                contador++;
            }

            if (contador < distanciaCamino)
            {
                Vector3 direccion = camino[contador].GetPosicionEscena() - transform.position;
                transform.position += direccion.normalized * velocidad * Time.deltaTime;
            }
        }
    }

    void GenerarCamino()
    {
        if (seguirJugador == true)
        {
            Vector3 posicionJugador = guerrero.transform.position;
            generadorCamino.BusquedaCamino(transform.position, posicionJugador);
            camino = generadorCamino.ConstruirCamino(grid.GetNodo(transform.position), grid.GetNodo(posicionJugador));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (camino != null)
        {
            foreach (Nodo n in camino)
            {
                Gizmos.color = Color.green;
                if (n.GetObstruido())
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
