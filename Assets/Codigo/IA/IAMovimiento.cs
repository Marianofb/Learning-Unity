using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{
    //componentes
    IAWaypoints waypoints;

    void Start()
    {
        waypoints = GetComponent<IAWaypoints>();
    }

    void Update()
    {
        waypoints.AcompañarEntusiasta();
    }
}
