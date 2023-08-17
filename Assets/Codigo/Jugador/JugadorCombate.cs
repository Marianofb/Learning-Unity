using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCombate : MonoBehaviour
{
    [Header("Componentes")]
    private JugadorAnimacion jugadorAnimador;
    private JugadorValores jugadorValores;

    [Header("Booleanos")]
    public bool espera;

    [Header("Etiquetas")]
    public LayerMask etiquetaEnemigo;
    public LayerMask etiquetaObstaculo;

    [Header("RangoAtaque")]
    public float radio;
    private Vector2 rango = new Vector2();
    private Vector2 direccionMouse = new Vector2();

    //Nombres de Animaciones de Combate
    private const string jugadorPuño = "Puño";
    private const string jugadorPatada = "Patada";

    //Mouse
    private float xAccion = 0f;
    private float yAccion = 0f;


    void Start()
    {
        jugadorAnimador = GetComponent<JugadorAnimacion>();
        jugadorValores = GetComponent<JugadorValores>();
    }

    void Update()
    {
        MouseAccion();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rango, radio);
    }

    void MouseAccion()
    {
        //ScreenToWorldPoint --> nos traduce la posicion "pixel" a una posicion de "world space"
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direccionMouse = posicionMouse - transform.position;
        direccionMouse.Normalize();

        if (espera == false)
        {
            xAccion = direccionMouse.x;
            yAccion = direccionMouse.y;
        }
    }

    public void RangoAtaque(float valor, float distancia)
    {
        radio = valor;

        if (xAccion <= -0.7f)
        {
            rango.Set(transform.position.x - distancia, transform.position.y);
        }

        if (xAccion >= 0.7f)
        {
            rango.Set(transform.position.x + distancia, transform.position.y);
        }

        if (yAccion <= -0.7f)
        {
            rango.Set(transform.position.x, transform.position.y - distancia);
        }

        if (yAccion >= 0.7f)
        {
            rango.Set(transform.position.x, transform.position.y + distancia);
        }

        Collider2D[] listaEnemigos = Physics2D.OverlapCircleAll(rango, radio, etiquetaEnemigo);
        foreach (Collider2D c in listaEnemigos)
        {
            Debug.Log("Hicimos daño a: " + c.name);
        }
    }

    public bool getEspera()
    {
        return espera;
    }
}
