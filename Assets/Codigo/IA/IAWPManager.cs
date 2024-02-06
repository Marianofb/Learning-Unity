using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAWPManager : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    GameObject aEstrella;
    AEstrella generadorCamino;
    Grid grid;
    Matematica mate;
    IACampoVision campoVision;

    [Header("Camino")]
    public LayerMask mascaraObstaculo;
    public bool guiaCreaCamino = false;
    List<Nodo> camino;
    int nodoActual = 0;

    [Header("Guia")]
    public float precision = 2f;
    public float velocidadGuia;
    GameObject guia;
    public GameObject jugador;

    void Start()
    {
        aEstrella = GameObject.Find("AEstrella");

        iA = GetComponent<IA>();
        generadorCamino = aEstrella.GetComponent<AEstrella>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();
        campoVision = aEstrella.GetComponent<IACampoVision>();

        guia = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        guia.transform.position = transform.position;
        guia.transform.localScale = transform.localScale / 2;
        Destroy(guia.GetComponent<Collider>());
        Destroy(guia.GetComponent<MeshRenderer>());
        velocidadGuia = iA.GetVelocidad() * 2f;

        SetJugador();
    }

    public Vector3 GetPosicionGuia()
    {
        return guia.transform.position;
    }

    public void SeguirGuia(float velocidad, GameObject destino)
    {
        GuiaRecorreCamino();
        if (!LlegueDestino(destino))
        {
            Vector3 direccion = guia.transform.position - transform.position;
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
        }
    }

    private bool LlegueDestino(GameObject destino)
    {
        if (grid.GetNodo(transform.position) == grid.GetNodo(destino.transform.position))
        {
            return true;
        }

        return false;
    }

    private void GuiaRecorreCamino()
    {
        if (camino == null)
        {
            nodoActual = 0;
        }
        else
        {
            int distanciaCamino = camino.Count - 1;
            if (nodoActual < distanciaCamino)
            {
                if (mate.Distancia(camino[nodoActual].GetPosicionEscena(), guia.transform.position) < precision)
                {
                    nodoActual++;
                }

                Debug.Log("Contador: " + nodoActual + "|| LargoCamino: " + distanciaCamino + "|| Distancia: " + mate.Distancia(guia.transform.position, camino[nodoActual].GetPosicionEscena()));
            }

            if (nodoActual <= distanciaCamino)
            {
                Vector3 direccion = camino[nodoActual].GetPosicionEscena() - guia.transform.position;
                guia.transform.position += direccion.normalized * velocidadGuia * Time.deltaTime;
            }
        }
    }

    public void GenerarCamino(GameObject inicio, GameObject destino)
    {
        nodoActual = 0;
        generadorCamino.BusquedaCamino(inicio.transform.position, destino.transform.position);
        camino = generadorCamino.ConstruirCamino(grid.GetNodo(inicio.transform.position), grid.GetNodo(destino.transform.position));
    }

    //GETTERS y SETTERS
    public void SetJugador()
    {
        jugador = GameObject.Find("Jugador");
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
                if (n == grid.GetNodo(jugador.transform.position))
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
