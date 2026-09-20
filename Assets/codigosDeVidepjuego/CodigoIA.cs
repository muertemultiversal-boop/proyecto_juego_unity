using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public float velocidad = 2f;
    public float rangoAtaque = 1f;
    public float tiempoEntreGolpes = 1.5f;

    private Transform jugador;
    private Animator animador;
    private float proximoGolpe = 0f;

    private const string PARAM_CAMINANDO = "Caminando";
    private const string PARAM_ATACAR = "Atacar";

    private void Start()
    {
        animador = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (jugador != null && !jugador.gameObject.activeInHierarchy)
        {
            jugador = null;
        }

        if (jugador == null)
        {
            BuscarJugador();
        }

        if (jugador == null)
        {
            AnimarCaminar(false);
            return;
        }

        float distancia = Mathf.Abs(jugador.position.x - transform.position.x);

        if (distancia <= rangoAtaque)
        {
            AnimarCaminar(false);
            Voltear(jugador.position.x > transform.position.x ? 1f : -1f);

            if (Time.time >= proximoGolpe)
            {
                proximoGolpe = Time.time + tiempoEntreGolpes;
                if (animador != null) animador.SetTrigger(PARAM_ATACAR);
                Debug.Log("Enemigo ataca");
                QuitarVidaAlJugador();
            }
        }
        else
        {
            Mover(jugador.position.x);
        }
    }

    private void QuitarVidaAlJugador()
    {
        Vida vida = jugador.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirGolpe();
        }
    }

    private void BuscarJugador()
    {
        GameObject[] candidatos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in candidatos)
        {
            if (obj != gameObject)
            {
                jugador = obj.transform;
                Debug.Log("Enemigo encontró al jugador: " + obj.name);
                return;
            }
        }
    }

    private void Mover(float destinoX)
    {
        float diferencia = destinoX - transform.position.x;
        if (Mathf.Abs(diferencia) < 0.1f)
        {
            AnimarCaminar(false);
            return;
        }

        float direccion = diferencia > 0f ? 1f : -1f;
        transform.position += Vector3.right * direccion * velocidad * Time.deltaTime;
        Voltear(direccion);
        AnimarCaminar(true);
    }

    private void Voltear(float direccion)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direccion;
        transform.localScale = escala;
    }

    private void AnimarCaminar(bool valor)
    {
        if (animador != null) animador.SetBool(PARAM_CAMINANDO, valor);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(rangoAtaque, -2f, 0f), transform.position + new Vector3(rangoAtaque, 2f, 0f));
        Gizmos.DrawLine(transform.position + new Vector3(-rangoAtaque, -2f, 0f), transform.position + new Vector3(-rangoAtaque, 2f, 0f));
    }
}