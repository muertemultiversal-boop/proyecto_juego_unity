using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarPersonaje : MonoBehaviour
{
    public static int personajeSeleccionado = -1;

    public void SeleccionarPersonaje1()
    {
        personajeSeleccionado = 0;
        Debug.Log("Personaje elegido: 0");
    }

    public void SeleccionarPersonaje2()
    {
        personajeSeleccionado = 1;
        Debug.Log("Personaje elegido: 1");
    }

    public void SeleccionarPersonaje3()
    {
        personajeSeleccionado = 2;
        Debug.Log("Personaje elegido: 2");
    }

    public void VolverAlInicioDeEscena()
    {
    SceneManager.LoadScene("Interfaz del jugador");
}
}
