using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probando : MonoBehaviour
{
	//VIDA
	int cantidad;
	int total;

    // Start is called before the first frame update
    void Start()
    {
        cantidad = -2;
        total = 12;
    }

    // Update is called once per frame
    void Update()
    {
        total =  Mathf.Clamp(total + cantidad, 0 , total);
       	if(total > 0)
       	{
       		//Debug.Log(total);       	
       	}
    }
}
