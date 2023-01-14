using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    private static Grid instancia = null;

    [Header ("Grid")]
    public int ancho;
    public int largo;
    public int tamañoCelda = 1;
    public bool mostrarNodos = false;
    private GameObject tileReferencia;
        
    [Header ("Nodos")]
    public LayerMask obstaculo;
    public Transform posicionJugador;
    private Vector3 posicionStalker;
    Nodo [,] nodos;

    private void Awake()
    {
         if(instancia != null)
        {
            Destroy(this.gameObject);
        }            
        else
        {
            instancia = this;
        }  
    }

    private void Start()
    {
        CrearGrid();
    }

    public int GetTamaño()
    {
        return ancho * largo;
    }

    public Nodo GetNodo(Vector3 posicion)
    {
        float gridAncho = ancho * tamañoCelda;
        float gridLargo = largo * tamañoCelda;
        Vector3 surOeste = new Vector3((-gridAncho/2 + tamañoCelda) - 1, (tamañoCelda - gridLargo/2) - 1);

        Vector3 posicionCentro = posicion - surOeste;
        int x = (int) posicionCentro.x / tamañoCelda;
        int y = (int) posicionCentro.y / tamañoCelda;
        //Debug.Log("X: " + x + " Y: "+ y);

        return nodos[x,y];
    }

    public List<Nodo> GetVecinos(Nodo nodo)
    {
        List<Nodo> vecinos = new List<Nodo>();

        //hay tres posibilidades por fila
        //y hay tres filas, -1, 0 y 1
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                //saltamos al nodo del que buscamos los vecinos
                if(x == 0 & y == 0)
                {
                    continue;
                }
                else
                {
                    int controlX = nodo.gridX + x;
                    int controlY = nodo.gridY + y;

                    if(controlX >= 0 & controlX < ancho & controlY >= 0 & controlY < largo)
                    {
                        vecinos.Add(nodos[controlX, controlY]);
                    }
                }
            }
        }

        return vecinos;
    }

    public void SetStalker(Vector3 stalker)
    {
        posicionStalker = stalker;
    }

    private void CrearGrid()
    {
        /*if(mostrarNodos == true)
        {
            tileReferencia = (GameObject)Instantiate(Resources.Load("Tiles/probando"));
        }
        else
        {
            tileReferencia = null;
        }*/

        nodos = new Nodo[ancho,largo];

        float gridAncho = ancho * tamañoCelda;
        float gridLargo = largo * tamañoCelda;

        Vector3 surOeste = new Vector3((-gridAncho/2 + tamañoCelda) - 0.5f, (tamañoCelda - gridLargo/2) - 0.5f);

        for(int x = 0; x < ancho; x++)
        {
            for(int y = 0; y < largo; y++)
            {
                float posX = x * tamañoCelda;
                float posY = y * tamañoCelda; 

                /*if(tileReferencia != null)
                {
                    GameObject tile = (GameObject)Instantiate(tileReferencia, transform);
                    tile.transform.position = new Vector3(posX, posY);
                }*/

                Vector3 posicionMundo =  new Vector3(posX, posY) + surOeste;
                bool caminable = !Physics2D.OverlapCircle(posicionMundo, tamañoCelda/2, obstaculo); 
                nodos[x,y] = new Nodo(caminable, posicionMundo, x, y);
                //Debug.Log("X: " + x + "; Y: " + y);
                //Debug.Log("posicion: " + posicionMundo);
                //if(caminable == false)
                    //Debug.Log("caminable: " + caminable);
            }
        }
        Destroy(tileReferencia);
        transform.position = surOeste;
    }

    public Vector3[] camino;
    private void OnDrawGizmos()
    {
        if(nodos != null && mostrarNodos == true)
        {      
            Nodo nodoJugador = GetNodo(posicionJugador.position);
            Nodo nodoStalker = GetNodo(posicionStalker);
            foreach(Nodo n in nodos)
            {
                //si bool == true entonces blanco, sino rojo
                //Gizmos.color = (n.caminable)?Color.white:Color.red;
                Gizmos.color = Color.white;
                int existe = Array.IndexOf(camino, n.GetPosicionMundo());
                if(camino != null && existe > -1)
                {
                    Gizmos.color = Color.black;
                } 

                if(n == nodoJugador)
                {
                    Gizmos.color = Color.green;
                }

                if(n == nodoStalker)
                {
                    Gizmos.color = Color.yellow;
                }

                if(!n.caminable)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(n.GetPosicionMundo(),  Vector3.one * (tamañoCelda - .1f));
            }
        }
    }
}