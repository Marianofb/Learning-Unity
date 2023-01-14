using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoVision : MonoBehaviour
{
    [Header("Deteccion")]
    public float contadorLocalizador = 0;
    public float esperaContador = 1f;
    [Range (0,1000)]
    public int tiempoDetectar;
    public float adicion = 1f;
    public float resta = 1f;
  
    [Header("Colisionador")]
    public LayerMask mascaraEnemigo; 
    public LayerMask mascaraObstaculo; 

    [Header("Radio")]
    [Range (0,360)]
    public float radio;
    public float angulo;
    private float radianes;
    private Vector2 rayoA;
    private Vector2 rayoB;

    [Header("Componentes")]
    Animator animador;
    Mate _mate;
    
    void Start()
    {
        animador = GetComponent<Animator>();
        radianes = angulo * Mathf.Deg2Rad;
    }

    void FixedUpdate()
    {
        Angulo();
    }

   void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
        Gizmos.DrawRay(transform.position, -rayoA);
        Gizmos.DrawRay(transform.position, -rayoB);
    }

    public bool Localizamos()
    {
        if(Localizador())
        {
            contadorLocalizador +=  adicion;
            contadorLocalizador = Mathf.Clamp(contadorLocalizador, 0, tiempoDetectar);
        }
        else
        {
            contadorLocalizador -=  resta;
            contadorLocalizador = Mathf.Clamp(contadorLocalizador, 0, tiempoDetectar);
        }

        if(contadorLocalizador == tiempoDetectar)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool Localizador()
    {
        Collider2D [] listaEnemigos = Physics2D.OverlapCircleAll(transform.position, radio, mascaraEnemigo );
        foreach (Collider2D enemigo in listaEnemigos)
        {
            Vector2 posicionJugador = transform.position;
            Vector2 posicionC = enemigo.transform.position;
            Vector2 direccionC = posicionC - posicionJugador;
            float distancia = Vector2.Distance(transform.position, posicionC);

            if(Vector2.Angle(transform.right, direccionC) < angulo & animador.GetFloat("Mirar X") > 0)
            {   
                if(!Physics2D.Raycast(transform.position, direccionC, distancia, mascaraObstaculo))
                {
                    //Debug.Log("Vimos a: " + enemigo.name);
                    return true;
                }
            }

            if(Vector2.Angle(-transform.right, direccionC) < angulo & animador.GetFloat("Mirar X")  < 0)
            {
                if(!Physics2D.Raycast(transform.position, direccionC, distancia, mascaraObstaculo))
                {
                    //Debug.Log("Vimos a: " + enemigo.name);
                    return true;
                }
            }

            if(Vector2.Angle(transform.up, direccionC) < angulo & animador.GetFloat("Mirar Y")  > 0)
            {
                if(!Physics2D.Raycast(transform.position, direccionC, distancia, mascaraObstaculo))
                {
                    //Debug.Log("Vimos a: " + enemigo.name);
                    return true;
                }
            }

             if(Vector2.Angle(-transform.up, direccionC) < angulo & animador.GetFloat("Mirar Y")  < 0)
            {
                if(!Physics2D.Raycast(transform.position, direccionC, distancia, mascaraObstaculo))
                {
                    //Debug.Log("Vimos a: " + enemigo.name);
                    return true;
                }
            }
        }

        return false;
    }

    void Angulo()
    {
        if (animador.GetFloat("Mirar X")  != 0)
        {
            float x = Mathf.Cos(radianes) * radio;
            float y = Mathf.Sin(radianes) * radio; 
        
            if (animador.GetFloat("Mirar X")  > 0)
            {
                rayoA = new Vector2(x, y);
                rayoB = new Vector2(x, -y);   
            }
            else
            {
                rayoA = new Vector2(-x, y);
                rayoB = new Vector2(-x, -y);  
            }
        }

        if (animador.GetFloat("Mirar Y")  != 0)
        {
            float x = Mathf.Sin(radianes) * radio; 
            float y = Mathf.Cos(radianes) * radio;

            if (animador.GetFloat("Mirar Y")  > 0)
            {
                rayoA = new Vector2(x, y);
                rayoB = new Vector2(-x, y); 
            }
            else
            {
                rayoA = new Vector2(x, -y);
                rayoB = new Vector2(-x, -y);  
            }
        }
    }
}
