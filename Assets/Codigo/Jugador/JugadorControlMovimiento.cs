using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorControlMovimiento : MonoBehaviour
{

    [Header("Variables")]
    public float velocidad = 2f;
    private float xAxis;
    private float yAxis;
    private Vector3 direccion;
    private float variacionPosicion;

    public void Caminar()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direccion.Set(xAxis, yAxis, 0f);
        variacionPosicion = direccion.magnitude;

        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    public bool GetEstaCaminando()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            return true;
        }

        return false;
    }
}
