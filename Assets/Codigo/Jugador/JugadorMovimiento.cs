using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class JugadorMovimiento : MonoBehaviour
{
    //Axis
    private float xAxis;
    private float yAxis;
    private Vector3 direccion;

    [Header("Desplazamiento")]
    public float velocidad;
    private float variacionPosicion;

    //Bool
    public bool bloqueo;

    void Start()
    {
        bloqueo = false;
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
        if (bloqueo == false)
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }

    public float GetVariacionPosicion()
    {
        return variacionPosicion;
    }

    public void SetBloqueo(bool b)
    {
        bloqueo = b;
    }


}
