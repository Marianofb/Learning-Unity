using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaActual;
    public float vidaMax;
    public float velRecuperacionVida;

    [Header("Estamina")]
    public float estaminaActual;
    public float estaminaMax;
    public float velRecuperacionEstamina;

    [Header("Estado")]
    public string estadoActual;

    [Header("Scripts")]
    public JugadorAnimacion animacion;
    public JugadorCampoVision campoVision;
    public JugadorControlMovimiento controlMovimiento;
    public JugadorControlCombate controlCombate;

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
        SetVariablesVidaEstamina();
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

    private void SetVariablesVidaEstamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }

    private void SetScripts()
    {
        animacion = GetComponent<JugadorAnimacion>();
        controlCombate = GetComponent<JugadorControlCombate>();
        controlMovimiento = GetComponent<JugadorControlMovimiento>();
    }

    private void SetStateMachineStates()
    {
        StateMachine = new JugadorStateMachine();
        IdleState = new JugadorIdle(this, StateMachine);
        CaminarState = new JugadorCaminar(this, StateMachine);
        AtacarState = new JugadorAtacar(this, StateMachine);
    }
}
