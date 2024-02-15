using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWPManager : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    GameObject aEstrella;
    AEstrella generadorCamino;
    Grid grid;
    Matematica mate;
    IACampoVision campoVision;

    [Header("Camino")]
    public LayerMask mascaraObstaculo;
    public bool guiaCreaCamino = false;
    public Vector2 direccionDestino;
    List<Nodo> camino;
    int nodoActual = 0;

    [Header("Guia")]
    public float precision = 2f;
    public float velocidadGuia;
    public GameObject guia;
    public GameObject jugador;

    void Awake()
    {
        SetCamino();
        SetComponentes();
        SetGuia();
        SetJugador();
    }

    bool generandoCamino = false;
    float tiempoEspera = 1.0f; // Tiempo de espera en segundos entre generaciones de camino
    float tiempoUltimaGeneracion = 0.0f;

    // Lógica para generar un nuevo camino con un tiempo de espera entre generaciones
    public void GenerarCaminoConEspera(GameObject inicio, GameObject destino)
    {
        if (Time.time - tiempoUltimaGeneracion >= tiempoEspera && !generandoCamino)
        {
            generandoCamino = true;
            GenerarCamino(inicio, destino);
            tiempoUltimaGeneracion = Time.time;
            StartCoroutine(ResetearGenerandoCamino());
        }
    }

    // Método auxiliar para restablecer la bandera de generandoCamino después de un tiempo
    IEnumerator ResetearGenerandoCamino()
    {
        yield return new WaitForSeconds(tiempoEspera);
        generandoCamino = false;
    }

    bool si = true;
    public void GenerarCamino(GameObject inicio, GameObject destino)
    {
        //reseteo el contador de camino cada vez que genero otro.
        //if (si)
        //{
        //  si = false;
        nodoActual = 0;
        generadorCamino.BusquedaCamino(inicio.transform.position, destino.transform.position);
        camino = generadorCamino.ConstruirCamino(inicio.transform.position, destino.transform.position);
        //}
    }

    public void SeguirGuia(float velocidad, GameObject destino)
    {
        GuiaRecorreCamino();
        Vector3 direccion = guia.transform.position - transform.position;
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    private void GuiaRecorreCamino()
    {
        if (camino == null)
        {
            nodoActual = 0;
        }
        else
        {
            int distanciaCamino = camino.Count - 1;
            if (nodoActual < distanciaCamino)
            {

                if (mate.Distancia(transform.position, camino[nodoActual].GetPosicionEscena()) < precision)
                {
                    nodoActual++;
                }

                //Debug.Log("Contador: " + nodoActual + "|| LargoCamino: " + distanciaCamino + "|| Distancia: " + mate.Distancia(guia.transform.position, camino[nodoActual].GetPosicionEscena()));
                //Debug.Log("Contador: " + nodoActual + "|| posicion: " + camino[nodoActual].GetPosicionEscena());
            }

            if (nodoActual <= distanciaCamino)
            {
                Vector3 direccion = camino[nodoActual].GetPosicionEscena() - guia.transform.position;
                guia.transform.position += direccion.normalized * velocidadGuia * Time.deltaTime;

                if (guia.transform.position == camino[nodoActual].GetPosicionEscena())
                {
                    guia.transform.position = transform.position;
                }
            }
        }
    }

    public bool LlegueDestino()
    {
        // Debug.Log("Pos Origen; " + transform.position + "|| Pos Destino: " + camino[camino.Count - 1].GetPosicionEscena());

        if (grid.GetNodo(transform.position).GetPosicionEscena() == camino[camino.Count - 1].GetPosicionEscena())
        {
            return true;
        }

        return false;
    }



    public bool CaminoVacio()
    {
        if (camino.Count == 0)
        {
            return true;
        }
        return false;
    }

    //GETTERS y SETTERS
    private void SetGuia()
    {
        guia = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        guia.transform.position = transform.position;
        guia.transform.localScale = transform.localScale / 2;
        Destroy(guia.GetComponent<Collider>());
        Destroy(guia.GetComponent<MeshRenderer>());
        velocidadGuia = iA.GetVelocidad() * 2f;
    }

    private void SetJugador()
    {
        jugador = GameObject.Find("Jugador");
    }

    private void SetComponentes()
    {
        iA = GetComponent<IA>();

        aEstrella = GameObject.Find("AEstrella");
        generadorCamino = aEstrella.GetComponent<AEstrella>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();
    }

    private void SetCamino()
    {
        camino = new List<Nodo>();
        direccionDestino = new Vector2();
    }

    public Vector3 GetPosicionGuia()
    {
        return guia.transform.position;
    }


    public Vector3 GetPosicionObjetivo()
    {
        return jugador.transform.position;
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
                if (n == grid.GetNodo(jugador.transform.position))
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
