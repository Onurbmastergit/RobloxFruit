using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float vida = 3;

    public float velocidade = 0;
    bool chegouAoDestino = true;

    bool rondarArea = true;
    bool seguirJogador = false;

    Vector3 destino;

    Rigidbody rb;

    private void Start()
    {
        destino = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (seguirJogador == true) 
        {
            Vector3 positionPlayer = GameObject.FindWithTag("Player").transform.position;
            transform.position = Vector3.MoveTowards(transform.position, positionPlayer, velocidade * Time.deltaTime);
        }
        if (rondarArea == true) 
        {
            if (chegouAoDestino)
            {
                float posicaoX = Random.Range(transform.position.x - 50, transform.position.x + 50);
                float posicaoZ = Random.Range(transform.position.z - 50, transform.position.z + 50);
                destino = new Vector3(posicaoX, transform.position.y, posicaoZ);
                Invoke("DesabilitaChegouAoDestino", 2f);
            }

            if (!chegouAoDestino)
            {
                transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                chegouAoDestino = true;
            }
        }
    }

    public void Damage(int valor)
    {
        vida -= valor;
        VerificarMorte();
    }

    void VerificarMorte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            chegouAoDestino = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            rondarArea = false;
            seguirJogador = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            rondarArea = true;
            seguirJogador = false;
        }
    }

    void DesabilitaChegouAoDestino()
    {
        chegouAoDestino = false;
    }
    void GeradorDeDestino() 
    {

    }
}