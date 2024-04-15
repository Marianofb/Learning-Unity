using UnityEngine;

public class JugadorCombos : MonoBehaviour
{

    //TiempoEntreAtaques
    //Puño1 Puño2 Puño3 (ejemplo, el nombre del ataque esta relacionado con un indice --> "Puño" + "indiceCombo" --> El indice va de 1 a 3)
    //La idea es que si el Jugador presiona cierta tecla que coincida con el combo en un rango de tiempo despues de haber inicializado la ancimacion de "Ataque1", si se presiona 
    //entonces cuando se finalize el primer ataque se activa el segundo y asi el tercero y cuando llegamos al ultimo volvemos al primero.

    Jugador jugador;
    public string nombreComboActual = null;
    public int indiceComboActual = 1;
    public float tiempoUltimoAtaque;
    public float tiempoEntreCombo = 0.5f;

    void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    void Update()
    {
        LimiteTiempo();
    }

    public void EjecutarCombo(string nombreAtaque, float duracionAtaque, int largoCombo)
    {
        if (nombreComboActual != nombreAtaque)
        {
            indiceComboActual = 1;
            nombreComboActual = nombreAtaque;
        }

        string nombreAtaqueActual = nombreAtaque + indiceComboActual;
        jugador.animacion.PlayAnimacion(nombreAtaqueActual);
        Invoke("AtaqueCompletado", duracionAtaque);

        indiceComboActual++;
        if (indiceComboActual > largoCombo)
        {
            indiceComboActual = 1;
        }
    }

    void AtaqueCompletado()
    {
        tiempoUltimoAtaque = Time.time;
        jugador.SetEstaAtacando(false);
    }

    void LimiteTiempo()
    {
        if (Time.time - tiempoUltimoAtaque > tiempoEntreCombo)
        {
            indiceComboActual = 1;
        }
    }
}
