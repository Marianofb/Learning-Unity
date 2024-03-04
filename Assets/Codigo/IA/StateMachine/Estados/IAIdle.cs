using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAIdle : IAState
{
    public IAIdle(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    { }

    public override void ActivarEstado()
    {
        iA.estadoActual = "IDLE";
        iA.iAAnimacion.PlayIdle();
        iA.iAAnimacion.SetDireccionIdle();

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        if (iA.iANivelDeteccion.Medio())
        {
            iA.iAAnimacion.SetDireccionJugador();
        }

        if (iA.iANivelDeteccion.Agro())
        {
            StateMachine.CambiarEstado(iA.PerseguirState);
        }

        base.ActualizarEstado();
    }
}
