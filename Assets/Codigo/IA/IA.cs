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

    [Header("Jugador")]
    public GameObject jugador;

    [Header("Componentes")]
    public IAWPManager iAWPManager;
    public IANivelDeteccion iANivelDeteccion;
    public IAAnimacion iAAnimacion;

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

        jugador = GameObject.Find("Jugador");
        iAWPManager.SetDestino(jugador);
    }

    void Start()
    {
        StateMachine.InicializarEstado(IdleState);
    }

    void Update()
    {
        StateMachine.EstadoActual.Desplazar();
    }

    //FUNCIONES 
    private void ActualizarEstamina(float x)
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

    private void SetComponentes()
    {
        iAAnimacion = GetComponent<IAAnimacion>();
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

