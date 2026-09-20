using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUI : MonoBehaviour
{
    public Sprite[] sprites;

    private Image imagen;
    private int golpesDePrueba = 0;

    void Awake()
    {
        imagen = GetComponent<Image>();
    }

    public void ActualizarBarra(int golpesRecibidos)
    {
        if (imagen == null) imagen = GetComponent<Image>();

        int indice = Mathf.Clamp(golpesRecibidos, 0, sprites.Length - 1);
        imagen.sprite = sprites[indice];
    }

    [ContextMenu("Probar golpe")]
    void ProbarGolpe()
    {
        golpesDePrueba++;
        ActualizarBarra(golpesDePrueba);
    }
}