using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    private static Grid instancia = null;

    [Header ("Grid")]
    public int ancho;
    public int largo;
    static int tamañoCelda = 1;

    [Header ("Nodos")]
    public LayerMask obstaculo;
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

    private void CrearGrid()
    {
        GameObject tileReferencia = (GameObject)Instantiate(Resources.Load("Tiles/probando"));
        nodos = new Nodo[ancho,largo];

        float gridAncho = ancho * tamañoCelda;
        float gridLargo = largo * tamañoCelda;
        Vector2 surOeste = new Vector2((-gridAncho/2 + tamañoCelda) - 0.5f, (tamañoCelda - gridLargo/2) - 0.5f);

        for(int x = 0; x < ancho; x++)
        {
            for(int y = 0; y < largo; y++)
            {
                GameObject tile = (GameObject)Instantiate(tileReferencia, transform);
                float posX = x * tamañoCelda;
                float posY = y * tamañoCelda; 
                tile.transform.position = new Vector2(posX, posY);
                
                Vector2 posicionMundo =  new Vector2(posX,posY) + surOeste;
                bool caminable = !Physics2D.OverlapCircle(posicionMundo, tamañoCelda/2); 
                nodos[x,y] = new Nodo(caminable, posicionMundo);
                //Debug.Log("X: " + x + "; Y: " + y);
                //Debug.Log("posicion: " + posicionMundo);
                if(caminable == false)
                    Debug.Log("caminable: " + caminable);
            }
        }
        Destroy(tileReferencia);
        transform.position = surOeste;
    }
}

