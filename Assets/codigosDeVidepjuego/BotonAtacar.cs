using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAtacar : MonoBehaviour, IPointerDownHandler
{
    // Se ejecuta en el momento exacto en que el jugador toca o presiona el botón
    public void OnPointerDown(PointerEventData eventData)
    {
        // Busca al personaje activo cada vez que se presiona el botón
        // FindWithTag busca el objeto que tenga el tag "Player"
        GameObject objetoJugador = GameObject.FindWithTag("Player");

        // Si no encontró ningún personaje, sale de la función y no hace nada
        if (objetoJugador == null)
            return;

        // Toma del personaje el script MovimientoJugador, que es el que sabe atacar
        MovimientoJugador jugador = objetoJugador.GetComponent<MovimientoJugador>();

        // Si el personaje tiene ese script, entonces ataca
        if (jugador != null)
            jugador.Atacar();
    }
}