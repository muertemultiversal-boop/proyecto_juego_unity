using UnityEngine;

public class Vida : MonoBehaviour
{
    // Cuántos golpes aguanta el personaje antes de morir
    public int vidaMaxima = 5;

    // Barra de vida en pantalla que cambia con cada golpe (se arrastra en el Inspector)
    public BarraVidaUI barra;

    // Vida que le queda al personaje en este momento
    private int vidaActual;

    // Se ejecuta una sola vez, cuando empieza el juego
    void Start()
    {
        // Empieza con la vida llena
        vidaActual = vidaMaxima;

        // Si tiene barra, la muestra llena (0 golpes recibidos)
        if (barra != null) barra.ActualizarBarra(0);
    }

    // Se llama cada vez que el personaje recibe un golpe
    public void RecibirGolpe()
    {
        // Si ya no tiene vida, no hace nada
        if (vidaActual <= 0) return;

        // Le quita una vida
        vidaActual--;

        // Actualiza la barra con los golpes recibidos (vida máxima menos vida actual)
        if (barra != null) barra.ActualizarBarra(vidaMaxima - vidaActual);

        // Muestra en la Console cuántas vidas le quedan
        Debug.Log(gameObject.name + " le quedan " + vidaActual + " vidas");

        // Si llegó a 0 vidas, muere
        if (vidaActual <= 0) Morir();
    }

    // Se ejecuta cuando el personaje se queda sin vida
    void Morir()
    {
        // Muestra en la Console que fue derrotado
        Debug.Log(gameObject.name + " fue derrotado");

        // Busca en la escena el script FinDePartida
        FinDePartida fin = FindFirstObjectByType<FinDePartida>();

        // Si existe, le avisa quién murió
        if (fin != null)
        {
            // Si este objeto tiene el script EnemigoIA es el enemigo, si no es el jugador
            if (GetComponent<EnemigoIA>() != null) fin.EnemigoMurio();
            else fin.JugadorMurio();
        }

        // Desactiva el objeto para que desaparezca del juego
        gameObject.SetActive(false);
    }

    // Crea una opción en el menú del Inspector para probar un golpe sin jugar
    [ContextMenu("Recibir golpe de prueba")]
    void GolpeDePrueba()
    {
        // Simula un golpe
        RecibirGolpe();
    }
}