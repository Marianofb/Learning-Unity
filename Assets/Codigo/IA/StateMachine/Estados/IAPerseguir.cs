using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPerseguir : IAState
{
    public IAPerseguir(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    {
    }

    public override void ActivarEstado()
    {
        iA.estadoActual = "PERSEGUIR";
        iA.iAAnimacion.PlayCaminar();

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        if (iA.iANivelDeteccion.Nada())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }

        if (iA.CercaJugador() && iA.iANivelDeteccion.Agro())
        {
            StateMachine.CambiarEstado(iA.AtaqueState);
        }

        iA.iAWPManager.GenerarCamino(iA.gameObject, iA.iAWPManager.jugador);
        iA.iAAnimacion.SetDireccionObjetivo();
        iA.iAWPManager.SeguirGuia(iA.velocidad, iA.iAWPManager.jugador);

        base.ActualizarEstado();
    }
}
