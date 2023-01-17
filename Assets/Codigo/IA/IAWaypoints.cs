using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{  
    public Vector3[] camino;
    public Vector3 direccion = new Vector3();
    public float distanciaPunto;
    public int puntoActual = 0;

    Mate _mate;
    Animator animador;

    void Start()
    {
        _mate = GetComponent<Mate>();
        animador = GetComponent<Animator>();
        
        animador.SetFloat("Mirar X", 1);
        animador.SetFloat("Mirar Y", 0);
    }

    public void MoverWaypoints(GameObject objeto, float velocidad)
    {
        Debug.Log("Punto Actual: " + puntoActual + " camino: " + camino.Length);
        if(camino.Length != 0 && puntoActual >= 0)
        {
            distanciaPunto = _mate.Distancia(camino[puntoActual], objeto.transform.position);
            
            //Desplazamiento entre puntos
            direccion = camino[puntoActual] - objeto.transform.position;
            objeto.transform.position += direccion.normalized * Time.deltaTime * velocidad;

            direccion.Normalize();
            animador.SetFloat("Mirar X", direccion.x);
            animador.SetFloat("Mirar Y", direccion.y);

            //Seleccion de puntos
            if(distanciaPunto < .1f) 
            {
                puntoActual--;
            }
        }
    }

    public bool LlegoDestino()
    {
        if(camino.Length != 0)
        {
            return false;
        }
    
        return true;
    }

    public void SetPuntoActual(Vector3[] camino)
    {
        puntoActual = camino.Length - 1;
    }

    public void SetWaypoints(Vector3[] lista)
    {
        camino = lista;
    }

    public Vector3[] GetWaypoints()
    {
        return camino;
    }
}
