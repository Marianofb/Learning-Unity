using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{   
    [Header ("Jugador")]
    GameObject jugador;
    IAWaypoints _waypoints;
    Vector3 jugadorPosicion; 

    [Header ("IA")]
    Vector3 IAPosicion;

    void Start()
    {
        jugador = GameObject.Find("Jugador");
        _waypoints = GetComponent<IAWaypoints>();
    }

    void FixedUpdate()
    {
        jugadorPosicion = jugador.transform.position;
        IAPosicion = transform.position;

        Mover();
    }

    void Mover()
    {
        _waypoints.MoverWaypoints();
    }

   
}
