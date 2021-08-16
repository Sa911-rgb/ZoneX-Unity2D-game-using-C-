using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;
    
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public float attackRate = 2f;

    float nextAttackTime = 0f;

    private float movementX;
    private bool isGrounded;
    private Rigidbody2D myBody;
    private Animator anim;
    private string RUN_ANIMATION = "Run";
    private string JUMP_ANIMATION = "Jump";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private bool facingRight;
    private Vector3 theScale;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        if (Time.time >= nextAttackTime) {
            Attack();
        }
    }

    private void FixedUpdate()
    {

    }

    void PlayerMoveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void AnimatePlayer() {
        if (movementX > 0 && !facingRight || movementX < 0 && facingRight)
        {
            facingRight = !facingRight;
            theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        
        if (movementX > 0 || movementX < 0)
        {
            anim.SetBool(RUN_ANIMATION, true);
            
        }
        else {
            anim.SetBool(RUN_ANIMATION, false);
        }
    }

    void PlayerJump() {
        if (isGrounded)
        {
            anim.SetTrigger(JUMP_ANIMATION);
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Attack() {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Punch");
            GiveDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("DoublePunch");
            GiveDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Kick");
            GiveDamage(15);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("Blast");
        }
    }

    void GiveDamage(int damage) {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        nextAttackTime = Time.time + 1f / attackRate;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }
        /*
        if (collision.gameObject.CompareTag(ENEMY_TAG)) {
            Destroy(gameObject);
        }
        */
    }
}
