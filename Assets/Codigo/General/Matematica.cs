using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matematica : MonoBehaviour
{
    public float Distancia(Vector3 tuPosicion, Vector3 otraPosicion)
    {
        //Teorema de Pitagoras
        float distancia = Mathf.Sqrt(Mathf.Pow(otraPosicion.x - tuPosicion.x, 2) +
                            Mathf.Pow(otraPosicion.y - tuPosicion.y, 2));

        return distancia;
    }

    //Volver a estudiar
    public float ProductoEscalar(Vector3 puntoInicial, Vector3 direccion)
    {
        float pEscalar = puntoInicial.x * direccion.x + puntoInicial.y * direccion.y;

        return pEscalar;
    }

    public float AnguloPEscalar(float pEscalar, Vector3 puntoInicial, Vector3 direccion)
    {
        float angulo = Mathf.Acos(pEscalar / (puntoInicial.magnitude * direccion.magnitude)) * Mathf.Rad2Deg;

        return angulo;
    }

    public Vector3 ProductoVectorial(Vector3 a, Vector3 b)
    {
        Vector3 v = new Vector3(a.y * b.z - a.z * b.y,
                                a.x * b.z - a.z * b.x,
                                a.x * b.y - a.y * b.x);

        return v;
    }
}
