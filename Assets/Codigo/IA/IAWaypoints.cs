using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{   
    public GameObject[] waypoints;
    public float distanciaLiderObjeto;
    public int _puntoAcutal = 0;
    Mate _mate;

    void Start()
    {
        _mate = GetComponent<Mate>();
    }

    public void MoverWaypoints(GameObject lider, GameObject seguidor, float velocidad)
    {
        float distanciaWaypoint = _mate.Distancia(lider.transform.position, waypoints[_puntoAcutal].transform.position);
        float distanciaLiderObjeto =  _mate.Distancia(lider.transform.position, seguidor.transform.position);

        //Seleccion de puntos
        if(distanciaWaypoint < 1f & distanciaLiderObjeto < 2f)  
            _puntoAcutal++;
            
    
        if(_puntoAcutal >= waypoints.Length)
            _puntoAcutal = 0;
        
        //Desplazamiento entre puntos
        Vector3 direccion = waypoints[_puntoAcutal].transform.position - lider.transform.position;
        lider.transform.position += direccion.normalized * Time.deltaTime * velocidad;
    }

    public void SeguirLider(GameObject lider, GameObject seguidor, float velocidad)
    {
        Vector3 direccion = lider.transform.position - seguidor.transform.position;
        seguidor.transform.position += direccion.normalized * Time.deltaTime * velocidad;
    }
}
