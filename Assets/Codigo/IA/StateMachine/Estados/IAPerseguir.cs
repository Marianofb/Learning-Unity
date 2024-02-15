using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPerseguir : IAState
{
    public IAPerseguir(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    { }

    public override void ActivarEstado()
    {
        iA.estadoActual = "PERSEGUIR";
        iA.iAWPManager.GenerarCamino(iA.gameObject, iA.iAWPManager.jugador);
        iA.iAAnimacion.SetDireccionObjetivo();

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        EstadoIdle();
        EstadoAtacar();
        ObjetivoAvistado();
        ObjetivoPredecirUbicacion();
        CaminarObservar();

        base.ActualizarEstado();
    }

    private void CaminarObservar()
    {
        if (iA.iAWPManager.CaminoVacio() || iA.iAWPManager.LlegueDestino())
        {
            iA.iAAnimacion.PlayIdle();
        }
        else
        {
            iA.iAAnimacion.SetDireccionObjetivo();
            iA.iAAnimacion.PlayCaminar();
            iA.iAWPManager.SeguirGuia(iA.velocidad, iA.iAWPManager.jugador);
        }
    }

    private void ObjetivoAvistado()
    {
        //Dentro del campo de vision
        if (iA.iACampoVision.Detectamos())
        {
            iA.iAWPManager.GenerarCaminoConEspera(iA.gameObject, iA.iAWPManager.jugador);
        }
    }

    private void ObjetivoPredecirUbicacion()
    {
        //Genero un Camino prediciendo/adivinando en que direccion se fue (PODRIA SER POR ZONAS)
        if (iA.iANivelDeteccion.Medio())
        {
            iA.iAWPManager.GenerarCaminoConEspera(iA.gameObject, iA.iAWPManager.jugador);
        }
    }

    private void EstadoAtacar()
    {
        //Atacar
        if (iA.iANivelDeteccion.Agro() && iA.PuedoAtacar())
        {
            StateMachine.CambiarEstado(iA.AtaqueState);
        }
    }

    private void EstadoIdle()
    {
        if (iA.iANivelDeteccion.Nulo() && iA.iAWPManager.LlegueDestino())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }
    }
}
