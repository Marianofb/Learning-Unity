using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPManager : MonoBehaviour
{
    Pathfinding_AlgoritmoA algoritmoA;
    IAMovimiento iAMovimiento;
    IAWaypoints iAWaypoints;
    CampoVision campoVision;
    Mate mate;

    public Transform seguidor, objetivo;
   
    Animator animador;
    GameObject jugador;
    Vector3 direccion;
    float distancia;

    void Start()
    {
        algoritmoA = GameObject.Find("Algoritmo*A").GetComponent<Pathfinding_AlgoritmoA>();
        iAMovimiento = gameObject.GetComponent<IAMovimiento>();
        iAWaypoints = gameObject.GetComponent<IAWaypoints>();
        campoVision = gameObject.GetComponent<CampoVision>();
        mate = GetComponent<Mate>();

        animador = GetComponent<Animator>();
        seguidor = this.gameObject.transform;
        jugador = GameObject.Find("Jugador");
    }

    void Update()
    {
        GenerarCaminoJugador();
    }

    public void GenerarCaminoJugador()
    {   
        if (campoVision.Localizamos())
        {
            direccion = objetivo.position - seguidor.position;
            direccion.Normalize();

            animador.SetFloat("Mirar X", direccion.x);
            animador.SetFloat("Mirar Y", direccion.y);
            if(!MismoCamino(iAWaypoints.GetWaypoints(), algoritmoA.BusquedaCamino(seguidor.position, objetivo.position)))
            {
                iAWaypoints.ReiniciarPuntoActual();
                iAWaypoints.SetWaypoints(algoritmoA.BusquedaCamino(seguidor.position, objetivo.position));
            }
        }
    }

    public bool MismoCamino(Vector3[] camino, Vector3[] caminoFuturo)
    {
        if(camino.Length != caminoFuturo.Length)
        {
            return false;
        }

        for(int i = 0; i < camino.Length; i++)
        {
            if(camino[i] != caminoFuturo[i])
                return false;
        }

        return true;
    }
}
