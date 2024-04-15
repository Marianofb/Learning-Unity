using UnityEngine;

public class IA : MonoBehaviour
{
    [Header("Vida")]
    public float vidaActual;
    public float vidaMax;
    public float velRecuperacionVida;

    [Header("Movimiento")]
    public float velocidad;

    [Header("Ataque")]
    public float distanciaAtaque = 1f;
    public float estaminaActual;
    public float estaminaMax;
    public float velRecuperacionEstamina;
    bool realizandoAtaque = false;
    bool recibiendoDaño = false;

    [Header("Estado")]
    public string estadoActual;

    [Header("Jugador")]
    public GameObject jugador;
    public LayerMask mascaraJugador;

    [Header("Componentes")]
    public IAMovimiento iAMovimiento;
    public IAWPManager iAWPManager;
    public IACampoVision iACampoVision;
    public IANivelDeteccion iANivelDeteccion;
    public IAAnimacion iAAnimacion;

    //StateMachine
    public IAStateMachine StateMachine { get; set; }
    public IAIdle IdleState { get; set; }
    public IAPatrullar PatrullarState { get; set; }
    public IAPerseguir PerseguirState { get; set; }
    public IAAtaque AtaqueState { get; set; }
    public IARecibiendoDaño RecibiendoDañoState { get; set; }


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
    public bool EstoyCercaJugador()
    {
        float distanciaJugador = Vector3.Distance(transform.position, jugador.transform.position);
        if (distanciaJugador <= distanciaAtaque)
        {
            return true;
        }

        return false;
    }

    //GETTERS y SETTERS
    public bool GetRealizandoAtaque()
    {
        return realizandoAtaque;
    }

    public float GetEstaminaActual()
    {
        return estaminaActual;
    }

    public float GetVelocidad()
    {
        return velocidad;
    }

    public Vector3 GetPosicionJugador()
    {
        return jugador.transform.position;
    }

    public bool GetRecibiendoDaño()
    {
        return recibiendoDaño;
    }

    public void SetBloqueo(bool bloqueo)
    {
        this.realizandoAtaque = bloqueo;
    }

    public void SetRecibiendoDaño(bool recibiendo)
    {
        this.recibiendoDaño = recibiendo;
    }

    private void SetVariablesVidaEstamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }

    private void SetComponentes()
    {
        iAAnimacion = GetComponent<IAAnimacion>();
        iAMovimiento = GetComponent<IAMovimiento>();
        iAWPManager = GetComponent<IAWPManager>();
        iANivelDeteccion = GetComponent<IANivelDeteccion>();
        iACampoVision = GetComponent<IACampoVision>();
    }

    private void SetStateMachineStates()
    {
        StateMachine = new IAStateMachine();
        jugador = GameObject.Find("Jugador");

        IdleState = new IAIdle(this, StateMachine);
        PatrullarState = new IAPatrullar(this, StateMachine);
        PerseguirState = new IAPerseguir(this, StateMachine);
        AtaqueState = new IAAtaque(this, StateMachine);
        RecibiendoDañoState = new IARecibiendoDaño(this, StateMachine);
    }
}

