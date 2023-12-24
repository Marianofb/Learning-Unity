using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estado
{
    public enum ESTADO
    {
        IDLE, PATRULLAR, PERSEGUIR, ATACAR
    }

    public enum EVENTO
    {
        COMENZAR, PROCESAR, FINALIZAR
    }

    public ESTADO nombre;
    protected EVENTO evento;
    protected GameObject npc;
    protected Animator animador;
    protected Estado sigEstado;
}
