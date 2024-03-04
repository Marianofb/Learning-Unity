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
        iA.iAWPManager.GenerarCamino();
        iA.iAAnimacion.SetDireccionJugador();

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
            iA.iAAnimacion.SetDireccionJugador();
            iA.iAAnimacion.PlayCaminar();
            iA.iAMovimiento.SeguirGuia();
        }
    }

    private void ObjetivoAvistado()
    {
        //Dentro del campo de vision
        if (iA.iACampoVision.Detectamos())
        {
            iA.iAWPManager.ReiniciarPoscionGuia();
            iA.iAWPManager.GenerarCamino();
        }
    }

    private void ObjetivoPredecirUbicacion()
    {
        //Genero un Camino prediciendo/adivinando en que direccion se fue (PODRIA SER POR ZONAS)
        if (iA.iANivelDeteccion.Medio())
        {
            iA.iAWPManager.GenerarCamino();
        }
    }

    private void EstadoAtacar()
    {
        //Atacar
        if (iA.iANivelDeteccion.Agro() && iA.EstoyCercaJugador())
        {
            StateMachine.CambiarEstado(iA.AtaqueState);
        }
    }

    private void EstadoIdle()
    {
        if (iA.iANivelDeteccion.Nulo())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }
    }
}
