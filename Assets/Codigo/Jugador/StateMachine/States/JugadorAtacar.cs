using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAtacar : JugadorState
{
    public JugadorAtacar(Jugador jugador, JugadorStateMachine StateMachine) : base(jugador, StateMachine)
    { }

    public override void ActivarEstado()
    {
        jugador.estadoActual = "ATACAR";
        jugador.animacion.SetAxisAccion();
        AtaquePuño();

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

    private void AtaquePuño()
    {
        jugador.animacion.PlayAtaquePuño();
    }

    private void CambiarEstado()
    {
        if (!jugador.GetAtacando())
        {
            if (!jugador.EstaCaminando())
            {
                StateMachine.CambiarEstado(jugador.IdleState);
            }
            else
            {
                StateMachine.CambiarEstado(jugador.CaminarState);
            }
        }
    }
}
