using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float vida = 3; // Quantidade de vida do inimigo

    public float velocidade = 0; // Velocidade de movimento do inimigo
    public bool cantAttack = false; // Indica se o inimigo pode atacar

    bool chegouAoDestino = true; // Indica se o inimigo chegou ao destino de rondagem
    bool rondarArea = true; // Indica se o inimigo está rondando a área
    bool seguirJogador = false; // Indica se o inimigo está seguindo o jogador

    Vector3 destino; // O destino atual para o qual o inimigo está se movendo

    Rigidbody rb;
    Animator animator;

    private void Start()
    {
        destino = Vector3.zero; // Inicializa o destino como zero
        rb = GetComponent<Rigidbody>(); // Obtém o Rigidbody do inimigo
        animator = GetComponent<Animator>(); // Obtém o Animator do inimigo
    }

    private void FixedUpdate()
    {
        // Se o inimigo não pode atacar, define a animação de ataque e retorna
        if (cantAttack == true)
        {
            animator.SetBool("Atacando", true);
            
        }
        
        // Se o inimigo está seguindo o jogador
        if (seguirJogador == true)
        {
            Debug.Log("Te achei"); // Debug: Indica que o inimigo encontrou o jogador
            animator.SetBool("Andando", true); // Define a animação de caminhada
            Vector3 positionPlayer = GameObject.FindWithTag("Player").transform.position; // Obtém a posição do jogador
            transform.position = Vector3.MoveTowards(transform.position, positionPlayer, velocidade * Time.deltaTime); // Move-se em direção ao jogador

            // Olha na direção do jogador
            float rotacaoX = transform.rotation.x;
            transform.LookAt(positionPlayer);
            transform.rotation = Quaternion.Euler(
                rotacaoX,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z
            );

            Invoke("DesabilitaChegouAoDestino", 2f); // Invoca o método para desativar a variável de chegada ao destino após 2 segundos
        }
        // Se o inimigo está rondando a área
        if (rondarArea == true)
        {
            // Se o inimigo chegou ao destino
            if (chegouAoDestino)
            {
                animator.SetBool("Andando", false); // Define a animação de caminhada como falsa
                // Define um novo destino aleatório dentro de uma área ao redor do inimigo
                float posicaoX = Random.Range(transform.position.x - 50, transform.position.x + 50);
                float posicaoZ = Random.Range(transform.position.z - 50, transform.position.z + 50);
                destino = new Vector3(posicaoX, transform.position.y, posicaoZ);
                Invoke("DesabilitaChegouAoDestino", 2f); // Invoca o método para desativar a variável de chegada ao destino após 2 segundos
            }

            // Se o inimigo não chegou ao destino
            if (!chegouAoDestino)
            {
                // Move-se em direção ao destino
                transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
            }

            // Se a distância entre o inimigo e o destino for menor que 0.1f, o inimigo chegou ao destino
            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                chegouAoDestino = true;
            }
        }
    }

    // Método para causar dano ao inimigo
    public void Damage(int valor)
    {
        vida -= valor; // Reduz a vida do inimigo
        VerificarMorte(); // Verifica se o inimigo morreu
    }

    // Método para verificar se o inimigo morreu
    void VerificarMorte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject); // Destroi o inimigo se sua vida for menor ou igual a zero
        }
    }

    // Quando o inimigo colide com algo
    private void OnCollisionEnter(Collision collider)
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            chegouAoDestino = true; // Define que o inimigo chegou ao destino
        }
    }

    // Quando algo entra no trigger do inimigo
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rondarArea = false; // Define que o inimigo não está mais rondando a área
            seguirJogador = true; // Define que o inimigo está seguindo o jogador
        }
    }

    // Quando algo sai do trigger do inimigo
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rondarArea = true; // Define que o inimigo está rondando a área novamente
            seguirJogador = false; // Define que o inimigo não está mais seguindo o jogador
        }
    }

    // Método para desativar a variável de chegada ao destino após 2 segundos
    void DesabilitaChegouAoDestino()
    {
        chegouAoDestino = false; // Desativa a variável de chegada ao destino
        animator.SetBool("Andando", true); // Define a animação de caminhada como verdadeira
        transform.LookAt(destino); // Olha na direção do destino
    }
}