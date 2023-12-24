using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAnimacion : MonoBehaviour
{
    //Componentes
    private Animator animador;
    private IA iA;
    private IACampoVision iACampoVision;
    private IAWPManager iAWPManager;

    //Direccion
    private float xAxis;
    private float yAxis;
    private Vector2 direccion = new Vector2();

    [Header("Debug")]
    public bool debugLog = false;

    //Booleans
    bool estaIdle;
    bool estaCaminando;
    bool estaAtacando;

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

    void Update()
    {

        ControlarCaminarIdle();
        SetDireccion();
    }

    void ControlarCaminarIdle()
    {
        if (iAWPManager.LlegueDestino())
        {
            CambiarAnimacion(Idle);
            estaCaminando = true;
            estaIdle = false;
        }
        else
        {
            CambiarAnimacion(Caminar);
            estaCaminando = false;
            estaIdle = true;
        }
    }

    public void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    public bool GetAnimacionActual(string nombreAnimacion)
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacion))
            return true;

        return false;
    }

    private void SetDireccion()
    {
        xAxis = iAWPManager.GetPosicionGuia().x - transform.position.x;
        yAxis = iAWPManager.GetPosicionGuia().y - transform.position.y;

        direccion = new Vector2(xAxis, yAxis).normalized;

        animador.SetFloat(rumboX, direccion.x);
        animador.SetFloat(rumboY, direccion.y);

        iA.SetDireccion(xAxis, yAxis);
    }

    private void SetComponentes()
    {
        animador = GetComponent<Animator>();
        iA = GetComponent<IA>();
        iACampoVision = GetComponent<IACampoVision>();
        iAWPManager = GetComponent<IAWPManager>();
    }
}
