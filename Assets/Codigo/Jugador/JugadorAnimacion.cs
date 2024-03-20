using UnityEngine;

public class JugadorAnimacion : MonoBehaviour
{
    //Componentes
    private Animator animador;
    private Jugador jugador;

    //Axis
    private float xAxis;
    private float yAxis;
    private Vector2 rumbo;

    //Mouse
    Vector2 mouse;

    //Nombres de Animaciones 
    private const string Idle = "Idle";
    private const string Caminar = "Caminar";
    private const string AtaquePuñoDer = "PuñoDer";
    private const string AtaquePuñoIzq = "PuñoIzq";
    private const string AtaqueCabeza = "Cabeza";

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";

    [Header("Ataque Puño")]
    public float duracion_AtaquePuño = 0.4f;
    private bool cambioPuño = false;
    [Header("AtaqueCabeza")]
    public float duracion_AtaqueCabeza = 0.4f;

    void Start()
    {
        animador = GetComponent<Animator>();
        jugador = GetComponent<Jugador>();

        rumbo = new Vector2();
        mouse = new Vector2();
    }

    void Update()
    {
        SetDireccionCaminar();
    }

    public void PlayIdle()
    {
        CambiarAnimacion(Idle);
    }

    public void PlayCaminar()
    {
        CambiarAnimacion(Caminar);
    }

    public void PlayAtaquePuño()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jugador.controlCombate.SetEstaAtacando(true);
            SetDireccionAtaque();
            if (!cambioPuño)
            {
                CambiarAnimacion(AtaquePuñoDer);
                cambioPuño = !cambioPuño;
            }
            else
            {
                CambiarAnimacion(AtaquePuñoIzq);
                cambioPuño = !cambioPuño;
            }
            jugador.controlCombate.SetZonaAtaquePuño();
            Invoke("AtaqueCompleado", duracion_AtaquePuño);
        }
    }

    public void PlayAtaqueCabeza()
    {
        if (Input.GetMouseButtonDown(1))
        {
            jugador.controlCombate.SetEstaAtacando(true);
            SetDireccionAtaque();
            CambiarAnimacion(AtaqueCabeza);
            jugador.controlCombate.SetZonaAtaqueCabeza();
            Invoke("AtaqueCompleado", duracion_AtaqueCabeza);
        }
    }

    private void AtaqueCompleado()
    {
        jugador.controlCombate.SetEstaAtacando(false);
    }

    private void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    private void SetDireccionCaminar()
    {
        SetAxis();
        if (!Mathf.Approximately(xAxis, 0.0f) || !Mathf.Approximately(yAxis, 0.0f))
        {
            rumbo.Set(xAxis, yAxis);
            rumbo.Normalize();
        }

        if (!jugador.controlCombate.GetEstaAtacando())
        {
            animador.SetFloat(rumboX, rumbo.x);
            animador.SetFloat(rumboY, rumbo.y);
        }
    }

    public void SetDireccionAtaque()
    {
        SetVectorMouse();
        animador.SetFloat(rumboX, mouse.x);
        animador.SetFloat(rumboY, mouse.y);

        //Mantener la direccion despues del ataque
        rumbo.Set(mouse.x, mouse.y);
    }

    private void SetVectorMouse()
    {
        //ScreenToWorldPoint --> nos traduce la posicion "pixel" a una posicion de "world space"
        Vector3 posicionMundoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse = posicionMundoMouse - transform.position;
        mouse.Normalize();
    }

    private void SetAxis()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
    }

    public Vector2 GetMouseVector2()
    {
        return mouse;
    }
}
