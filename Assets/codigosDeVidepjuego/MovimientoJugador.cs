using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Qué tan rápido camina el personaje
    public float velocidad = 5f;

    // Tamaño del círculo con el que el golpe detecta a quién le pega
    public float rangoGolpe = 0.5f;

    // Segundos que espera el golpe después de iniciar la animación de ataque
    public float retrasoGolpe = 0.3f;

    // Indican si el personaje está caminando a la izquierda o a la derecha
    private bool moviendoIzquierda = false;
    private bool moviendoDerecha = false;

    // Cuerpo del personaje, que es el que se mueve con la física
    private Rigidbody2D rb;

    // Controla las animaciones del personaje
    private Animator animator;

    // Se ejecuta una sola vez, cuando empieza el juego
    void Start()
    {
        // Toma el Rigidbody2D y el Animator que están en este mismo objeto
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Se ejecuta en cada cuadro del juego
    void Update()
    {
        // Guarda el tamaño del personaje sin el signo (siempre positivo)
        float tamano = Mathf.Abs(transform.localScale.x);

        if (moviendoIzquierda)
        {
            // Camina a la izquierda y mantiene su velocidad vertical (caída o salto)
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);

            // Tamaño negativo en X: el dibujo mira a la izquierda
            transform.localScale = new Vector3(-tamano, transform.localScale.y, transform.localScale.z);
        }
        else if (moviendoDerecha)
        {
            // Camina a la derecha y mantiene su velocidad vertical
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);

            // Tamaño positivo en X: el dibujo mira a la derecha
            transform.localScale = new Vector3(tamano, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Si no se presiona ningún botón, se queda quieto en horizontal
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // Está caminando si se mueve en cualquiera de las dos direcciones
        bool caminando = moviendoIzquierda || moviendoDerecha;

        // Activa o desactiva la animación de caminar
        animator.SetBool("Caminando", caminando);
    }

    // El botón izquierdo llama a esta función cuando se presiona
    public void EmpezarIzquierda()
    {
        moviendoIzquierda = true;
    }

    // El botón izquierdo llama a esta función cuando se suelta
    public void TerminarIzquierda()
    {
        moviendoIzquierda = false;
    }

    // El botón derecho llama a esta función cuando se presiona
    public void EmpezarDerecha()
    {
        moviendoDerecha = true;
    }

    // El botón derecho llama a esta función cuando se suelta
    public void TerminarDerecha()
    {
        moviendoDerecha = false;
    }

    // El botón de atacar llama a esta función
    public void Atacar()
    {
        // Si ya está en plena animación de ataque, no vuelve a atacar
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Ataque"))
        {
            return;
        }

        // Activa la animación de ataque
        animator.SetTrigger("Atacar");

        // Espera un momento y luego ejecuta Golpear, para que el daño coincida con la animación
        Invoke("Golpear", retrasoGolpe);
    }

    // Detecta a quién le pega el personaje y le quita vida
    void Golpear()
    {
        // Decide el lado del golpe: 1 si mira a la derecha y -1 si mira a la izquierda
        float lado = 1;
        if (transform.localScale.x < 0)
        {
            lado = -1;
        }

        // Calcula el punto donde pega, un poco delante del personaje
        Vector2 punto = new Vector2(transform.position.x + lado * rangoGolpe, transform.position.y);

        // Guarda en una lista todo lo que esté dentro de un círculo en ese punto
        Collider2D[] golpeados = Physics2D.OverlapCircleAll(punto, rangoGolpe);

        // Revisa uno por uno lo que golpeó
        foreach (Collider2D objeto in golpeados)
        {
            // Toma el script Vida del objeto golpeado
            Vida vida = objeto.GetComponent<Vida>();

            // Si tiene vida y no es el mismo personaje, le resta un golpe
            if (vida != null && objeto.gameObject != gameObject)
            {
                vida.RecibirGolpe();
            }
        }
    }
}