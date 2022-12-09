using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorValores : MonoBehaviour
{
    [Header ("Vida")]
    public float vidaTotal;
    public float vidaMax;
    public float recuperacionVida;

    [Header ("Estamina")]
    public float estaminaTotal;
    public float estaminaMax;
    public float recuperacionEstamina;

    [Header ("Componentes")]
    JugadorCombate combate;

    void Start()
    {
        estaminaTotal = estaminaMax;
        vidaTotal = vidaMax;
        combate = GetComponent<JugadorCombate>();
    }

    void Update()
    {
        RecuperarEstamina();
        RecuperarVida();
    }

    public void ActualizarEstamina(float costo)
    { 
        estaminaTotal = Mathf.Clamp(estaminaTotal + costo, 0f, estaminaMax);    
    }

    public void ActualizarVida(float costo)
    { 
        vidaTotal = Mathf.Clamp(vidaTotal + costo, 0f, vidaMax);    
    }

    private void RecuperarEstamina()
    {
        if(combate.getEspera() == false & estaminaTotal < estaminaMax)
        {
            ActualizarEstamina(recuperacionEstamina);
        }

        if(estaminaTotal >= estaminaMax)
        {
             //estoy lleno
        }
    }

    private void RecuperarVida()
    {
        if(vidaTotal < vidaMax)
        {
            ActualizarVida(recuperacionVida);
        }
        else
        {
            //estoy lleno
        }
    }

    public float getEstaminaTotal()
    {
        return estaminaTotal;
    }
}
