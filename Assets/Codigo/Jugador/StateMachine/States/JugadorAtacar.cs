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
        jugador.controlCombate.AccionarAtaquePuño();
    }

    private void AtaqueCabeza()
    {
        jugador.controlCombate.AccionarAtaqueCabeza();
    }

    private void CambiarEstado()
    {
        if (!jugador.GetEstaAtacando())
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
