using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAWPManager : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    GameObject aEstrella;
    AEstrella generadorCamino;
    Grid grid;
    Matematica mate;

    [Header("Camino")]
    public bool guiaCreaCamino = false;
    List<Nodo> camino;
    int contador = 0;

    [Header("Nodos")]
    GameObject destino;

    [Header("Guia")]
    public float precision = 3f;
    public float velocidadGuia;
    GameObject guia;

    void Start()
    {
        aEstrella = GameObject.Find("AEstrella");
        destino = GameObject.Find("Guerrero");

        iA = GetComponent<IA>();
        generadorCamino = aEstrella.GetComponent<AEstrella>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();

        guia = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        guia.transform.position = transform.position;
        guia.transform.localScale = transform.localScale / 2;
        Destroy(guia.GetComponent<Collider>());
        Destroy(guia.GetComponent<MeshRenderer>());
        velocidadGuia = iA.GetVelocidad() * 2;
    }

    void Update()
    {
        CrearCaminoOrigenDestino();
    }

    public Vector3 GetPosicionGuia()
    {
        return guia.transform.position;
    }

    public void SeguirGuia(float velocidad)
    {
        GuiaRecorreCamino();
        if (grid.GetNodo(transform.position) != grid.GetNodo(destino.transform.position))
        {
            Vector3 direccion = guia.transform.position - transform.position;
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
        }
    }

    public bool LlegueDestino()
    {
        if (grid.GetNodo(transform.position) == grid.GetNodo(destino.transform.position) && camino == null)
        {
            return true;
        }

        return false;
    }

    private void CrearCaminoOrigenDestino()
    {
        if (!LlegueDestino())
        {
            GenerarCamino(guia, destino);
        }
    }

    private void GuiaRecorreCamino()
    {
        if (camino != null)
        {
            int distanciaCamino = camino.Count - 1;
            contador = 0;

            //Debug.Log("Contador: " + contador + "/// LargoCamino: " + distanciaCamino);
            if (contador < distanciaCamino)
            {
                if (mate.Distancia(guia.transform.position, camino[contador].GetPosicionEscena()) < precision)
                {
                    contador++;
                }
            }

            if (contador <= distanciaCamino)
            {
                Vector3 direccion = camino[contador].GetPosicionEscena() - guia.transform.position;
                guia.transform.position += direccion.normalized * velocidadGuia * Time.deltaTime;
            }
        }
    }

    private void GenerarCamino(GameObject inicio, GameObject fin)
    {
        generadorCamino.BusquedaCamino(inicio.transform.position, fin.transform.position);
        camino = generadorCamino.ConstruirCamino(grid.GetNodo(inicio.transform.position), grid.GetNodo(fin.transform.position));
    }

    //GETTERS y SETTERS
    public Vector3 GetCaminoDireccion()
    {
        return camino[contador].GetPosicionEscena();
    }

    private void OnDrawGizmosSelected()
    {
        if (camino != null)
        {
            foreach (Nodo n in camino)
            {
                Gizmos.color = Color.green;
                if (n.GetObstruido())
                {
                    Gizmos.color = Color.red;
                }
                if (n == grid.GetNodo(destino.transform.position))
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
