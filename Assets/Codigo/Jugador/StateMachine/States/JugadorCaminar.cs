public class JugadorCaminar : JugadorState
{
    public JugadorCaminar(Jugador jugador, JugadorStateMachine StateMachine) : base(jugador, StateMachine)
    { }

    public override void ActivarEstado()
    {
        jugador.estadoActual = "CAMINAR";

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        jugador.animacion.PlayCaminar();
        jugador.controlMovimiento.Caminar();

        CambiarEstado();

        base.ActualizarEstado();
    }

    private void CambiarEstado()
    {
        if (!jugador.controlCombate.PresionoTeclaAtaque())
        {
            if (!jugador.controlMovimiento.GetEstaCaminando())
            {
                StateMachine.CambiarEstado(jugador.IdleState);
            }
        }
        else
        {
            StateMachine.CambiarEstado(jugador.AtacarState);
        }
    }
}