using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class    PantallaDeCarga : MonoBehaviour
{
    public float tiempoEspera = 2.5f;

    void Start()
    {
        StartCoroutine(Cargar());
    }

    IEnumerator Cargar()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(GameData.escenaDestino);
    }
}