using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 5;
    public BarraVidaUI barra;

    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        if (barra != null) barra.ActualizarBarra(0);
    }

    public void RecibirGolpe()
    {
        if (vidaActual <= 0) return;

        vidaActual--;
        if (barra != null) barra.ActualizarBarra(vidaMaxima - vidaActual);
        Debug.Log(gameObject.name + " le quedan " + vidaActual + " vidas");

        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        Debug.Log(gameObject.name + " fue derrotado");

        FinDePartida fin = FindFirstObjectByType<FinDePartida>();
        if (fin != null)
        {
            if (GetComponent<EnemigoIA>() != null) fin.EnemigoMurio();
            else fin.JugadorMurio();
        }

        gameObject.SetActive(false);
    }

    [ContextMenu("Recibir golpe de prueba")]
    void GolpeDePrueba()
    {
        RecibirGolpe();
    }
}