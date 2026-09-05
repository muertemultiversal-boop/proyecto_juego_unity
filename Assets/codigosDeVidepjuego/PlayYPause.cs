using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject panelPausa; // arrastras aqui tu Panel de pausa

    public void PausarJuego()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }
}
