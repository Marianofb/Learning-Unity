using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{   
    [Header ("Waypoints")]
    IAWaypoints _waypoints;
    WPManager _wPManager;
    Mate _mate;

    [Header ("IA")]
    public float velocidad;
    Vector3 direccion;
    GameObject jugador;

    void Start()
    {
        _waypoints = GetComponent<IAWaypoints>();
        _wPManager = GetComponent<WPManager>();
        _mate = GetComponent<Mate>();

        jugador = GameObject.Find("Jugador");  
    }

    void Update()
    {
        _wPManager.GenerarCaminoJugador(this.gameObject.transform, jugador.transform);
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        
            _waypoints.MoverWaypoints(this.gameObject, velocidad);
    }
}
