using UnityEngine;

public class IAAnimacion : MonoBehaviour
{
    //Componentes
    private Animator animador;
    private IA iA;
    private IAWPManager iAWPManager;

    //Direccion
    private float xAxis;
    private float yAxis;
    private Vector2 direccion = new Vector2();

    //Nombres de Variables del Controlador Animador
    private const string rumboX = "Rumbo X";
    private const string rumboY = "Rumbo Y";

    //Nombres de Animaciones 
    public const string Idle = "Idle";
    public const string Caminar = "Caminar";
    private const string AtaquePuñoDer = "PuñoDer";
    private const string AtaquePuñoIzq = "PuñoIzq";
    private const string RecibiendoDaño = "RecibiendoDaño";

    //Bools de  las Animaciones (der, izq)
    public bool cambioPuño = false;

    //Duracion de  las Animaciones (segun el clip)
    public float duracion_RecibiendoDaño = 0.5f;
    public float duracion_AtaquePuño = 0.5f;
    public float duracion_AtaqueCabeza = 0.5f;

    void Awake()
    {
        SetComponentes();
    }

    public void PlayIdle()
    {
        CambiarAnimacion(Idle);
    }

    public void PlayCaminar()
    {
        CambiarAnimacion(Caminar);
    }

    public void PlayAtaquePuño()
    {
        if (!cambioPuño)
        {
            CambiarAnimacion(AtaquePuñoDer);
        }
        else
        {
            CambiarAnimacion(AtaquePuñoIzq);
        }
        iA.SetBloqueo(true);
        //El "tiempo" depende de la cantidad de sprites en un clip
        //Que no coinice con el StateInfo.length, entoces lo harcodeo
        Invoke("AnimacionAtaqueCompletada", duracion_AtaquePuño);
    }

    public void PlayRecibiendoDaño()
    {
        CambiarAnimacion(RecibiendoDaño);
        Invoke("AnimacionRecibiendoDañoCompletada", duracion_RecibiendoDaño);
    }

    void AnimacionAtaqueCompletada()
    {
        iA.SetBloqueo(false);
    }

    void AnimacionRecibiendoDañoCompletada()
    {
        iA.SetRecibiendoDaño(false);
    }

    private void CambiarAnimacion(string nombreAnimacion)
    {
        animador.Play(nombreAnimacion);
    }

    public void SetDireccionJugador()
    {
        xAxis = iA.GetPosicionJugador().x - transform.position.x;
        yAxis = iA.GetPosicionJugador().y - transform.position.y;

        direccion = new Vector2(xAxis, yAxis);

        animador.SetFloat(rumboX, direccion.x);
        animador.SetFloat(rumboY, direccion.y);
        //Sobreescribir Accion con Direccion 
        //(solo porque estamos usando el mismo Aniamdor para el jugaro y la IA)
        animador.SetFloat("Accion X", direccion.x);
        animador.SetFloat("Accion Y", direccion.y);
    }

    //Para Animation Event (con que este en una sola animacion configura a todo el Blend Tree)
    public void CambioPuño()
    {
        cambioPuño = !cambioPuño;
    }

    public void SetDireccionIdle()
    {
        Vector2 random = Random.insideUnitCircle;

        //Debug.Log("RANDOM X: " + random.x);
        //Debug.Log("RANDOM Y: " + random.y);
        animador.SetFloat(rumboX, random.x);
        animador.SetFloat(rumboY, random.y);
    }

    private void SetComponentes()
    {
        animador = GetComponent<Animator>();
        iA = GetComponent<IA>();
        iAWPManager = GetComponent<IAWPManager>();
    }
}
