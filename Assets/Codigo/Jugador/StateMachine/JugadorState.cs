using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorState
{
    protected Jugador jugador;
    protected JugadorStateMachine StateMachine;

    public JugadorState(Jugador jugador, JugadorStateMachine StateMachine)
    {
        this.jugador = jugador;
        this.StateMachine = StateMachine;
    }

    public virtual void ActivarEstado() { }

    public virtual void DesactivarEstado() { }

    public virtual void ActualizarEstado() { }
}
