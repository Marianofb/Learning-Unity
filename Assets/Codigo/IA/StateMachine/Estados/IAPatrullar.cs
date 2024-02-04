using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPatrullar : IAState
{
    public IAPatrullar(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    {
    }

    public override void ActivarEstado()
    {
        iA.estadoActual = "PATRULLAR";
        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        base.ActualizarEstado();
    }
}
