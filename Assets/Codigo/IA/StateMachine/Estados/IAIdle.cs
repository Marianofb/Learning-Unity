using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAIdle : IAState
{
    public IAIdle(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
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

        Debug.Log("HAGO NADA");
        if (iA.EntrarCombate())
        {
            StateMachine.CambiarEstado(iA.PerseguirState);
        }

    }
}
