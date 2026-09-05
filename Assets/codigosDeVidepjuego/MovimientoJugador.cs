using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Qué tan rápido se mueve el personaje hacia los lados.
    // Puedes cambiar este número en el Inspector para ajustar la velocidad.
    public float velocidad = 5f;

    // Qué tan fuerte salta el personaje hacia arriba.
    public float fuerzaSalto = 7f;

    // Estas 2 variables guardan si el jugador está tocando
    // el botón izquierda o derecha AHORA MISMO.
    private bool moviendoIzquierda = false;
    private bool moviendoDerecha = false;

    // Aquí vamos a guardar una referencia al Rigidbody 2D
    // del personaje (el componente que le da física/gravedad).
    private Rigidbody2D rb;

    // Start() se ejecuta UNA sola vez, al arrancar la escena.
    void Start()
    {
        // Buscamos el Rigidbody 2D que ya está puesto en este mismo
        // GameObject (el personaje), y lo guardamos en "rb".
        rb = GetComponent<Rigidbody2D>();
    }

    // Update() se ejecuta constantemente, muchas veces por segundo,
    // mientras el juego está corriendo.
    void Update()
    {
        // Si el botón izquierda está siendo presionado ahora...
        if (moviendoIzquierda)
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
        }
        // Si no era izquierda, revisamos si es derecha...
        else if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
        }
        // Si no se está tocando ningún botón de movimiento...
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // Se llama cuando el jugador PRESIONA el botón izquierda.
    public void EmpezarAMoverIzquierda()
    {
        moviendoIzquierda = true;
    }

    // Se llama cuando el jugador SUELTA el botón izquierda.
    public void DejarDeMoverIzquierda()
    {
        moviendoIzquierda = false;
    }

    // Se llama cuando el jugador PRESIONA el botón derecha.
    public void EmpezarAMoverDerecha()
    {
        moviendoDerecha = true;
    }

    // Se llama cuando el jugador SUELTA el botón derecha.
    public void DejarDeMoverDerecha()
    {
        moviendoDerecha = false;
    }

    // El salto es diferente a los otros: se ejecuta UNA sola vez
    // apenas tocas el botón.
    public void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }
}