using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTiempo : MonoBehaviour
{
    float tiempo = 30f; 
    void Start()
    {
        
    }

    void Update()
    {
        tiempo -= Time.deltaTime;
        Debug.Log("Tiempo: " + tiempo);
    }
}
