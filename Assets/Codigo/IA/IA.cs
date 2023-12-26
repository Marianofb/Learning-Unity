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

    [Header("Niveles Deteccion")]
    private string nivelNulo = "Nada";
    private string nivelBajo = "Bajo";
    private string nivelMedio = "Medio";
    private string nivelAlto = "Alto";

    [Header("Componentes")]
    IAWPManager iAWPManager;
    IANivelDeteccion iANivelDeteccion;

    //StateMachine
    public IAStateMachine StateMachine { get; set; }

    public IAIdle IdleState { get; set; }

    public IAPatrullar PatrullarState { get; set; }

    public IAPerseguir PerseguirState { get; set; }


    void Awake()
    {
        SetStateMachineStates();
        SetComponentes();
        SetVariablesVidaEstamina();
    }

    void Start()
    {
        StateMachine.InicializarEstado(IdleState);
    }

    void Update()
    {
        StateMachine.EstadoActual.Desplazar();
        EntrarCombate();

    }

    //FUNCIONES 
    private void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    public bool EntrarCombate()
    {
        Debug.Log("COSO: " + iANivelDeteccion.GetNivelDeteccion());
        if (iANivelDeteccion.GetNivelDeteccion() == nivelAlto)
        {
            Debug.Log("Entrando en combate");
            return true;
        }

        return false;
    }

    public void Mover()
    {
        Debug.Log("MOVEEER");

        iAWPManager.SeguirGuia(velocidad);
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
        iANivelDeteccion = GetComponent<IANivelDeteccion>();
    }

    private void SetStateMachineStates()
    {
        StateMachine = new IAStateMachine();

        IdleState = new IAIdle(this, StateMachine);
        PatrullarState = new IAPatrullar(this, StateMachine);
        PerseguirState = new IAPerseguir(this, StateMachine);
    }
}

