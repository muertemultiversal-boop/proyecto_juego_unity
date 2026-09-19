using UnityEngine; 
using UnityEngine.SceneManagement; 

public class SeleccionarPersonaje : MonoBehaviour
{

    public static int personajeSeleccionado = -1;

    // Se llama cuando el jugador aprieta el botón del personaje 1
    public void SeleccionarPersonaje1()
    {
        personajeSeleccionado = 0; // Guardamos que el personaje elegido es el número 0
        Debug.Log("Personaje elegido: 0"); 
    }

    // Se llama cuando el jugador aprieta el botón del personaje 2
    public void SeleccionarPersonaje2()
    {
        personajeSeleccionado = 1; // Guardamos que el personaje elegido es el número 1
        Debug.Log("Personaje elegido: 1");
    }

    // Se llama cuando el jugador aprieta el botón del personaje 3
    public void SeleccionarPersonaje3()
    {
        personajeSeleccionado = 2; // Guardamos que el personaje elegido es el número 2
        Debug.Log("Personaje elegido: 2");
    }

    // Se llama cuando el jugador aprieta "Volver al inicio" desde el menú de pausa
    public void VolverAlInicioDeEscena()
    {
        Time.timeScale = 1f; // IMPORTANTE: resetemos el tiempo a la normalidad despues del Pause
        SceneManager.LoadScene("Interfaz del jugador"); // Cargamos la escena del menú principal
    }
}
