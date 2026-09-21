using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Panel de pausa que se muestra al pausar (se arrastra aquí en el Inspector)
    public GameObject panelPausa;

    // Muestra el panel de pausa y detiene el juego
    public void PausarJuego()
    {
        // Activa el panel para que aparezca en pantalla
        panelPausa.SetActive(true);

        // Time.timeScale en 0 congela el tiempo, así todo el juego se detiene
        Time.timeScale = 0f;
    }

    // Oculta el panel de pausa y el juego sigue
    public void ReanudarJuego()
    {
        // Desactiva el panel para que desaparezca
        panelPausa.SetActive(false);

        // Time.timeScale en 1 devuelve el juego a su velocidad normal
        Time.timeScale = 1f;
    }
}
