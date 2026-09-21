using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDePartida : MonoBehaviour
{
    public GameObject panelGanaste;
    public GameObject panelPerdiste;
    public GameObject panelEmpate;
    public float tiempoEspera = 0.5f;
    public float tiempoVolver = 3f;

    private bool jugadorMurio;
    private bool enemigoMurio;
    private bool esperando;

    public void JugadorMurio()
    {
        jugadorMurio = true;
        Empezar();
    }

    public void EnemigoMurio()
    {
        enemigoMurio = true;
        Empezar();
    }

    private void Empezar()
    {
        if (esperando) return;
        esperando = true;
        StartCoroutine(Terminar());
    }

    private IEnumerator Terminar()
    {
        yield return new WaitForSecondsRealtime(tiempoEspera);

        if (jugadorMurio && enemigoMurio) panelEmpate.SetActive(true);
        else if (enemigoMurio) panelGanaste.SetActive(true);
        else panelPerdiste.SetActive(true);

        yield return new WaitForSecondsRealtime(tiempoVolver);
        IrAlInicio();
    }

    public void IrAlInicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Interfaz del jugador");
    }
}