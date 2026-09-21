using UnityEngine;
using UnityEngine.EventSystems;

public class BotonMovimiento : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Guarda el script de movimiento del personaje, que es el que sabe caminar
    private MovimientoJugador jugador;

    // Se marca en el Inspector: activado es el botón de la derecha, desactivado el de la izquierda
    public bool esDerecha;

    // Busca al personaje y toma su script de movimiento
    void BuscarJugador()
    {
        // Solo lo busca si todavía no lo tiene guardado
        if (jugador == null)
        {
            // Busca el objeto que tiene el tag "Player"
            GameObject objetoJugador = GameObject.FindWithTag("Player");

            // Si lo encontró, toma de él el script MovimientoJugador
            if (objetoJugador != null)
                jugador = objetoJugador.GetComponent<MovimientoJugador>();
        }
    }

    // Se ejecuta cuando el jugador toca o presiona el botón
    public void OnPointerDown(PointerEventData eventData)
    {
        // Se asegura de tener al personaje antes de moverlo
        BuscarJugador();

        // Si no hay personaje, sale y no hace nada
        if (jugador == null) return;

        // Según el botón, el personaje empieza a caminar a la derecha o a la izquierda
        if (esDerecha)
            jugador.EmpezarDerecha();
        else
            jugador.EmpezarIzquierda();
    }

    // Se ejecuta cuando el jugador suelta el botón
    public void OnPointerUp(PointerEventData eventData)
    {
        // Se asegura de tener al personaje antes de detenerlo
        BuscarJugador();

        // Si no hay personaje, sale y no hace nada
        if (jugador == null) return;

        // Según el botón, el personaje deja de caminar a la derecha o a la izquierda
        if (esDerecha)
            jugador.TerminarDerecha();
        else
            jugador.TerminarIzquierda();
    }
}