using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedidoCaminoManager : MonoBehaviour
{
    //Componentes
    AEstrella generadorCamino;

    //Pedido
    Queue<PedidoCamino> colaPedidos = new Queue<PedidoCamino>();
    PedidoCamino pedidoActual = new PedidoCamino();
    bool procesandoPedido = false;

    void Awake()
    {
        SetComponentes();
    }

    public void SolicitarCamino(Vector3 incio, Vector3 fin, Action<List<Nodo>, bool> resultado)
    {
        PedidoCamino nuevoPedido = new PedidoCamino(incio, fin, resultado);
        colaPedidos.Enqueue(nuevoPedido);
        IntentarProcesarSiguientePedido();
    }

    public void IntentarProcesarSiguientePedido()
    {
        if (!procesandoPedido && colaPedidos.Count > 0)
        {
            pedidoActual = colaPedidos.Dequeue();
            procesandoPedido = true;
            generadorCamino.ComenzarBusquedaCamino(pedidoActual.inicio, pedidoActual.fin);
        }
    }

    public void FinalizarProcesarPedido(List<Nodo> camino, bool exito)
    {
        pedidoActual.resultado(camino, exito);
        procesandoPedido = false;
        IntentarProcesarSiguientePedido();
    }

    private void SetComponentes()
    {
        generadorCamino = GetComponent<AEstrella>();
    }

    struct PedidoCamino
    {
        public Vector3 inicio;
        public Vector3 fin;
        public Action<List<Nodo>, bool> resultado;

        public PedidoCamino(Vector3 inicio, Vector3 fin, Action<List<Nodo>, bool> resultado)
        {
            this.inicio = inicio;
            this.fin = fin;
            this.resultado = resultado;
        }
    }
}
