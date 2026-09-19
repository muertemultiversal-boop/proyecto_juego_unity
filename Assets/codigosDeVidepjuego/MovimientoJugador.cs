using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f; // la velocidad del stickman, se puede cambiar desde el inspector

    private bool moviendoIzquierda = false;
    private bool moviendoDerecha = false;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // esto es para que no se resetee el tamaño del personaje cuando se voltea
        float tamano = Mathf.Abs(transform.localScale.x);

        if (moviendoIzquierda)
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            transform.localScale = new Vector3(-tamano, transform.localScale.y, transform.localScale.z); // voltea para la izquierda
        }
        else if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            transform.localScale = new Vector3(tamano, transform.localScale.y, transform.localScale.z); // vuelve a mirar a la derecha
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // quieto si no toco nada
        }

        bool caminando = moviendoIzquierda || moviendoDerecha;
        animator.SetBool("Caminando", caminando); // le aviso al animator si esta caminando o no
    }

    // metodos que llaman los botones de la pantalla

    public void EmpezarIzquierda()
    {
        moviendoIzquierda = true;
    }

    public void TerminarIzquierda()
    {
        moviendoIzquierda = false;
    }

    public void EmpezarDerecha()
    {
        moviendoDerecha = true;
    }

    public void TerminarDerecha()
    {
        moviendoDerecha = false;
    }

    public void Atacar()
    {
        animator.SetTrigger("Atacar"); // activa la animacion de golpe
    }
}