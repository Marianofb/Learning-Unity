using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAnimacion : MonoBehaviour
{
    [Header("Componentes")]
    private Animator animador;
    private JugadorMovimiento jugadorMovimiento;
    private JugadorCombate jugadorCombate;
    private JugadorValores jugadorValores;

    //Axis
    private float xAxis;
    private float yAxis;
    private Vector2 rumbo;

    //Booleanos
    private bool estaIdle;
    private bool estaCaminando;
    private bool estaAtacando;

    //Nombres de Animaciones 
    private string animacionActual;
    private const string jugadorIdle = "Idle";
    private const string jugadorCaminar = "Caminar";
    private const string jugadorPuño = "Puño";

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";


    void Start()
    {
        animador = GetComponent<Animator>();
        jugadorMovimiento = GetComponent<JugadorMovimiento>();
        jugadorCombate = GetComponent<JugadorCombate>();
        jugadorValores = GetComponent<JugadorValores>();

        rumbo = new Vector2();
    }

    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        Direccion();
        Caminar();
        Puño();
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

    void Caminar()
    {
        if (jugadorMovimiento.GetVariacionPosicion() != 0)
        {
            CambiarAnimacion(jugadorCaminar);
            estaCaminando = true;
            estaIdle = false;
        }
        else
        {
            CambiarAnimacion(jugadorIdle);
            estaCaminando = false;
            estaIdle = true;
        }
    }

    void Puño()
    {
        if (Input.GetMouseButtonDown(0) && jugadorValores.getEstaminaTotal() > 2f && estaAtacando == false)
        {
            CambiarAnimacion(jugadorPuño);
            jugadorValores.ActualizarEstamina(-25f);
            jugadorCombate.RangoAtaque(0.25f, 0.2f);
        }
    }

    void EstaAtacando()
    {
        if (GetAnimacionActual(jugadorPuño))
        {
            estaAtacando = true;
        }
        else
        {
            estaAtacando = false;
        }
    }

    public void CambiarAnimacion(String nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    public bool GetAnimacionActual(String nombreAnimacion)
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacion))
            return true;

        return false;
    }
}
