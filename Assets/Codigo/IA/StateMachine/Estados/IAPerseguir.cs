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
        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void Desplazar()
    {
        base.Desplazar();

        if (!iA.iANivelDeteccion.Agro())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }

        iA.iAWPManager.GenerarCamino(iA.gameObject, iA.jugador);
        iA.iAWPManager.SeguirGuia(iA.velocidad, iA.jugador);
        iA.iAAnimacion.SetDireccionObjetivo();
        iA.iAAnimacion.SetCaminar();
    }
}
