using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimientoOld : MonoBehaviour
{
    [Header("Desplazamiento")]
    public float velocidad;
    float x;
    float y;

    [Header("Animacion")]
    public Vector2 mirar = new Vector2();

    [Header("Componentes")]
    public Animator animador;
    public JugadorCombate combate;


    void Start()
    {
        animador = GetComponent<Animator>();
        combate = GetComponent<JugadorCombate>();
    }

    void Update()
    {
        Direccion();
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Vector3 direccion = new Vector3(x, y);

        if (combate.getEspera() == false)
        {
            //El transform se normaliza para que cuando presionamos X e Y al mismo tiempo, el jugador no se mueva a mayor velocidad en diagonal
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
        }
    }

    void Direccion()
    {
        //El vector velocidad se actualiza todo el tiempo asi podemos hacer la transicion entre "Caminar" y "Quieto"
        Vector2 velocidad = new Vector2(x, y);
        //El vector mirar se actualiza solo cuando se esta mvoviendo, asi mantenemos la direccion a la que se dirigio
        if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(y, 0.0f))
        {
            mirar.Set(x, y);
            mirar.Normalize();
        }

        if (combate.getEspera() == true)
        {
            mirar.Set(animador.GetFloat("Accion X"), animador.GetFloat("Accion Y"));
        }

        animador.SetFloat("Mirar X", mirar.x);
        animador.SetFloat("Mirar Y", mirar.y);
        animador.SetFloat("Velocidad", velocidad.magnitude);
    }
}
