using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class IAAnimacion : MonoBehaviour
{
    //Componentes
    private Animator animador;
    private IA iA;
    private IAWPManager iAWPManager;

    //Direccion
    private float xAxis;
    private float yAxis;
    private Vector2 direccion = new Vector2();

    //Booleans
    bool estaIdle;
    bool estaCaminando;
    bool estaAtacando;
    bool direccionInicializada = false;

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";

    //Nombres de Animaciones 
    private string animacionActual;
    public const string Idle = "Idle";
    public const string Caminar = "Caminar";
    public const string AtaquePuño = "Puño";

    void Awake()
    {
        SetComponentes();
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
        CambiarAnimacion(AtaquePuño);
        iA.SetBloqueo(true);
        float delay = animador.GetCurrentAnimatorStateInfo(0).length;
        Invoke("AtaqueCompleado", delay);
    }

    void AtaqueCompleado()
    {
        iA.SetBloqueo(false);
    }

    private void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    public bool GetAnimacionActual(string nombreAnimacion)
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacion))
            return true;

        return false;
    }

    public float GetDuracionAnimacionActual(string nombreAnimacion)
    {
        AnimatorClipInfo[] clips = animador.GetCurrentAnimatorClipInfo(0);
        float duracion = clips[0].clip.length;
        return duracion;
    }

    public void CambiarAnimacionSuave(string nombreAnimacion)
    {
        animador.CrossFade(nombreAnimacion, 0.1f); // Puedes ajustar el tiempo de transición según tu necesidad
    }

    public void SetDireccion()
    {
        if (animador.GetFloat(rumboX) == 0.0f && animador.GetFloat(rumboY) == 0.0f && direccionInicializada == false)
        {
            Vector2 random = Random.insideUnitCircle - (Vector2)transform.position;

            animador.SetFloat(rumboX, random.x);
            animador.SetFloat(rumboY, random.y);

            direccionInicializada = true;
        }
    }

    public void SetDireccionObjetivo()
    {
        xAxis = iAWPManager.GetPosicionGuia().x - transform.position.x;
        yAxis = iAWPManager.GetPosicionGuia().y - transform.position.y;

        direccion = new Vector2(xAxis, yAxis).normalized;

        animador.SetFloat(rumboX, direccion.x);
        animador.SetFloat(rumboY, direccion.y);
        //Sobreescribir Accion con Direccion 
        //(solo porque estamos usando el mismo Aniamdor para el jugaro y la IA)
        animador.SetFloat("Accion X", direccion.x);
        animador.SetFloat("Accion Y", direccion.y);
    }

    private void SetComponentes()
    {
        animador = GetComponent<Animator>();
        iA = GetComponent<IA>();
        iAWPManager = GetComponent<IAWPManager>();
    }
}
