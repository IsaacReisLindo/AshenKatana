using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovimentPlayer : MonoBehaviour {
    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int speed = 5;


    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask GroundLayer;


    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private int movemntingHash = Animator.StringToHash("movementing");
    private int jumpingHash = Animator.StringToHash("jumping");
    private int attackingHash = Animator.StringToHash("attacking");





    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update() {
       
        Attacking();

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector2.up * 350);
        }
        isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.2f, GroundLayer);


        animator.SetBool(movemntingHash, horizontalInput != 0);
        animator.SetBool(jumpingHash, !isGrounded);


        if (horizontalInput > 0) {
            spriteRenderer.flipX = false;
        } else if (horizontalInput < 0) {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }


    private void Attacking() {
       if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger(attackingHash);
        }
    }
}
