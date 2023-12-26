using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateMachine
{
    public IAState EstadoActual { get; set; }

    public void InicializarEstado(IAState estadoIncializacion)
    {
        EstadoActual = estadoIncializacion;
        EstadoActual.ActivarEstado();
    }

    public void CambiarEstado(IAState nuevoEstado)
    {
        EstadoActual.DesactivarEstado();
        EstadoActual = nuevoEstado;
        EstadoActual.ActivarEstado();
    }
}

