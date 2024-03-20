public class JugadorAtacar : JugadorState
{
    public JugadorAtacar(Jugador jugador, JugadorStateMachine StateMachine) : base(jugador, StateMachine)
    { }

    public override void ActivarEstado()
    {
        jugador.estadoActual = "ATACAR";
        AtaquePuño();
        AtaqueCabeza();

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

    private void AtaqueCabeza()
    {
        jugador.animacion.PlayAtaqueCabeza();
    }

    private void CambiarEstado()
    {
        if (!jugador.controlCombate.GetEstaAtacando())
        {
            if (!jugador.controlMovimiento.GetEstaCaminando())
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
