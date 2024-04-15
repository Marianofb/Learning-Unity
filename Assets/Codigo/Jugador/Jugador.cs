using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Variables Salud")]
    public float vidaActual;
    public float vidaMax;
    public float velRecuperacionVida;

    [Header("Variables Movimiento")]
    public float velocidad = 2f;
    public float estaminaActual;
    public float estaminaMax;
    public float velRecuperacionEstamina;

    [Header("Variables Combate")]
    public bool estaAtacando;

    [Header("State Machine")]
    public string estadoActual;

    [Header("Scripts")]
    public JugadorAnimacion animacion;
    public JugadorCampoVision campoVision;
    public JugadorControlMovimiento controlMovimiento;
    public JugadorControlCombate controlCombate;
    public JugadorCombos combos;

    //StateMachine
    public JugadorStateMachine StateMachine { get; set; }
    public JugadorIdle IdleState { get; set; }
    public JugadorCaminar CaminarState { get; set; }
    public JugadorAtacar AtacarState { get; set; }

    void Awake()
    {
        SetScripts();
        SetStateMachineStates();
    }

    void Start()
    {
        SetVariables_Vida_Estamina();
        StateMachine.InicializarEstado(IdleState);
    }

    void Update()
    {
        StateMachine.EstadoActual.ActualizarEstado();
    }

    public void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    private void SetVariables_Vida_Estamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }

    public void SetEstaAtacando(bool estaAtacando)
    {
        this.estaAtacando = estaAtacando;
    }

    public bool GetEstaAtacando()
    {
        return estaAtacando;
    }

    public float GetVelocidad()
    {
        return velocidad;
    }

    private void SetScripts()
    {
        animacion = GetComponent<JugadorAnimacion>();
        campoVision = GetComponent<JugadorCampoVision>();
        controlCombate = GetComponent<JugadorControlCombate>();
        controlMovimiento = GetComponent<JugadorControlMovimiento>();
        combos = GetComponent<JugadorCombos>();
    }

    private void SetStateMachineStates()
    {
        StateMachine = new JugadorStateMachine();
        IdleState = new JugadorIdle(this, StateMachine);
        CaminarState = new JugadorCaminar(this, StateMachine);
        AtacarState = new JugadorAtacar(this, StateMachine);
    }
}
