using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWPManager : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    GameObject aEstrella;
    AEstrella generadorCamino;
    PedidoCaminoManager pedidoCaminoManager;
    Grid grid;
    Matematica mate;
    IACampoVision campoVision;

    [Header("Camino")]
    public LayerMask mascaraObstaculo;
    public bool guiaCreaCamino = false;
    List<Nodo> camino;
    int nodoActual = 0;

    [Header("Guia")]
    public float precision = 2f;
    public float velocidadGuia;
    public GameObject guia;

    [Header("Jugador")]
    public Vector2 direccionActual;
    public Vector2 direccionGuardada;
    public GameObject jugador;

    void Awake()
    {
        SetCamino();
        SetComponentes();
        SetGuia();
        SetJugador();
    }

    void Update()
    {
        DireccionDestino();
        JugadorCambiaZonaDireccion();
    }

    float tiempoEspera = 1.0f; // Tiempo de espera en segundos entre generaciones de camino
    float tiempoUltimaGeneracion = 0.0f;
    // Lógica para generar un nuevo camino con un tiempo de espera entre generaciones
    public void GenerarCamino()
    {
        if (Time.time - tiempoUltimaGeneracion >= tiempoEspera)
        {
            tiempoUltimaGeneracion = Time.time;
            pedidoCaminoManager.SolicitarCamino(transform.position, jugador.transform.position, ResultadoEncontrarCamino);
            StartCoroutine(EsperaGenenrarCamino());
        }
    }

    private void ResultadoEncontrarCamino(List<Nodo> nuevoCamino, bool encontramosCamino)
    {
        if (encontramosCamino)
        {
            camino = nuevoCamino;
            nodoActual = 0;
            direccionGuardada = direccionActual;
        }
    }

    // Método auxiliar para restablecer la bandera de generandoCamino después de un tiempo
    IEnumerator EsperaGenenrarCamino()
    {
        yield return new WaitForSeconds(tiempoEspera);
    }

    public void SeguirGuia(float velocidad, GameObject destino)
    {
        GuiaRecorreCamino();
        //Vector3 direccion = guia.transform.position - transform.position;
        //transform.position += direccion.normalized * velocidad * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, guia.transform.position, velocidad * Time.deltaTime);
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
            }

            if (nodoActual <= distanciaCamino)
            {
                //Vector3 direccion = camino[nodoActual].GetPosicionEscena() - guia.transform.position;
                //guia.transform.position += direccion.normalized * velocidadGuia * Time.deltaTime;
                guia.transform.position = Vector2.MoveTowards(guia.transform.position, camino[nodoActual].GetPosicionEscena(), velocidadGuia * Time.deltaTime);

                //Debug.Log("Contador: " + nodoActual + "|| LargoCamino: " + distanciaCamino + "|| Distancia: " + mate.Distancia(guia.transform.position, camino[nodoActual].GetPosicionEscena()));
                //Debug.Log("Contador: " + nodoActual + "|| posicion: " + camino[nodoActual].GetPosicionEscena());
            }
        }
    }

    public bool LlegueDestino()
    {
        //Debug.Log("Pos; " + transform.position + "|| Pos FINAL: " + camino[camino.Count - 1].GetPosicionEscena());

        if (transform.position == camino[camino.Count - 1].GetPosicionEscena())
        {
            return true;
        }

        return false;
    }

    public void JugadorCambiaZonaDireccion()
    {
        if (direccionActual != direccionGuardada)
        {
            guia.transform.position = transform.position;
            CaminoEliminarUltimo();
        }
    }

    private void DireccionDestino()
    {
        Vector2 direccionObjetivo = jugador.transform.position - transform.position;
        if (direccionObjetivo.x > 0)
        {
            direccionActual.x = 1;
        }
        else
        {
            direccionActual.x = -1;
        }

        if (direccionObjetivo.y > 0)
        {
            direccionActual.y = 1;
        }
        else
        {
            direccionActual.y = -1;
        }

    }

    public bool CaminoVacio()
    {
        if (camino.Count == 0)
        {
            return true;
        }

        return false;
    }

    public void CaminoEliminarUltimo()
    {
        if (!CaminoVacio())
        {
            camino.Remove(camino[camino.Count - 1]);
        }
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
        pedidoCaminoManager = aEstrella.GetComponent<PedidoCaminoManager>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();
    }

    private void SetCamino()
    {
        camino = new List<Nodo>();
        direccionActual = new Vector2();
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
                if (n == grid.GetNodo(camino[camino.Count - 1].GetPosicionEscena()))
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
