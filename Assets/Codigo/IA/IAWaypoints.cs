using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{  
    public Vector3[] camino;
    public Vector3 direccion = new Vector3();
    public float distanciaPuntoSiguiente;
    public int puntoActual;

    Mate _mate;
    Animator _animador;

    void Start()
    {
        _animador = GetComponent<Animator>();
        _mate = GetComponent<Mate>();
    }

    public void MoverWaypoints(GameObject objeto, float velocidad)
    {
        if(puntoActual < camino.Length)
        {
            distanciaPuntoSiguiente = _mate.Distancia(camino[puntoActual], objeto.transform.position);
            
            //Desplazamiento entre puntos
            direccion = camino[puntoActual] - objeto.transform.position;
            objeto.transform.position += direccion.normalized * Time.deltaTime * velocidad;

            direccion.Normalize();
            _animador.SetFloat("Mirar X", direccion.x);
            _animador.SetFloat("Mirar Y", direccion.y);

            //Seleccion de puntos
            if(distanciaPuntoSiguiente < 0.1f) 
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

    public Vector3[] GetWaypoints()
    {
        return camino;
    }

    public void ReiniciarPuntoActual()
    {
        puntoActual = 0;
    }
}
