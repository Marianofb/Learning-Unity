using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANivelDeteccion : MonoBehaviour
{
    [Header("Componentes")]
    IACampoVision iACampoVision;

    [Header("Variables Deteccion")]
    public int velocidadDeteccion = 1;
    public float totalDeteccion = 0;
    public int minTotalDeteccion = 10;
    public int maxTotalDeteccion = 100;

    [Header("Niveles Deteccion")]
    public int nivelDeteccionAlto = 90;
    public int nivelDeteccionMedio = 50;
    public int nivelDeteccionBajo = 30;
    public string nivelDeteccion = "";
    private string nivelNulo = "Nada";
    private string nivelBajo = "Bajo";
    private string nivelMedio = "Medio";
    private string nivelAlto = "Alto";

    // Start is called before the first frame update
    void Awake()
    {
        SetComponentes();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarTotalDeteccion();
        ActualizarNivelDeteccion();
    }

    private void SetComponentes()
    {
        iACampoVision = GetComponent<IACampoVision>();
    }

    public string GetNivelDeteccion()
    {
        return nivelDeteccion;
    }

    //Tambien tiene que cambiar segun la distancia
    private void ActualizarTotalDeteccion()
    {
        if (iACampoVision.Detectamos())
        {
            totalDeteccion += 0.1f * velocidadDeteccion;
        }
        else
        {
            totalDeteccion -= 0.1f;
        }

        totalDeteccion = Mathf.Clamp(totalDeteccion, 0, maxTotalDeteccion);
    }

    public void ActualizarNivelDeteccion()
    {
        string nivel = nivelNulo;
        if (totalDeteccion >= minTotalDeteccion && totalDeteccion <= nivelDeteccionBajo)
        {
            nivel = nivelBajo;
            //Debug.Log("HMMM");
        }
        else if (totalDeteccion > nivelDeteccionBajo && totalDeteccion <= nivelDeteccionMedio)
        {
            nivel = nivelMedio;
            //Debug.Log("HOLA?");
        }
        else if (totalDeteccion > nivelDeteccionMedio && totalDeteccion <= maxTotalDeteccion)
        {
            nivel = nivelAlto;
            //Debug.Log("¡QUIETO!");
        }
        else
        {
            nivel = "CERO";
            //Debug.Log("TODO TRANQUILO");
        }

        nivelDeteccion = nivel;
    }


}
