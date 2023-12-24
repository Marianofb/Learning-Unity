using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaActual;
    public float vidaMax;
    public float velRecuperacionVida;

    [Header("Estamina")]
    public float estaminaActual;
    public float estaminaMax;
    public float velRecuperacionEstamina;

    [Header("Variables Desplazar")]
    public float velocidad;
    public bool bloqueo = false;
    private float xAxis;
    private float yAxis;
    private Vector3 direccion;
    private float variacionPosicion;

    void Start()
    {
        SetVariablesVidaEstamina();
    }

    void Update()
    {
        Desplazar();
    }

    //FUNCIONES 
    private void Desplazar()
    {
        //SetVariables
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direccion.Set(xAxis, yAxis, 0f);
        variacionPosicion = direccion.magnitude;

        //Desplazar
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    public void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    //SETTERS y GETTERS
    public void SetBloqueo(bool x)
    {
        bloqueo = x;
    }

    public float GetVariacionPosicion()
    {
        return variacionPosicion;
    }

    public float GetEstaminaActual()
    {
        return estaminaActual;
    }

    private void SetVariablesVidaEstamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }
}
