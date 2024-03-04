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

    //Nombres de Animaciones 
    private float delay;
    private const string Idle = "Idle";
    private const string Caminar = "Caminar";
    private const string AtaquePuño = "Puño";

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
        SetAxis();
        Direccion();
    }

    public void PlayIdle()
    {
        CambiarAnimacion(Idle);
    }

    public void PlayCaminar()
    {
        CambiarAnimacion(Caminar);
    }

    public void PlayAtaquePuño()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CambiarAnimacion(AtaquePuño);
            Invoke("AtaqueCompleado", 0.5f);
        }
    }

    private void AtaqueCompleado()
    {
        jugador.SetAtacando(false);
    }

    private void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    private void Direccion()
    {
        if (!Mathf.Approximately(xAxis, 0.0f) || !Mathf.Approximately(yAxis, 0.0f))
        {
            rumbo.Set(xAxis, yAxis);
            rumbo.Normalize();
        }

        animador.SetFloat(rumboX, rumbo.x);
        animador.SetFloat(rumboY, rumbo.y);
    }

    private void SetAxis()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
    }

    public void SetAxisAccion()
    {
        animador.SetFloat(accionX, jugadorControlesCombate.GetXAccion());
        animador.SetFloat(accionY, jugadorControlesCombate.GetYAccion());

        //Se mantiene la direccion despues del ataque
        rumbo.x = jugadorControlesCombate.GetXAccion();
        rumbo.y = jugadorControlesCombate.GetYAccion();
    }

}
