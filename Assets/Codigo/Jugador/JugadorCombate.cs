using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCombate : MonoBehaviour
{
    [Header ("Componentes")]
    public Animator animador;
    public JugadorValores valores;
    public JugadorMovimiento movimiento;

    [Header ("AccionBloqueaAccion")]
    public bool espera;

    [Header ("RangoAtaque")]
    public LayerMask mascaraEnemigo; 
    public LayerMask mascaraObstaculo; 
    public float radio;
    public Vector2 direccionMouse = new Vector2();
    public Vector2 ataque = new Vector2();

    void Start()
    {
        animador = GetComponent<Animator>();
        valores = GetComponent<JugadorValores>();
        movimiento =  GetComponent<JugadorMovimiento>();
    }

    void Update()
    {
        Ataques();
        MouseAccion();
        AccionBloqueaAccion();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ataque, radio);
    }

    void Ataques()
    {
        Puño();
        Patada();
    }

    void Puño()
    {
        if(Input.GetMouseButtonDown(0) && valores.getEstaminaTotal() > 2f && espera == false)
        {
            animador.Play("Puño");
            valores.ActualizarEstamina(-25f);
            RangoAtaque(0.25f,0.2f);
        }
    }

    void Patada()
    {
        if(Input.GetMouseButtonDown(1) && valores.getEstaminaTotal() > 5f && espera == false)
        {
            animador.Play("Patada");
            valores.ActualizarEstamina(-35f);
            RangoAtaque(0.25f, 0.4f);
        }
    }

    void MouseAccion()
    {
        //ScreenToWorldPoint --> nos traduce la posicion "pixel" a una posicion de "world space"
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direccionMouse = posicionMouse - transform.position;
        direccionMouse.Normalize();

        if(espera == false)
        {
            animador.SetFloat("Accion X", direccionMouse.x);
            animador.SetFloat("Accion Y", direccionMouse.y);
        }
    }

    void AccionBloqueaAccion()
    {
        if(
            animador.GetCurrentAnimatorStateInfo(0).IsName("Puño") || 
            animador.GetCurrentAnimatorStateInfo(0).IsName("Patada")
            ) 
        {
            espera = true;
        }
        else
        {
            espera = false;
        }
    }

    void RangoAtaque(float valor, float distancia)
    {
        radio = valor;
        float x = animador.GetFloat("Accion X");
        float y = animador.GetFloat("Accion Y");
       
        if(x <= -0.7f)
        {
            ataque.Set(transform.position.x - distancia, transform.position.y);
        }

        if(x >= 0.7f)
        {
            ataque.Set(transform.position.x + distancia, transform.position.y);
        }

        if(y <= -0.7f)
        {
            ataque.Set(transform.position.x, transform.position.y - distancia);
        }

        if(y >= 0.7f)
        {
            ataque.Set(transform.position.x, transform.position.y + distancia);
        }

        Collider2D [] listaEnemigos = Physics2D.OverlapCircleAll(ataque, radio, mascaraEnemigo );
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
