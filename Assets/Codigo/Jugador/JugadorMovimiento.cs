using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class JugadorMovimiento : MonoBehaviour
{
    //Axis
    private float xAxis;
    private float yAxis;
    public Vector3 direccion;

    [Header("Desplazamiento")]
    public float velocidad;
    public float variacionPosicion;

    void Start()
    {

    }

    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direccion.Set(xAxis, yAxis, 0f);
        variacionPosicion = direccion.magnitude;
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    public float GetVariacionPosicion()
    {
        return variacionPosicion;
    }
}
