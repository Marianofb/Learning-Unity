using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraJugador : MonoBehaviour
{
    public Transform jugador;
    public Vector3 offset; 
    public float velocidadCamara;

    void FixedUpdate()
    {
        Vector3 posicionJugador = jugador.position + offset;
        Vector3 posicionCamara = Vector3.Lerp(transform.position, posicionJugador, velocidadCamara * Time.deltaTime);
        transform.position = posicionCamara;
    }
}
