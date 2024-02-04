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

    [Header("Variables")]
    public bool bloqueo = false;
    public float distanciaAtaque = 1f;
    public float velocidad;
    public string estadoActual;

    [Header("Jugador")]
    GameObject jugador;

    [Header("Componentes")]
    public IAWPManager iAWPManager;
    public IANivelDeteccion iANivelDeteccion;
    public IAAnimacion iAAnimacion;

    //StateMachine
    public IAStateMachine StateMachine { get; set; }

    public IAIdle IdleState { get; set; }

    public IAPatrullar PatrullarState { get; set; }

    public IAPerseguir PerseguirState { get; set; }

    public IAAtaque AtaqueState { get; set; }


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
        StateMachine.EstadoActual.ActualizarEstado();
    }

    //FUNCIONES 
    private void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    public bool CercaJugador()
    {
        float distanciaJugador = Vector3.Distance(transform.position, jugador.transform.position);

        if (distanciaJugador <= distanciaAtaque)
        {
            return true;
        }

        return false;
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

    public void SetBloqueo(bool bloqueo)
    {
        this.bloqueo = bloqueo;
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
        jugador = GameObject.Find("Jugador");

        IdleState = new IAIdle(this, StateMachine);
        PatrullarState = new IAPatrullar(this, StateMachine);
        PerseguirState = new IAPerseguir(this, StateMachine);
        AtaqueState = new IAAtaque(this, StateMachine);
    }
}

