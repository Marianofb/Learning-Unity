using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorControlMovimiento : MonoBehaviour
{
    private float xAxis;
    private float yAxis;
    private Vector3 direccion;
    private float variacionPosicion;

    //Nombre de Animacion
    private const string AnimIdle = "Idle";
    private const string AnimCaminar = "Caminar";

    Jugador jugador;

    void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    public void Caminar()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direccion.Set(xAxis, yAxis, 0f);
        variacionPosicion = direccion.magnitude;

        transform.position += direccion.normalized * jugador.GetVelocidad() * Time.deltaTime;
    }

    public void PlayIdle()
    {
        jugador.animacion.PlayAnimacion(AnimIdle);
    }

    public void PlayCaminar()
    {
        jugador.animacion.PlayAnimacion(AnimCaminar);
    }

    public bool GetEstaCaminando()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            return true;
        }

        return false;
    }
}
