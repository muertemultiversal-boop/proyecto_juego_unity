using UnityEngine;
using UnityEngine.EventSystems;

public class BotonMovimiento : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private MovimientoJugador jugador;
    public bool esDerecha;

    void BuscarJugador()
    {
        if (jugador == null)
        {
            GameObject objetoJugador = GameObject.FindWithTag("Player");
            if (objetoJugador != null)
                jugador = objetoJugador.GetComponent<MovimientoJugador>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuscarJugador();
        if (jugador == null) return;

        if (esDerecha)
            jugador.EmpezarDerecha();
        else
            jugador.EmpezarIzquierda();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BuscarJugador();
        if (jugador == null) return;

        if (esDerecha)
            jugador.TerminarDerecha();
        else
            jugador.TerminarIzquierda();
    }
}