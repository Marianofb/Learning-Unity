using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAWaypoints : MonoBehaviour
{
    //Componentes
    GameObject aEstrella;
    GameObject guerrero;
    AEstrella generadorCamino;
    Grid grid;
    Matematica mate;

    //Variables
    GameObject entusiasta;
    public bool seguirJugador = false;
    public float velocidad = 0.5f;
    public float velocidadEntusiasta = 1f;
    public float precision = 3f;
    List<Nodo> camino;
    int contador = 0;

    void Start()
    {
        aEstrella = GameObject.Find("AEstrella");
        guerrero = GameObject.Find("Guerrero");

        generadorCamino = aEstrella.GetComponent<AEstrella>();
        grid = aEstrella.GetComponent<Grid>();
        mate = aEstrella.GetComponent<Matematica>();

        entusiasta = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        entusiasta.transform.position = transform.position;
        entusiasta.transform.localScale = transform.localScale / 2;
        Destroy(entusiasta.GetComponent<Collider>());
        Destroy(entusiasta.GetComponent<MeshRenderer>());
    }

    void Update()
    {
        DirigirseJugador();
    }

    public void AcompañarEntusiasta()
    {
        RatrearCamino();
        if (grid.GetNodo(transform.position) != grid.GetNodo(guerrero.transform.position))
        {
            Vector3 direccion = entusiasta.transform.position - transform.position;
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
        }
    }

    void DirigirseJugador()
    {
        if (seguirJugador == true)
        {
            GenerarCamino(entusiasta, guerrero);
        }
    }

    void RatrearCamino()
    {
        if (camino != null)
        {
            int distanciaCamino = camino.Count - 1;
            contador = 0;

            //Debug.Log("Contador: " + contador + "/// LargoCamino: " + distanciaCamino);
            if (contador < distanciaCamino)
            {
                if (mate.Distancia(entusiasta.transform.position, camino[contador].GetPosicionEscena()) < precision)
                {
                    contador++;
                }
            }

            if (contador <= distanciaCamino)
            {
                Vector3 direccion = camino[contador].GetPosicionEscena() - entusiasta.transform.position;
                entusiasta.transform.position += direccion.normalized * velocidadEntusiasta * Time.deltaTime;
            }
        }
    }

    void GenerarCamino(GameObject inicio, GameObject fin)
    {
        generadorCamino.BusquedaCamino(inicio.transform.position, fin.transform.position);
        camino = generadorCamino.ConstruirCamino(grid.GetNodo(inicio.transform.position), grid.GetNodo(fin.transform.position));
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
                if (n == grid.GetNodo(guerrero.transform.position))
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.GetPosicionEscena(), Vector2.one * grid.ladoNodo);
            }
        }
    }
}
