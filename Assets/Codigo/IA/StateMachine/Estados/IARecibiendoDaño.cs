using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IARecibiendoDaño : IAState
{
    public IARecibiendoDaño(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    { }

    public override void ActivarEstado()
    {
        iA.estadoActual = "RECIBIENDO DAÑO";


        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        CambiarEstado();
        base.ActualizarEstado();
    }

    private void CambiarEstado()
    {
        if (!iA.GetRecibiendoDaño())
        {
            if (!iA.EstoyCercaJugador() && !iA.GetRealizandoAtaque())
            {
                StateMachine.CambiarEstado(iA.PerseguirState);
            }

            if (iA.EstoyCercaJugador() && !iA.GetRealizandoAtaque())
            {
                StateMachine.CambiarEstado(iA.AtaqueState);
            }
        }
    }
}
