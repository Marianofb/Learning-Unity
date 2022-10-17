using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{   
    public GameObject[] waypoints;
    int _puntoAcutal = 0;
    public float velocidadMovimiento = 3f;
    //public float velocidadRotacion = 1f;
    Mate _mate;

    void Start()
    {
        _mate = GetComponent<Mate>();
    }

    public void MoverWaypoints()
    {
        float distancia = _mate.Distancia(transform.position, waypoints[_puntoAcutal].transform.position);

        //Seleccion de puntos
        if(distancia < 1f)
            _puntoAcutal++;
    
        if(_puntoAcutal >= waypoints.Length)
            _puntoAcutal = 0;
        
        //Desplazamiento entre puntos
        Vector3 direccion = waypoints[_puntoAcutal].transform.position - this.transform.position;
        this.transform.position += direccion.normalized * Time.deltaTime * velocidadMovimiento;
    }
}
