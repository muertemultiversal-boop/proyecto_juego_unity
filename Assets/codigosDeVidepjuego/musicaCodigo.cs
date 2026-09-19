using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // NUEVO: se ejecuta automáticamente cuando este objeto se destruye
    void OnDestroy()
    {
        // Solo nos desuscribimos si este objeto era el "Instance" activo
        // (evita problemas si se destruye un duplicado que nunca se suscribió)
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Cortamos la conexión al evento, así no queda "colgada"
            Instance = null; // Dejamos limpio el Instance para que el próximo objeto pueda tomar su lugar sin problema
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Mapa1" || scene.name == "Mapa2")
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    public static void CambiarVolumen(float valor)
    {
        if (Instance != null && Instance.audioSource != null)
        {
            Instance.SetVolume(valor);
        }
    }
}