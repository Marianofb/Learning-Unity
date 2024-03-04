using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorControlesCombate : MonoBehaviour
{
    [Header("Etiquetas")]
    public LayerMask etiquetaEnemigo;
    public LayerMask etiquetaObstaculo;

    [Header("RangoAtaque")]
    public float radio;
    public Vector2 rango = new Vector2();
    private Vector2 direccionMouse = new Vector2();

    [Header("Mouse")]
    private float xAccion = 0f;
    private float yAccion = 0f;


    void Start()
    {

    }

    void Update()
    {
        DireccionMouse();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rango, radio);
    }

    void DireccionMouse()
    {
        //ScreenToWorldPoint --> nos traduce la posicion "pixel" a una posicion de "world space"
        Vector3 posicionMundoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direccionMouse = posicionMundoMouse - transform.position;
        direccionMouse.Normalize();

        xAccion = direccionMouse.x;
        yAccion = direccionMouse.y;

    }

    public void RangoAtaqueVIEJO(float radio, float distancia)
    {
        this.radio = radio;

        //oeste
        if (xAccion <= -0.7f)
        {
            rango.Set(transform.position.x - distancia, transform.position.y);
        }

        //este
        if (xAccion >= 0.7f)
        {
            rango.Set(transform.position.x + distancia, transform.position.y);
        }

        //sur
        if (yAccion <= -0.7f)
        {
            rango.Set(transform.position.x, transform.position.y - distancia);
        }

        //norte
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

    public void RangoAtaque2(float valor, float distancia)
    {
        radio = valor;

        float angle = Mathf.Atan2(direccionMouse.y, direccionMouse.x) * Mathf.Rad2Deg;

        float x = transform.position.x + distancia * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia * Mathf.Sin(angle * Mathf.Deg2Rad);

        rango.Set(x, y);

        Collider2D[] listaEnemigos = Physics2D.OverlapCircleAll(rango, radio, etiquetaEnemigo);
        foreach (Collider2D c in listaEnemigos)
        {
            Debug.Log("Hicimos daño a: " + c.name);
        }
    }

    public float GetXAccion()
    {
        return xAccion;
    }

    public float GetYAccion()
    {
        return yAccion;
    }
}
