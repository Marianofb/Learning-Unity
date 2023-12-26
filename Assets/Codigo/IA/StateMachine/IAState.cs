using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAState
{
    protected IA iA;
    protected IAStateMachine StateMachine;

    public IAState(IA iA, IAStateMachine StateMachine)
    {
        this.iA = iA;
        this.StateMachine = StateMachine;
    }

    public virtual void ActivarEstado() { }

    public virtual void DesactivarEstado() { }

    public virtual void Desplazar() { }
}
