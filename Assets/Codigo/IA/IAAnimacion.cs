using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class IAAnimacion : MonoBehaviour
{
    //Componentes
    private Animator animador;
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
    private const string Idle = "Idle";
    private const string Caminar = "Caminar";

    void Awake()
    {
        SetComponentes();
    }

    public void SetIdle()
    {
        CambiarAnimacion(Idle);
    }

    public void SetCaminar()
    {
        CambiarAnimacion(Caminar);
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
    }

    private void SetComponentes()
    {
        animador = GetComponent<Animator>();
        iAWPManager = GetComponent<IAWPManager>();
    }
}
