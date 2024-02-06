using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACampoVision : MonoBehaviour
{
    //Componentes
    private Animator animador;
    private IA iA;

    [Header("LayerMask")]
    public LayerMask mascaraDetectar;
    public LayerMask mascaraObstaculo;

    [Header("Circunferencia")]
    public float radio = 1f;
    public float angulo;
    private float radianes;

    //Rayos
    private Vector2 rayoPositivo;
    private Vector2 rayoNegativo;

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";

    void Start()
    {
        animador = GetComponent<Animator>();
        iA = GetComponent<IA>();
        //Debug.Log("COS: " + Mathf.Cos(radianes) + "//SIN: " + Mathf.Sin(radianes));
    }

    void Update()
    {
        Rayos();
    }

    void OnDrawGizmosSelected()
    {
        //Circulo
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radio);

        //Rayos
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rayoPositivo);
        Gizmos.DrawRay(transform.position, rayoNegativo);
    }

    //Intercambiamos entre Cos y Sin, pro el valor que da cada uno
    //Por ejemplo
    //angulo = 60; COS = 0.5; SIN = 0.8660254
    private void Rayos()
    {
        radianes = angulo * Mathf.Deg2Rad;

        bool controlX = EstaEnRango(animador.GetFloat(rumboX), 0.5f, 1f) || EstaEnRango(animador.GetFloat(rumboX), -1f, -0.5f);
        bool controlY = EstaEnRango(animador.GetFloat(rumboY), 0f, 0.3f) || EstaEnRango(animador.GetFloat(rumboY), -0.3f, 0);

        //Este, Oeste
        if (controlX && controlY)
        {
            float x = Mathf.Cos(radianes);
            float y = Mathf.Sin(radianes);

            if (animador.GetFloat(rumboX) > 0)
            {
                rayoPositivo = new Vector2(x, y) * radio;
                rayoNegativo = new Vector2(x, -y) * radio;
            }
            else
            {
                rayoPositivo = new Vector2(-x, y) * radio;
                rayoNegativo = new Vector2(-x, -y) * radio;
            }
        }

        //Norte, Sur
        if (!controlX && !controlY)
        {
            float x = Mathf.Sin(radianes);
            float y = Mathf.Cos(radianes);

            if (animador.GetFloat(rumboY) > 0)
            {
                rayoPositivo = new Vector2(x, y) * radio;
                rayoNegativo = new Vector2(-x, y) * radio;
            }
            else
            {
                rayoPositivo = new Vector2(x, -y) * radio;
                rayoNegativo = new Vector2(-x, -y) * radio;
            }
        }

        //En la comaparacion tiene que ser contra 0.5f, porque todavia no cambia a la animacion "en diagonal"... 
        //Por ejemplo si estuvieramos comparando con 0f ya esta detectando al NE aunque la animacion este en direccion al ESTE
        //SE, SO, NE, NO
        if (animador.GetFloat(rumboX) != 0 && animador.GetFloat(rumboY) != 0)
        {
            float x = Mathf.Sin(radianes);
            float y = Mathf.Cos(radianes);

            if (animador.GetFloat(rumboX) > 0.5f && animador.GetFloat(rumboY) > 0.5f)
            {
                rayoPositivo = Vector2.right * radio;
                rayoNegativo = new Vector2(-x, y) * radio;
            }

            if (animador.GetFloat(rumboX) > 0.5f && animador.GetFloat(rumboY) < -0.5f)
            {
                rayoPositivo = Vector2.right * radio;
                rayoNegativo = new Vector2(-x, -y) * radio;
            }

            if (animador.GetFloat(rumboX) < -0.5f && animador.GetFloat(rumboY) < -0.5f)
            {
                rayoPositivo = -Vector2.right * radio;
                rayoNegativo = new Vector2(x, -y) * radio;
            }

            if (animador.GetFloat(rumboX) < -0.5f && animador.GetFloat(rumboY) > 0.5f)
            {
                rayoPositivo = -Vector2.right * radio;
                rayoNegativo = new Vector2(x, y) * radio;
            }
        }
    }

    private bool EstaEnRango(float valor, float rangoMinimo, float rangoMaximo)
    {
        return valor >= rangoMinimo && valor <= rangoMaximo;
    }

    public bool Detectamos()
    {
        Collider2D[] objetivos = Physics2D.OverlapCircleAll(transform.position, radio, mascaraDetectar);
        foreach (Collider2D obj in objetivos)
        {
            Vector2 direccion = obj.transform.position - transform.position;
            float distancia = Vector2.Distance(transform.position, obj.transform.position);

            if (Visible(direccion, distancia, mascaraObstaculo))
            {
                return true;
            }
        }

        return false;
    }

    public bool Visible(Vector2 direccion, float distancia, LayerMask obstaculo)
    {
        if (!Physics2D.Raycast(transform.position, direccion, distancia, mascaraObstaculo))
        {
            if (Vector2.Angle(transform.right, direccion) < angulo && animador.GetFloat(rumboX) > 0)
            {
                return true;
            }

            if (Vector2.Angle(-transform.right, direccion) < angulo && animador.GetFloat(rumboX) < 0)
            {
                return true;
            }

            if (Vector2.Angle(transform.up, direccion) < angulo && animador.GetFloat(rumboY) > 0)
            {
                return true;
            }

            if (Vector2.Angle(-transform.up, direccion) < angulo && animador.GetFloat(rumboY) < 0)
            {
                return true;
            }
        }

        return false;
    }

    public float GetRadio()
    {
        return radio;
    }
}
