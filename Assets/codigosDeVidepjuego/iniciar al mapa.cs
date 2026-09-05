using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class IniciarAlMapa : MonoBehaviour
{
    public TextMeshProUGUI textoAviso;

    public void IniciarJuego()
    {
        if (SeleccionarPersonaje.personajeSeleccionado == -1)
        {
            StartCoroutine(MostrarAviso());
            return;
        }

        GameData.escenaDestino = "SeleccionDelMapa";
        SceneManager.LoadScene("PantallaDeCarga");
    }

    IEnumerator MostrarAviso()
    {
        textoAviso.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoAviso.gameObject.SetActive(false);
    }

    public void IrASeleccionDePersonajes()
    {
        SceneManager.LoadScene("SeleccionDePersonajes");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

    public void IrAMapa1()
    {
        GameData.escenaDestino = "Mapa1";
        SceneManager.LoadScene("PantallaDeCarga");
    }

    public void IrAMapa2()
    {
        GameData.escenaDestino = "Mapa2";
        SceneManager.LoadScene("PantallaDeCarga");
    }
}