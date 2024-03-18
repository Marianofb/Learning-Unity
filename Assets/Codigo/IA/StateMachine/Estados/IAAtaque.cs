using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAtaque : IAState
{
    public IAAtaque(IA iA, IAStateMachine iAStateMachine) : base(iA, iAStateMachine)
    {
    }

    public override void ActivarEstado()
    {
        iA.estadoActual = "ATAQUE";
        iA.iAAnimacion.SetDireccionJugador();
        iA.iAAnimacion.PlayAtaquePuño();

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        if (!iA.EstoyCercaJugador() && !iA.GetRealizandoAtaque())
        {
            StateMachine.CambiarEstado(iA.PerseguirState);
        }

        if (iA.EstoyCercaJugador() && !iA.GetRealizandoAtaque())
        {
            iA.iAAnimacion.SetDireccionJugador();
            iA.iAAnimacion.PlayAtaquePuño();
        }
        base.ActualizarEstado();
    }
}
