using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPManager : MonoBehaviour
{
    Pathfinding_AlgoritmoA algoritmoA;
    IAMovimiento iAMovimiento;
    IAWaypoints iAWaypoints;
    VIEJOCampoVision campoVision;
    Mate mate;

    void Start()
    {
        algoritmoA = GameObject.Find("Algoritmo*A").GetComponent<Pathfinding_AlgoritmoA>();
        iAWaypoints = gameObject.GetComponent<IAWaypoints>();
        campoVision = gameObject.GetComponent<VIEJOCampoVision>();
        mate = GetComponent<Mate>();
    }

    public void GenerarCaminoJugador(Transform seguidor, Transform objetivo)
    {
        if (campoVision.Localizamos())
        {
            Vector3[] camino = algoritmoA.BusquedaCamino(objetivo.position, seguidor.position);
            if (!MismoCamino(iAWaypoints.GetWaypoints(), camino))
            {
                iAWaypoints.SetPuntoActual(camino);
                iAWaypoints.SetWaypoints(camino);
            }
        }
    }

    bool MismoCamino(Vector3[] camino, Vector3[] caminoFuturo)
    {
        if (camino.Length != caminoFuturo.Length)
        {
            return false;
        }

        for (int i = 0; i < camino.Length; i++)
        {
            if (camino[i] != caminoFuturo[i])
                return false;
        }

        return true;
    }
}
