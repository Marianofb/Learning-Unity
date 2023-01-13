using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPManager : MonoBehaviour
{
    public GameObject jugador;
    public Vector3 jugadorPosicion; 
    public Pathfinding_AlgoritmoA algoritmoA;
    public IAMovimiento iAMovimiento;
    public IAWaypoints iAWaypoints;
    public CampoVision campoVision;

    public Transform seguidor, objetivo;

    void Start()
    {
        algoritmoA = GameObject.Find("Algoritmo*A").GetComponent<Pathfinding_AlgoritmoA>();
        iAMovimiento = gameObject.GetComponent<IAMovimiento>();
        iAWaypoints = gameObject.GetComponent<IAWaypoints>();
        jugador = GameObject.Find("Jugador");

        seguidor = this.gameObject.transform;
    }

    void Update()
    {
        GenerarCamino();
    }

    public void GenerarCamino()
    {
        if (Input.GetKeyDown("p"))
        {
            iAWaypoints.ReiniciarPuntoActual();
            iAWaypoints.SetWaypoints(algoritmoA.BusquedaCamino(seguidor.position, objetivo.position));
        }
    }

    public void EncontramosJugador()
    {

    }
}
