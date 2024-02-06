using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPerseguir : IAState
{
    public IAPerseguir(IA iA, IAStateMachine StateMachine) : base(iA, StateMachine)
    { }

    public override void ActivarEstado()
    {
        iA.estadoActual = "PERSEGUIR";
        iA.iAAnimacion.PlayCaminar();
        iA.iAWPManager.GenerarCamino(iA.gameObject, iA.iAWPManager.jugador);

        base.ActivarEstado();
    }

    public override void DesactivarEstado()
    {
        base.DesactivarEstado();
    }

    public override void ActualizarEstado()
    {
        if (iA.iANivelDeteccion.Nada())
        {
            StateMachine.CambiarEstado(iA.IdleState);
        }

        //Atacar
        if (iA.iANivelDeteccion.Agro() && iA.PuedoAtacar())
        {
            StateMachine.CambiarEstado(iA.AtaqueState);
        }

        //Dentro del campo de vision
        if (iA.iACampoVision.Detectamos())
        {
            iA.iAWPManager.GenerarCamino(iA.gameObject, iA.iAWPManager.jugador);
        }

        //Genero un Camino prediciendo/adivinando en que direccion se fue (PODRIA SER POR ZONAS)
        if (iA.iANivelDeteccion.Medio())
        {
            iA.iAWPManager.GenerarCamino(iA.gameObject, iA.iAWPManager.jugador);
        }

        iA.iAAnimacion.SetDireccionObjetivo();
        iA.iAWPManager.SeguirGuia(iA.velocidad, iA.iAWPManager.jugador);

        base.ActualizarEstado();
    }
}
