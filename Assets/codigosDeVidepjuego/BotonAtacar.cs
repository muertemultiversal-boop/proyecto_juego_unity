using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAtacar : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Busca al personaje activo cada vez que se presiona el botón
        GameObject objetoJugador = GameObject.FindWithTag("Player");

        if (objetoJugador == null)
            return;

        MovimientoJugador jugador = objetoJugador.GetComponent<MovimientoJugador>();

        // Si tiene el script  ataca
        
        if (jugador != null)
            jugador.Atacar();
    }
}