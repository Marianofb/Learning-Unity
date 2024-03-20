using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorControlCombate : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask mascaraEnemigo;
    public LayerMask mascaraObstaculo;

    [Header("Variables")]
    public bool estaAtacando = false;
    private Collider2D[] listaEnemigos;
    private Vector2 zonaAtaque;

    [Header("Puño")]
    public float radio_Puño = 0.1f;
    public float distancia_Puño = 0.3f;
    [Header("Cabeza")]
    public float radio_Cabeza = 0.1f;
    public float distancia_Cabeza = 0.3f;

    [Header("OnDrawGizmos")]
    public float radioEjemplo;
    //Para poder visualizar el radio que voy a querer para los ataques
    public Transform ejemploZonaAtaque;
    float radioZonaAtaque;

    Jugador jugador;

    void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    void Start()
    {
        zonaAtaque = new Vector2();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ejemploZonaAtaque.position, radioEjemplo);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(zonaAtaque, radioZonaAtaque);
    }

    public void AnimationTriggerEvent()
    {
        if (listaEnemigos != null)
        {
            foreach (Collider2D c in listaEnemigos)
            {
                IA iA = c.GetComponent<IA>();
                iA.recibiendoDaño = true;
                iA.iAAnimacion.PlayRecibiendoDaño();
                iA.StateMachine.CambiarEstado(iA.RecibiendoDañoState);
            }

            listaEnemigos = null;
        }
    }

    private void SetListaEnemigosAtacados(float radio)
    {
        listaEnemigos = Physics2D.OverlapCircleAll(zonaAtaque, radio, mascaraEnemigo);
    }

    public void SetZonaAtaquePuño()
    {
        radioZonaAtaque = radio_Puño;
        float angle = Mathf.Atan2(jugador.animacion.GetMouseVector2().y, jugador.animacion.GetMouseVector2().x) * Mathf.Rad2Deg;
        float x = transform.position.x + distancia_Puño * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia_Puño * Mathf.Sin(angle * Mathf.Deg2Rad);

        zonaAtaque.Set(x, y);
        SetListaEnemigosAtacados(radio_Puño);
    }

    public void SetZonaAtaqueCabeza()
    {
        radioZonaAtaque = radio_Cabeza;
        float angle = Mathf.Atan2(jugador.animacion.GetMouseVector2().y, jugador.animacion.GetMouseVector2().x) * Mathf.Rad2Deg;
        float x = transform.position.x + distancia_Cabeza * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia_Cabeza * Mathf.Sin(angle * Mathf.Deg2Rad);

        zonaAtaque.Set(x, y);
        SetListaEnemigosAtacados(radio_Cabeza);
    }

    public bool PresionoTeclaAtaque()
    {
        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1))
        {
            return true;
        }

        return false;
    }

    public void SetEstaAtacando(bool estaAtacando)
    {
        this.estaAtacando = estaAtacando;
    }

    public bool GetEstaAtacando()
    {
        return estaAtacando;
    }


}
