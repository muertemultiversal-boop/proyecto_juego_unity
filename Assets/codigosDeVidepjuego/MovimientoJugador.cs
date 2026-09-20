using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float rangoGolpe = 0.5f;
    public float retrasoGolpe = 0.3f;

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
        float tamano = Mathf.Abs(transform.localScale.x);

        if (moviendoIzquierda)
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            transform.localScale = new Vector3(-tamano, transform.localScale.y, transform.localScale.z);
        }
        else if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            transform.localScale = new Vector3(tamano, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        bool caminando = moviendoIzquierda || moviendoDerecha;
        animator.SetBool("Caminando", caminando);
    }

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Ataque"))
        {
            return;
        }

        animator.SetTrigger("Atacar");
        Invoke("Golpear", retrasoGolpe);
    }

    void Golpear()
    {
        float lado = 1;
        if (transform.localScale.x < 0)
        {
            lado = -1;
        }

        Vector2 punto = new Vector2(transform.position.x + lado * rangoGolpe, transform.position.y);
        Collider2D[] golpeados = Physics2D.OverlapCircleAll(punto, rangoGolpe);

        foreach (Collider2D objeto in golpeados)
        {
            Vida vida = objeto.GetComponent<Vida>();
            if (vida != null && objeto.gameObject != gameObject)
            {
                vida.RecibirGolpe();
            }
        }
    }
}