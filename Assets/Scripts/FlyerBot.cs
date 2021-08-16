using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerBot : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public int maxHealth = 100;
    private int currentHealth;
    [HideInInspector]
    public float speed;
    private Rigidbody2D myBody;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
