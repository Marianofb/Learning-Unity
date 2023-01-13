using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{  
    public Vector3[] camino;
    public Vector3 direccion = new Vector2();
    public float distanciaSeguidor;
    public float distanciaWaypoint;
    public int puntoActual;

    Mate _mate;
    Animator _animador;

    void Start()
    {
        _animador = GetComponent<Animator>();
        _mate = GetComponent<Mate>();
    }

    public void MoverWaypoints( GameObject lider, GameObject seguidor, float velocidad)
    {
        if(puntoActual < camino.Length)
        {
            distanciaWaypoint = _mate.Distancia(lider.transform.position, camino[puntoActual]);
            distanciaSeguidor =  _mate.Distancia(lider.transform.position, seguidor.transform.position);

            //Loop
            /*if(_puntoAcutal == waypoints.Length)
            {
                _puntoAcutal = 0;
            }*/
            
            //Desplazamiento entre puntos
            direccion = camino[puntoActual] - lider.transform.position;
            lider.transform.position += direccion.normalized * Time.deltaTime * velocidad;

            _animador.SetFloat("Mirar X", direccion.x);
            _animador.SetFloat("Mirar Y", direccion.y);

            //Seleccion de puntos
            if(distanciaWaypoint < 0.1f && distanciaSeguidor < 2f) 
            {
                puntoActual++;
            }
        }
    }

    public void SeguirLider(GameObject lider, GameObject seguidor, float velocidad)
    {
        Vector3 direccion = lider.transform.position - seguidor.transform.position;
        seguidor.transform.position += direccion.normalized * Time.deltaTime * velocidad;
    }

    public bool LlegoDestino()
    {
        if(puntoActual != camino.Length)
        {
            return false;
        }
    
        return true;
    }

    public void SetWaypoints(Vector3[] lista)
    {
        camino = lista;
    }

    public void ReiniciarPuntoActual()
    {
        puntoActual = 0;
    }
}
