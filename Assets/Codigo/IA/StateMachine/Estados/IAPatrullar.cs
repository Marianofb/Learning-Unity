using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPatrullar : IAState
{
    public IAPatrullar(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    {
    }

    public virtual void ActivarEstado()
    {
        base.ActivarEstado();
    }

    public virtual void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public virtual void Desplazar()
    {
        base.Desplazar();
    }
}
