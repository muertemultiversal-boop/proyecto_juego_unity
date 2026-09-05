using UnityEngine;

public class SpawnDelJugador : MonoBehaviour
{
    // Estas son las "casillas" donde vas a arrastrar cada personaje
    // desde la Hierarchy, en el Inspector de Unity.
    // Por ahora solo llenas "personaje1", los otros 2 los dejas vacíos.
    public GameObject personaje1;
    public GameObject personaje2;
    public GameObject personaje3;

    // Start() se ejecuta UNA sola vez, apenas arranca la escena.
    void Start()
    {
        // Primero apagamos los 3 personajes (por si estaban visibles en la escena).
        // El "if" de adelante es solo para evitar error si todavía
        // no arrastraste ese personaje en el Inspector (está vacío/null).
        if (personaje1 != null) personaje1.SetActive(false);
        if (personaje2 != null) personaje2.SetActive(false);
        if (personaje3 != null) personaje3.SetActive(false);

        // Ahora revisamos cuál personaje eligió el jugador.
        // Ese número (0, 1 o 2) quedó guardado antes, en la pantalla
        // de selección de personaje, dentro de SeleccionarPersonaje.personajeSeleccionado.

        // Si eligió el personaje 1 (guardado como el número 0)...
        if (SeleccionarPersonaje.personajeSeleccionado == 0 && personaje1 != null)
        {
            personaje1.SetActive(true); // ...lo encendemos, para que se vea en el mapa.
        }
        // Si no fue el 1, revisamos si fue el personaje 2 (número 1)...
        else if (SeleccionarPersonaje.personajeSeleccionado == 1 && personaje2 != null)
        {
            personaje2.SetActive(true);
        }
        // Si no fue ninguno de los 2 anteriores, probamos con el personaje 3 (número 2)...
        else if (SeleccionarPersonaje.personajeSeleccionado == 2 && personaje3 != null)
        {
            personaje3.SetActive(true);
        }
        // Si no entra en ninguno de los 3 casos, no aparece nadie
        // (esto pasaría si el jugador nunca eligió personaje, por ejemplo).
    }
}
