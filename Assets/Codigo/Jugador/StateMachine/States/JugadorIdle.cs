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
        if (!jugador.controlCombate.PresionoTeclaAtaque())
        {
            if (jugador.controlMovimiento.GetEstaCaminando())
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
