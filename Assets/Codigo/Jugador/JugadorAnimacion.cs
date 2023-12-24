using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAnimacion : MonoBehaviour
{
    [Header("Componentes")]
    private Animator animador;
    private Jugador jugador;
    private JugadorControlesCombate jugadorControlesCombate;

    //Axis
    private float xAxis;
    private float yAxis;
    private Vector2 rumbo;

    //Booleanos
    public bool debugLog;
    private bool estaIdle;
    private bool estaCaminando;
    private bool estaAtacando;


    //Nombres de Animaciones 
    private string animacionActual;
    private const string Idle = "Idle";
    private const string Caminar = "Caminar";
    private const string Puño = "Puño";

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";
    private const string accionX = "Accion X";
    private const string accionY = "Accion Y";


    void Start()
    {
        animador = GetComponent<Animator>();
        jugador = GetComponent<Jugador>();
        jugadorControlesCombate = GetComponent<JugadorControlesCombate>();
        rumbo = new Vector2();

        debugLog = false;
    }

    void Update()
    {
        //Axis
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        //Animacion
        Direccion();
        CaminarIdle();
        AtaquePuño();

        //Booleano
        EstaAtacando();

        //Debug
        DebugAxisAccion();
    }

    void Direccion()
    {
        if (!Mathf.Approximately(xAxis, 0.0f) || !Mathf.Approximately(yAxis, 0.0f))
        {
            rumbo.Set(xAxis, yAxis);
            rumbo.Normalize();
        }

        animador.SetFloat(rumboX, rumbo.x);
        animador.SetFloat(rumboY, rumbo.y);
    }

    void CaminarIdle()
    {
        if (jugador.GetVariacionPosicion() != 0 && estaAtacando == false)
        {
            CambiarAnimacion(Caminar);
            estaCaminando = true;
            estaIdle = false;
        }
        else
        {
            CambiarAnimacion(Idle);
            estaCaminando = false;
            estaIdle = true;
        }
    }

    void AtaquePuño()
    {
        if (Input.GetMouseButtonDown(0) && jugador.GetEstaminaActual() > 2f && estaAtacando == false)
        {
            CambiarAnimacion(Puño);
            jugador.ActualizarEstamina(-25f);
            jugadorControlesCombate.RangoAtaque2(0.25f, 0.2f);
            SetAxisAccion();
        }
    }

    void EstaAtacando()
    {
        //Esta animacion o mas ||
        if (GetAnimacionActual(Puño))
        {
            estaAtacando = true;
            jugador.SetBloqueo(estaAtacando);
        }
        else
        {
            estaAtacando = false;
            jugador.SetBloqueo(estaAtacando);
        }
    }

    void SetAxisAccion()
    {
        animador.SetFloat(accionX, jugadorControlesCombate.GetXAccion());
        animador.SetFloat(accionY, jugadorControlesCombate.GetYAccion());
    }


    void DebugAxisAccion()
    {
        if (debugLog == true)
        {
            Debug.Log("Valor xAccion:" + jugadorControlesCombate.GetXAccion());
            Debug.Log("Valor yAccion:" + jugadorControlesCombate.GetYAccion());
        }
    }


    public void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    public bool GetAnimacionActual(string nombreAnimacion)
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacion))
            return true;

        return false;
    }

}
