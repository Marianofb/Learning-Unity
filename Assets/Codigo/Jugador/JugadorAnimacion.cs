using UnityEngine;
using UnityEngine.UIElements;

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

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";

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

    public void PlayAnimacion(string nombreAnimacion)
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

        if (!jugador.GetEstaAtacando())
        {
            animador.SetFloat(rumboX, rumbo.x);
            animador.SetFloat(rumboY, rumbo.y);
        }
    }

    public void SetDireccionAtaque_Mouse()
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

    public Vector2 GetMouseVector()
    {
        return mouse;
    }
}
