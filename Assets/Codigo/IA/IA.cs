using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
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
    private Vector3 direccion;

    [Header("Componentes")]
    IAWPManager iAWPManager;

    void Awake()
    {
        SetComponentes();
    }

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
        iAWPManager.SeguirGuia(velocidad);
    }

    public void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    //GETTERS y SETTERS

    public float GetEstaminaActual()
    {
        return estaminaActual;
    }

    public float GetVelocidad()
    {
        return velocidad;
    }

    private void SetVariablesVidaEstamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }

    public void SetDireccion(float x, float y)
    {
        direccion = new Vector2(x, y);
    }

    private void SetComponentes()
    {
        iAWPManager = GetComponent<IAWPManager>();
    }


    public void SetBloqueo(bool x)
    {
        bloqueo = x;
    }
}

