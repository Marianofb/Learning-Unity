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

        Debug.Log("HAGO COSAS");
        iA.Mover();

        if (!iA.EntrarCombate())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }
    }
}
