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

    [Header("Variables Movimiento")]
    public float velocidad;
    public bool atacando = false;
    private float xAxis;
    private float yAxis;
    private Vector3 direccion;
    private float variacionPosicion;

    [Header("Estado")]
    public string estadoActual;

    [Header("Componentes")]
    public JugadorAnimacion animacion;

    //StateMachine
    public JugadorStateMachine StateMachine { get; set; }
    public JugadorIdle IdleState { get; set; }
    public JugadorCaminar CaminarState { get; set; }
    public JugadorAtacar AtacarState { get; set; }

    void Awake()
    {
        SetComponentes();
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

    public bool EstaAtacando()
    {
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1))
        {
            return true;
        }

        return false;
    }


    public bool EstaCaminando()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            return true;
        }

        return false;
    }

    public void Caminar()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direccion.Set(xAxis, yAxis, 0f);
        variacionPosicion = direccion.magnitude;

        transform.position += direccion.normalized * velocidad * Time.deltaTime;

    }

    public void ActualizarEstamina(float x)
    {
        estaminaActual -= x;
    }

    //SETTERS y GETTERS
    public void SetAtacando(bool x)
    {
        atacando = x;
    }

    private void SetVariablesVidaEstamina()
    {
        estaminaActual = estaminaMax;
        vidaActual = vidaMax;
    }

    private void SetComponentes()
    {
        animacion = GetComponent<JugadorAnimacion>();
    }

    private void SetStateMachineStates()
    {
        StateMachine = new JugadorStateMachine();

        IdleState = new JugadorIdle(this, StateMachine);
        CaminarState = new JugadorCaminar(this, StateMachine);
        AtacarState = new JugadorAtacar(this, StateMachine);
    }

    public float GetVariacionPosicion()
    {
        return variacionPosicion;
    }

    public float GetEstaminaActual()
    {
        return estaminaActual;
    }

    public bool GetAtacando()
    {
        return atacando;
    }
}
