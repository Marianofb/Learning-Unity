using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorStateMachine
{
    public JugadorState EstadoActual { get; set; }

    public void InicializarEstado(JugadorState estadoIncializacion)
    {
        EstadoActual = estadoIncializacion;
        EstadoActual.ActivarEstado();
    }

    public void CambiarEstado(JugadorState nuevoEstado)
    {
        EstadoActual.DesactivarEstado();
        EstadoActual = nuevoEstado;
        EstadoActual.ActivarEstado();
    }
}
