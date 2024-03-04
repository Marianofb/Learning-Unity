using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWPManager : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    IAMovimiento iAMovimiento;
    GameObject aEstrella;
    PedidoCaminoManager pedidoCaminoManager;
    Grid grid;
    Matematica mate;

    [Header("Camino")]
    List<Nodo> camino;
    public LayerMask mascaraObstaculo;
    public float precision = 2f;
    public int nodoGuia = 0;
    public int nodoIA = 0;

    [Header("Jugador")]
    public Vector2 direccionObjetivo;
    public Vector2 direccionGuardada;

    [Header("Guia")]
    public float velocidadGuia;
    GameObject guia;

    void Awake()
    {
        SetCamino();
        SetComponentes();
        SetGuia();
    }

    void Update()
    {
        ZonaXYJugador();
    }

    float tiempoEspera = .5f;
    float tiempoUltimo = 0.0f;
    public void GenerarCamino()
    {
        if (Time.time - tiempoUltimo >= tiempoEspera)
        {
            tiempoUltimo = Time.time;
            pedidoCaminoManager.SolicitarCamino(transform.position, iA.jugador.transform.position, ResultadoEncontrarCamino);
            StartCoroutine(EsperaGenenrarCamino());
        }

    }

    private void ResultadoEncontrarCamino(List<Nodo> nuevoCamino, bool encontramosCamino)
    {
        if (encontramosCamino)
        {
            camino = nuevoCamino;
            nodoGuia = 1;
            direccionGuardada = direccionObjetivo;
        }
    }

    IEnumerator EsperaGenenrarCamino()
    {
        yield return new WaitForSeconds(tiempoEspera);
    }

    public void GuiaRecorreCamino()
    {
        if (CaminoVacio())
        {
            nodoGuia = 0;
        }
        else
        {
            int distanciaCamino = camino.Count - 1;
            if (nodoGuia < distanciaCamino)
            {
                if (mate.Distancia(transform.position, camino[nodoGuia].GetPosicionEscena()) < precision)
                {
                    nodoGuia++;
                }
            }

            if (nodoGuia <= distanciaCamino)
            {
                guia.transform.position = Vector2.MoveTowards(guia.transform.position, camino[nodoGuia].GetPosicionEscena(), velocidadGuia * Time.deltaTime);
            }
        }
    }

    public bool LlegueDestino()
    {
        if (!CaminoVacio())
        {
            if (transform.position == camino[camino.Count - 1].GetPosicionEscena())
            {
                return true;
            }
        }

        return false;
    }

    public void ReiniciarPoscionGuia()
    {
        if (!CaminoVacio() && JugadorCambiaZonaXY() || CaminoGuiaBloqueado())
        {
            guia.transform.position = iA.jugador.transform.position;
        }
    }

    private bool JugadorCambiaZonaXY()
    {
        if (direccionObjetivo != direccionGuardada)
        {
            return true;
        }

        return false;
    }

    private bool CaminoGuiaBloqueado()
    {
        Vector2 direccionGuia = guia.transform.position - transform.position;
        float distanciaGuia = Vector2.Distance(transform.position, guia.transform.position);
        if (Physics2D.Raycast(transform.position, direccionGuia, distanciaGuia, mascaraObstaculo))
        {
            return true;
        }

        return false;
    }

    private void ZonaXYJugador()
    {
        Vector2 direccion = iA.jugador.transform.position - transform.position;
        if (direccion.x > 0)
        {
            direccionObjetivo.x = 1;
        }
        else
        {
            direccionObjetivo.x = -1;
        }

        if (direccion.y > 0)
        {
            direccionObjetivo.y = 1;
        }
        else
        {
            direccionObjetivo.y = -1;
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
        if (camino.Count > 3)
        {
            camino.Remove(camino[camino.Count - 1]);
        }
    }

    //GETTERS y SETTERS

    public Vector3 GetPosicionGuia()
    {
        return guia.transform.position;
    }


    public Vector3 GetPoscionUltimoNodo()
    {
        if (!CaminoVacio())
        {
            return camino[camino.Count - 1].GetPosicionEscena();
        }

        return Vector3.zero;
    }

    private void SetCamino()
    {
        camino = new List<Nodo>();
        direccionObjetivo = new Vector2();
    }


    private void SetComponentes()
    {
        iA = GetComponent<IA>();
        iAMovimiento = GetComponent<IAMovimiento>();

        aEstrella = GameObject.Find("AEstrella");
        pedidoCaminoManager = aEstrella.GetComponent<PedidoCaminoManager>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();
    }

    private void SetGuia()
    {
        guia = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        guia.name = "Guia " + this.name;
        guia.transform.position = transform.position;
        guia.transform.localScale = transform.localScale / 2;
        Destroy(guia.GetComponent<Collider>());
        Destroy(guia.GetComponent<MeshRenderer>());
        Destroy(guia.GetComponent<MeshFilter>());
        velocidadGuia = iA.GetVelocidad() * 2f;
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

                if (n == grid.GetNodo(camino[camino.Count - 1].GetPosicionEscena()))
                {
                    Gizmos.color = Color.cyan;
                }

                if (n == grid.GetNodo(iA.jugador.transform.position))
                {
                    Gizmos.color = Color.magenta;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.GetLadoNodo());
            }
        }
    }
}
