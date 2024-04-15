using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAMovimiento : MonoBehaviour
{

    IA iA;

    void Awake()
    {
        iA = GetComponent<IA>();
    }

    public void SeguirGuia()
    {
        iA.iAWPManager.GuiaRecorreCamino();
        transform.position = Vector2.MoveTowards(transform.position, iA.iAWPManager.GetPosicionGuia(), iA.GetVelocidad() * Time.deltaTime);

    }
}
