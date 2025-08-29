using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativePlayer : MonoBehaviour
{
    [Header("Movimento")]
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    private float speed;              // Velocidade atual baseada em walk/run
    private float moveInput;

    [Header("Pulo")]
    public float jumpForce = 7f;
    public bool isGrounded;

    [Header("Componentes")]
    public Rigidbody2D rb;

    [Header("Detecção Layers")]
    public LayerMask groundLayer;     // Layer do chão
    public LayerMask wallLayer;       // Layer das paredes
    private bool isTouchingWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = walkSpeed; // Inicializa com velocidade de caminhada
    }

    void Update()
    {
        // Captura input horizontal
        moveInput = Input.GetAxis("Horizontal");

        // Corrida com Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Calcula velocidade horizontal
        float targetVelocityX = moveInput * speed;

        // Se estiver no ar e encostando na parede, reduz apenas levemente a velocidade
        if (!isGrounded && isTouchingWall)
        {
            targetVelocityX *= 0.7f; // Ajuste o valor entre 0 e 1 para suavidade
        }

        // Aplica velocity horizontal mantendo a gravidade no Y
        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Detecta chão
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }

        // Detecta parede lateral
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Sai do chão
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }

        // Sai da parede
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isTouchingWall = false;
        }
    }
}
