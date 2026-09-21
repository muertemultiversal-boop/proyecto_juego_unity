using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUI : MonoBehaviour
{
    // Lista de imágenes de la barra, de llena a vacía (se llenan en el Inspector)
    public Sprite[] sprites;

    // Guarda el componente Image de la barra, que es el que muestra el dibujo
    private Image imagen;

    // Se ejecuta una sola vez, cuando el objeto se crea
    void Awake()
    {
        // Toma el componente Image que está en este mismo objeto
        imagen = GetComponent<Image>();
    }

    // Cambia la imagen de la barra según los golpes recibidos
    public void ActualizarBarra(int golpesRecibidos)
    {
        // Si la imagen todavía no se ha tomado, la busca aquí
        if (imagen == null) imagen = GetComponent<Image>();

        // Mathf.Clamp mantiene el número entre 0 y el último sprite de la lista
        // Así nunca se pide una imagen que no existe
        int indice = Mathf.Clamp(golpesRecibidos, 0, sprites.Length - 1);

        // Cambia el dibujo de la barra por el sprite que le toca
        imagen.sprite = sprites[indice];
    }
}
