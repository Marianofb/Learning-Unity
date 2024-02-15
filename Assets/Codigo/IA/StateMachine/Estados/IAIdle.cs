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
            iA.iAAnimacion.SetDireccionObjetivo();
        }

        if (iA.iANivelDeteccion.Agro())
        {
            StateMachine.CambiarEstado(iA.PerseguirState);
        }

        iA.iAAnimacion.SetDireccionRandom();

        base.ActualizarEstado();
    }
}
