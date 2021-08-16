using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public int maxHealth = 100;
    private int currentHealth;
    [HideInInspector]
    public float speed;
    private Rigidbody2D myBody;
    private bool isDead;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;    
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    public void TakeDamage(int damage) {
        if (!isDead) {
            currentHealth -= damage;
            anim.SetTrigger("Hurt");
        }
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        anim.SetBool("IsDead", true);
        isDead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isDead) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            this.enabled = false;
        } 
    }
}
