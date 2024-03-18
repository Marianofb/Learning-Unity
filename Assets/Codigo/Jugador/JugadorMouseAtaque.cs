using UnityEngine;
using UnityEngine.UIElements;

public class JugadorMouseAtaque : MonoBehaviour
{
    //DrawGizmos
    float radioGizmo;

    //ZonaAtaque
    private Vector2 zonaAtaque;

    //Componentes
    private JugadorAnimacion animacion;

    [Header("Etiquetas")]
    public LayerMask mascaraEnemigo;
    public LayerMask mascaraObstaculo;

    [Header("VariablesAtaque")]
    //Puño
    public float radio_Puño = 0.1f;
    public float distancia_Puño = 0.3f;
    //Cabeza
    public float radio_Cabeza = 0.1f;
    public float distancia_Cabeza = 0.3f;

    void Awake()
    {
        SetComponentes();
    }

    void Start()
    {
        zonaAtaque = new Vector2();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(zonaAtaque, radioGizmo);
    }

    public void SetZonaAtaquePuño()
    {
        radioGizmo = radio_Puño;
        float angle = Mathf.Atan2(animacion.GetMouseVector2().y, animacion.GetMouseVector2().x) * Mathf.Rad2Deg;
        float x = transform.position.x + distancia_Puño * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia_Puño * Mathf.Sin(angle * Mathf.Deg2Rad);

        zonaAtaque.Set(x, y);
        EnemigoAtacado(radio_Puño);
    }

    public void SetZonaAtaqueCabeza()
    {
        radioGizmo = radio_Cabeza;
        float angle = Mathf.Atan2(animacion.GetMouseVector2().y, animacion.GetMouseVector2().x) * Mathf.Rad2Deg;
        float x = transform.position.x + distancia_Cabeza * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + distancia_Cabeza * Mathf.Sin(angle * Mathf.Deg2Rad);

        zonaAtaque.Set(x, y);
        EnemigoAtacado(radio_Cabeza);
    }

    private void EnemigoAtacado(float radio)
    {
        Collider2D[] listaEnemigos = Physics2D.OverlapCircleAll(zonaAtaque, radio, mascaraEnemigo);
        Debug.Log("LAAAARGP: " + listaEnemigos.Length);
        foreach (Collider2D c in listaEnemigos)
        {
            Debug.Log("Hicimos daño a: " + c.name);
            IA iA = c.GetComponent<IA>();
            //ia.RecibeDaño();
            iA.recibiendoDaño = true;
            iA.iAAnimacion.PlayRecibiendoDaño();
            iA.StateMachine.CambiarEstado(iA.RecibiendoDañoState);
        }
    }

    void SetComponentes()
    {
        animacion = GetComponent<JugadorAnimacion>();
    }

    /*
    public float GetXAccion()
    {
        return xAccion;
    }

    public float GetYAccion()
    {
        return yAccion;
    }

    
 public void RangoAtaqueVIEJO(float radio, float distancia)
 {
     this.radio = radio;

     //oeste
     if (xAccion <= -0.7f)
     {
         rango.Set(transform.position.x - distancia, transform.position.y);
     }

     //este
     if (xAccion >= 0.7f)
     {
         rango.Set(transform.position.x + distancia, transform.position.y);
     }

     //sur
     if (yAccion <= -0.7f)
     {
         rango.Set(transform.position.x, transform.position.y - distancia);
     }

     //norte
     if (yAccion >= 0.7f)
     {
         rango.Set(transform.position.x, transform.position.y + distancia);
     }

     Collider2D[] listaEnemigos = Physics2D.OverlapCircleAll(rango, radio, mascaraEnemigo);
     foreach (Collider2D c in listaEnemigos)
     {
         Debug.Log("Hicimos daño a: " + c.name);
     }
 }*/
}
