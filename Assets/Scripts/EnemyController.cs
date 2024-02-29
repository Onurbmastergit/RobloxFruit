using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float vida = 3;

    public float velocidade = 0;
    public bool cantAttack = false;
    bool chegouAoDestino = true;

    bool rondarArea = true;
    bool seguirJogador = false;

    Vector3 destino;

    Rigidbody rb;

    Animator animator;

    private void Start()
    {
        destino = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (cantAttack == true)
        {
            animator.SetBool("isAttack", true);
            return;
        }
        
        if (seguirJogador == true)
        {
            Debug.Log("Te achei");
            animator.SetBool("isWalk", true);
            Vector3 positionPlayer = GameObject.FindWithTag("Player").transform.position;
            transform.position = Vector3.MoveTowards(transform.position, positionPlayer, velocidade * Time.deltaTime);

            //OLHAR O PLAYER
            float rotacaoX = transform.rotation.x;
            transform.LookAt(positionPlayer);
            transform.rotation = Quaternion.Euler(
            rotacaoX,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z
                );

            Invoke("DesabilitaChegouAoDestino", 2f);
        }
        if (rondarArea == true)
        {
            if (chegouAoDestino)
            {
                animator.SetBool("isWalk", false);
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
        if (collider.gameObject.tag == "Player")
        {
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
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
        animator.SetBool("isWalk", true);
        transform.LookAt(destino);
    }
    void CantAttack()
    {
        animator.SetBool("isAttack", false);
        cantAttack = false;
    }
}