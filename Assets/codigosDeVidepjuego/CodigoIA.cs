using UnityEngine; // Trae las herramientas de Unity (MonoBehaviour, Transform, Animator, etc.)

public class EnemigoIA : MonoBehaviour // Nuestro script; "MonoBehaviour" permite ponerlo en un objeto del juego
{
    public float velocidad = 2f; // Qué tan rápido camina el enemigo (se puede cambiar en el Inspector)
    public float rangoAtaque = 1f; // Distancia a la que el enemigo se detiene y golpea
    public float tiempoEntreGolpes = 1.5f; // Segundos que espera entre un golpe y el siguiente

    private Transform jugador; // Aquí guardamos la posición del jugador para saber siempre dónde está
    private Animator animador; // Referencia al Animator para poder activar las animaciones
    private float proximoGolpe = 0f; // Momento (en segundos del juego) en que ya podrá volver a golpear

    private const string PARAM_CAMINANDO = "Caminando"; // Nombre EXACTO del parámetro bool del Animator
    private const string PARAM_ATACAR = "Atacar"; // Nombre EXACTO del parámetro trigger del Animator

    private void Start() // Se ejecuta una sola vez, cuando el enemigo aparece
    {
        animador = GetComponentInChildren<Animator>(); // Busca el Animator en este objeto (o en sus hijos)
    }

    private void Update() // Se ejecuta en cada frame del juego
    {
        if (jugador != null && !jugador.gameObject.activeInHierarchy) // Si el jugador guardado fue desactivado (por ejemplo al cambiar de personaje)...
        {
            jugador = null; // ...lo olvidamos para buscar al jugador correcto
        }

        if (jugador == null) // Si todavía no tenemos al jugador guardado...
        {
            BuscarJugador(); // ...lo buscamos
        }

        if (jugador == null) // Si aun así no lo encontramos (no hay jugador en la escena)...
        {
            AnimarCaminar(false); // ...el enemigo se queda quieto (Idle)
            return; // Termina aquí; no ejecuta el resto del Update
        }

        float distancia = Mathf.Abs(jugador.position.x - transform.position.x); // Mide qué tan lejos está el jugador solo en horizontal (ignora la altura)

        if (distancia <= rangoAtaque) // Si el jugador está lo bastante cerca para golpearlo...
        {
            AnimarCaminar(false); // Deja de caminar (vuelve a Idle)
            Voltear(jugador.position.x > transform.position.x ? 1f : -1f); // Se gira para mirar al jugador

            if (Time.time >= proximoGolpe) // Si ya pasó el tiempo de espera desde el último golpe...
            {
                proximoGolpe = Time.time + tiempoEntreGolpes; // Calcula cuándo podrá golpear otra vez
                if (animador != null) animador.SetTrigger(PARAM_ATACAR); // Activa la animación de ataque
                Debug.Log("Enemigo ataca al jugador"); // Mensaje en la Console (aquí luego va el daño real)
            }
        }
        else // Si el jugador está lejos (sea cuanto sea)...
        {
            Mover(jugador.position.x); // ...lo persigue caminando hacia su posición en X
        }
    }

    // Busca al jugador por su tag, ignorando al propio enemigo
    private void BuscarJugador()
    {
        GameObject[] candidatos = GameObject.FindGameObjectsWithTag("Player"); // Lista de todos los objetos activos con tag "Player"
        foreach (GameObject obj in candidatos) // Revisa uno por uno
        {
            if (obj != gameObject) // Si NO es el propio enemigo...
            {
                jugador = obj.transform; // ...ese es el jugador, lo guardamos
                Debug.Log("Enemigo encontró al jugador: " + obj.name); // Muestra en la Console el nombre del objeto que está siguiendo
                return; // Ya lo encontramos, salimos de la función
            }
        }
    }

    // Mueve al enemigo hacia una posición X y lo voltea
    private void Mover(float destinoX)
    {
        float diferencia = destinoX - transform.position.x; // Cuánto le falta para llegar (positivo = el destino está a la derecha)
        if (Mathf.Abs(diferencia) < 0.1f) // Si ya está casi exactamente en ese punto...
        {
            AnimarCaminar(false); // ...deja de caminar
            return; // ...y no se mueve más (evita que tiemble de un lado a otro)
        }

        float direccion = diferencia > 0f ? 1f : -1f; // 1 = derecha, -1 = izquierda
        transform.position += Vector3.right * direccion * velocidad * Time.deltaTime; // Lo desplaza según velocidad y tiempo del frame
        Voltear(direccion); // Gira el sprite hacia donde camina
        AnimarCaminar(true); // Activa la animación de caminar
    }

    // Gira el sprite según la dirección (1 = derecha, -1 = izquierda)
    private void Voltear(float direccion)
    {
        Vector3 escala = transform.localScale; // Copia el tamaño actual del objeto
        escala.x = Mathf.Abs(escala.x) * direccion; // Deja el tamaño positivo y le pone el signo de la dirección
        transform.localScale = escala; // Aplica el nuevo tamaño (esto voltea el dibujo)
    }

    // Enciende o apaga la animación de caminar
    private void AnimarCaminar(bool valor)
    {
        if (animador != null) animador.SetBool(PARAM_CAMINANDO, valor); // Cambia el bool "Caminando" (solo si hay Animator)
    }

    // Dibuja líneas rojas en la ventana Scene para ver el rango de ataque (solo cuando el enemigo está seleccionado)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Color rojo para el rango de ataque
        Gizmos.DrawLine(transform.position + new Vector3(rangoAtaque, -2f, 0f), transform.position + new Vector3(rangoAtaque, 2f, 0f)); // Línea vertical a la derecha
        Gizmos.DrawLine(transform.position + new Vector3(-rangoAtaque, -2f, 0f), transform.position + new Vector3(-rangoAtaque, 2f, 0f)); // Línea vertical a la izquierda
    }
}