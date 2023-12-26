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

        if (iA.iANivelDeteccion.Agro())
        {
            StateMachine.CambiarEstado(iA.PerseguirState);
        }

        iA.iAAnimacion.SetDireccion();
        iA.iAAnimacion.SetIdle();
    }
}
