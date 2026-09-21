using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    // Qué tan rápido camina el enemigo
    public float velocidad = 2f;

    // A qué distancia del jugador el enemigo se detiene y ataca
    public float rangoAtaque = 1f;

    // Segundos que espera el enemigo entre un golpe y otro
    public float tiempoEntreGolpes = 1.5f;

    // Guarda la posición del jugador al que persigue
    private Transform jugador;

    // Controla las animaciones del enemigo
    private Animator animador;

    // Momento del juego en el que el enemigo puede volver a pegar
    private float proximoGolpe = 0f;

    // Nombres de los parámetros del Animator (deben escribirse igual que en Unity)
    private const string PARAM_CAMINANDO = "Caminando";
    private const string PARAM_ATACAR = "Atacar";

    // Se ejecuta una sola vez, cuando empieza el juego
    private void Start()
    {
        // Busca el Animator que está dentro de este objeto o de sus hijos
        animador = GetComponentInChildren<Animator>();
    }

    // Se ejecuta en cada cuadro del juego
    private void Update()
    {
        // Si el jugador que tenía ya está desactivado (murió), lo olvida
        if (jugador != null && !jugador.gameObject.activeInHierarchy)
        {
            jugador = null;
        }

        // Si no tiene a quién perseguir, busca un jugador
        if (jugador == null)
        {
            BuscarJugador();
        }

        // Si sigue sin haber jugador, se queda quieto y no hace nada más
        if (jugador == null)
        {
            AnimarCaminar(false);
            return;
        }

        // Calcula qué tan lejos está el jugador (solo en el eje X, sin signo)
        float distancia = Mathf.Abs(jugador.position.x - transform.position.x);

        // Si el jugador está dentro del rango de ataque
        if (distancia <= rangoAtaque)
        {
            // Deja de caminar y mira hacia el jugador
            AnimarCaminar(false);
            Voltear(jugador.position.x > transform.position.x ? 1f : -1f);

            // Solo ataca si ya pasó el tiempo de espera entre golpes
            if (Time.time >= proximoGolpe)
            {
                // Calcula cuándo podrá volver a pegar
                proximoGolpe = Time.time + tiempoEntreGolpes;

                // Activa la animación de ataque
                if (animador != null) animador.SetTrigger(PARAM_ATACAR);

                // Muestra un mensaje en la Console para comprobar que atacó
                Debug.Log("Enemigo ataca");

                // Le quita una vida al jugador
                QuitarVidaAlJugador();
            }
        }
        else
        {
            // Si está lejos, camina hacia el jugador
            Mover(jugador.position.x);
        }
    }

    // Le hace daño al jugador usando su script Vida
    private void QuitarVidaAlJugador()
    {
        // Toma el script Vida del jugador
        Vida vida = jugador.GetComponent<Vida>();

        // Si el jugador tiene ese script, le resta una vida
        if (vida != null)
        {
            vida.RecibirGolpe();
        }
    }

    // Busca en la escena al personaje que tiene el tag "Player"
    private void BuscarJugador()
    {
        // Guarda en una lista todos los objetos con el tag "Player"
        GameObject[] candidatos = GameObject.FindGameObjectsWithTag("Player");

        // Revisa uno por uno los objetos de la lista
        foreach (GameObject obj in candidatos)
        {
            // Se salta a sí mismo, para no perseguirse a sí mismo
            if (obj != gameObject)
            {
                // Guarda al jugador encontrado y muestra su nombre en la Console
                jugador = obj.transform;
                Debug.Log("Enemigo encontró al jugador: " + obj.name);
                return;
            }
        }
    }

    // Camina hacia una posición X
    private void Mover(float destinoX)
    {
        // Calcula cuánto le falta para llegar a ese punto
        float diferencia = destinoX - transform.position.x;

        // Si ya está casi ahí, se detiene
        if (Mathf.Abs(diferencia) < 0.1f)
        {
            AnimarCaminar(false);
            return;
        }

        // Decide hacia dónde ir: 1 es a la derecha y -1 a la izquierda
        float direccion = diferencia > 0f ? 1f : -1f;

        // Se mueve en esa dirección según su velocidad
        transform.position += Vector3.right * direccion * velocidad * Time.deltaTime;

        // Gira el dibujo hacia donde camina y activa la animación de caminar
        Voltear(direccion);
        AnimarCaminar(true);
    }

    // Gira el dibujo del enemigo hacia la izquierda o la derecha
    private void Voltear(float direccion)
    {
        // Toma el tamaño actual del enemigo
        Vector3 escala = transform.localScale;

        // Cambia el signo de X: positivo mira a la derecha y negativo a la izquierda
        escala.x = Mathf.Abs(escala.x) * direccion;

        // Aplica el nuevo tamaño
        transform.localScale = escala;
    }

    // Activa o desactiva la animación de caminar
    private void AnimarCaminar(bool valor)
    {
        if (animador != null) animador.SetBool(PARAM_CAMINANDO, valor);
    }

    // Dibuja en la pestaña Scene el rango de ataque con dos líneas rojas
    // Solo se ve al seleccionar el enemigo y no aparece en el juego
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(rangoAtaque, -2f, 0f), transform.position + new Vector3(rangoAtaque, 2f, 0f));
        Gizmos.DrawLine(transform.position + new Vector3(-rangoAtaque, -2f, 0f), transform.position + new Vector3(-rangoAtaque, 2f, 0f));
    }
}