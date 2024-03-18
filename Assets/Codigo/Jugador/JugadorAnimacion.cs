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
    private Vector2 mouse;

    //Nombres de Animaciones 
    private const string Idle = "Idle";
    private const string Caminar = "Caminar";
    private const string AtaquePuñoDer = "PuñoDer";
    private const string AtaquePuñoIzq = "PuñoIzq";
    private const string AtaqueCabeza = "Cabeza";

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";
    private const string accionX = "Accion X";
    private const string accionY = "Accion Y";

    //Variablees Puño
    public float duracion_AtaquePuño = 0.4f;
    private bool cambioPuño = false;
    ////Variables Cabeza
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
        SetAxis();
        Direccion();
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
            jugador.SetAtacando(true);
            SetZonaAtaque();
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
            jugador.mouseAtaque.SetZonaAtaquePuño();
            Invoke("AtaqueCompleado", duracion_AtaquePuño);
        }
    }

    public void PlayAtaqueCabeza()
    {
        if (Input.GetMouseButtonDown(1))
        {
            jugador.SetAtacando(true);
            SetZonaAtaque();
            CambiarAnimacion(AtaqueCabeza);
            jugador.mouseAtaque.SetZonaAtaqueCabeza();
            Invoke("AtaqueCompleado", duracion_AtaqueCabeza);
        }
    }

    private void AtaqueCompleado()
    {
        jugador.SetAtacando(false);
    }

    private void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    private void Direccion()
    {
        if (!Mathf.Approximately(xAxis, 0.0f) || !Mathf.Approximately(yAxis, 0.0f))
        {
            rumbo.Set(xAxis, yAxis);
            rumbo.Normalize();
        }

        animador.SetFloat(rumboX, rumbo.x);
        animador.SetFloat(rumboY, rumbo.y);
    }

    public void SetZonaAtaque()
    {
        SetMouseVector2();
        animador.SetFloat(accionX, mouse.x);
        animador.SetFloat(accionY, mouse.y);

        //Mantener la direccion despues del ataque
        rumbo.x = mouse.x;
        rumbo.y = mouse.y;
    }

    private void SetMouseVector2()
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
