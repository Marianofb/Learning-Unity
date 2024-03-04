using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorIdle : JugadorState
{
    public JugadorIdle(Jugador jugador, JugadorStateMachine StateMachine) : base(jugador, StateMachine)
    { }

    public override void ActivarEstado()
    {
        jugador.estadoActual = "IDLE";
        jugador.animacion.PlayIdle();

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
        if (!jugador.GetAtacando())
        {
            if (jugador.EstaCaminando())
            {
                StateMachine.CambiarEstado(jugador.CaminarState);
            }
        }
        else
        {
            StateMachine.CambiarEstado(jugador.AtacarState);
        }
    }
}
