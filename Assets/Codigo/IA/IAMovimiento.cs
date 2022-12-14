using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{   
    [Header ("Camino")]
    public GameObject jugador;
    public Vector3 jugadorPosicion; 
    public IAWaypoints _waypoints;
    public Mate _mate;

    [Header ("Lider")]
    public float velocidadLider;
    GameObject lider;
    
    [Header ("IA")]
    public Vector3 IAPosicion;
    public float velocidad;

    void Start()
    {
        jugador = GameObject.Find("Jugador");
        _waypoints = GetComponent<IAWaypoints>();
        _mate = GetComponent<Mate>();

        lider = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Destroy(lider.GetComponent<Collider>());
        Destroy(lider.GetComponent<MeshRenderer>());
        lider.transform.position = this.transform.position;
        lider.transform.localScale -= new Vector3(0.35f, 0.5f, 0f);
        velocidadLider *= velocidad;
    }

    void FixedUpdate()
    {
        jugadorPosicion = jugador.transform.position;
        IAPosicion = transform.position;

        Mover();
        CaminoLider();
    }

    void Mover()
    {
        _waypoints.SeguirLider(lider, this.gameObject, velocidad);
    }

    void CaminoLider()
    {
       _waypoints.MoverWaypoints(lider, this.gameObject, velocidadLider);
    }
}
