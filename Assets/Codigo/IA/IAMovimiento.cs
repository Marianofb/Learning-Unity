using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{   
    [Header ("Waypoints")]
    IAWaypoints _waypoints;
    Mate _mate;

    [Header ("Lider")]
    private float velocidadLider = 1f;
    GameObject _lider;
    
    [Header ("IA")]
    public Vector3 IAPosicion;
    public float velocidad;

    void Start()
    {
        _waypoints = GetComponent<IAWaypoints>();
        _mate = GetComponent<Mate>();

        _lider = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        //Destroy(lider.GetComponent<Collider>());
        //Destroy(lider.GetComponent<MeshRenderer>());
        _lider.transform.position = this.transform.position;
        _lider.transform.localScale -= new Vector3(0.35f, 0.5f, 0f);
        velocidadLider *= velocidad;
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        if(!_waypoints.LlegoDestino())
        {
            _waypoints.MoverWaypoints(this.gameObject, this.gameObject, velocidad);
        }
    }
}
