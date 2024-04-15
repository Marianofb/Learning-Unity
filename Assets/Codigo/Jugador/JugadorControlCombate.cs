using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorControlCombate : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask mascaraEnemigo;
    public LayerMask mascaraObstaculo;

    [Header("Enemigos en Zona Ataque")]
    private Collider2D[] listaEnemigos;
    private Vector2 zonaAtaque;

    [Header("Ataque Puño")]
    public string nombre_AtaquePuño = "Puño";
    public float radio_Puño = 0.1f;
    public float distancia_Puño = 0.3f;
    public float duracion_AtaquePuño = 0.25f;
    public int largoCombo_AtaquePuño = 2;
    [Header("AtaqueCabeza")]
    public string nombre_AtaqueCabeza = "Cabeza";
    public float radio_Cabeza = 0.1f;
    public float distancia_Cabeza = 0.3f;
    public float duracion_AtaqueCabeza = 0.4f;
    public int largoCombo_AtaqueCabeza = -1;

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
                iA.SetRecibiendoDaño(true);
                iA.iAAnimacion.PlayRecibiendoDaño();
                iA.StateMachine.CambiarEstado(iA.RecibiendoDañoState);
            }

            listaEnemigos = null;
        }
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

    public void AccionarAtaquePuño()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jugador.SetEstaAtacando(true);
            jugador.animacion.SetDireccionAtaque_Mouse();
            jugador.combos.EjecutarCombo(nombre_AtaquePuño, duracion_AtaquePuño, largoCombo_AtaquePuño);
            SetRadioAtaque(radio_Puño);
        }
    }

    public void AccionarAtaqueCabeza()
    {
        if (Input.GetMouseButtonDown(1))
        {
            jugador.SetEstaAtacando(true);
            jugador.animacion.SetDireccionAtaque_Mouse();
            jugador.combos.EjecutarCombo(nombre_AtaqueCabeza, duracion_AtaqueCabeza, largoCombo_AtaqueCabeza);
            SetRadioAtaque(radio_Cabeza);
        }
    }

    private void SetListaEnemigosAtacados(float radio)
    {
        listaEnemigos = Physics2D.OverlapCircleAll(zonaAtaque, radio, mascaraEnemigo);
    }

    private void SetRadioAtaque(float radio)
    {
        radioZonaAtaque = radio;
        float angle = Mathf.Atan2(jugador.animacion.GetMouseVector().y, jugador.animacion.GetMouseVector().x) * Mathf.Rad2Deg;
        float x = transform.position.x + distancia_Puño * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia_Puño * Mathf.Sin(angle * Mathf.Deg2Rad);

        zonaAtaque.Set(x, y);
        SetListaEnemigosAtacados(radio_Puño);
    }
}
