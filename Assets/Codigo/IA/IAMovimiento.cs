using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{
    [Header("Componentes")]
    IA iA;
    IAWPManager iAWPManager;

    [Header("Flocking")]
    public Vector3 direccion = Vector3.zero;
    public LayerMask mascaraVecino;
    public float radioVecino = 2f;
    public float distanciaEvadir = 1f;
    public List<GameObject> vecinos;
    public int cantVecinos;

    void Awake()
    {
        SetComponentes();
    }

    void Update()
    {
        SetVecinos();
    }

    void OnDrawGizmosSelected()
    {
        //Radio Vecinos
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVecino);

        //Distancia Evadir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaEvadir);
    }

    public void SeguirGuia()
    {
        iAWPManager.GuiaRecorreCamino();
        transform.position = Vector2.MoveTowards(transform.position, iAWPManager.GetPosicionGuia(), iA.GetVelocidad() * Time.deltaTime);

    }


    private void SetVecinos()
    {
        vecinos = new List<GameObject>();
        Collider2D[] colisionadores = Physics2D.OverlapCircleAll(transform.position, radioVecino, mascaraVecino);
        if (colisionadores.Count() > 0)
        {
            foreach (Collider2D c in colisionadores)
            {
                GameObject nuevoVecino = c.gameObject;
                if (nuevoVecino.name != name)
                {
                    vecinos.Add(c.gameObject);
                }
            }
        }
    }

    private void SetComponentes()
    {
        iA = GetComponent<IA>();
        iAWPManager = GetComponent<IAWPManager>();
    }
}
