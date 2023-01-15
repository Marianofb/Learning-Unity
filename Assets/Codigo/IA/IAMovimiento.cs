using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{   
    [Header ("Waypoints")]
    IAWaypoints _waypoints;
    WPManager _wPManager;
    Mate _mate;

    [Header ("Lider")]
    //private float velocidadLider = 1f;
    //GameObject _lider;
    
    [Header ("IA")]
    public float velocidad;
    Vector3 direccion;
    GameObject jugador;

    [Header ("Componentes")]
    Animator animador;

    void Start()
    {
        _waypoints = GetComponent<IAWaypoints>();
        _wPManager = GetComponent<WPManager>();
        _mate = GetComponent<Mate>();
        animador = GetComponent<Animator>();

        GenerarLider();
        jugador = GameObject.Find("Jugador");
        animador.SetFloat("Mirar X", 1);
        animador.SetFloat("Mirar Y", 1);
       
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
        if(!_waypoints.LlegoDestino())
        {
            _waypoints.MoverWaypoints(this.gameObject, velocidad);
            direccion = jugador.transform.position - this.gameObject.transform.position;
            direccion.Normalize();

            animador.SetFloat("Mirar X", direccion.x);
            animador.SetFloat("Mirar Y", direccion.y);
        }
    }

    void GenerarLider()
    {
        //_lider = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        //Destroy(lider.GetComponent<Collider>());
        //Destroy(lider.GetComponent<MeshRenderer>());
        //_lider.transform.position = this.transform.position;
        //_lider.transform.localScale -= new Vector3(0.35f, 0.5f, 0f);
        //velocidadLider *= velocidad;
    }
}
