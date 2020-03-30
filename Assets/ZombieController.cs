using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    public float health = 100f;
    public Transform target;
    public float engaugeDistance = 10f;
    public float attackDistance = 3f;
    
    [SerializeField]
    public float moveSpeed = 5f;
    private bool facingLeft = true;
    private Animator anim;
    // public NinjaController ninjaController;
    public float attackDamage = 2f;
    // public SpriteRenderer healthBar;
    Rigidbody2D rb;
    Vector3 localScale;
    // Animator anim;
    // Use gameObject for initialization
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsWalking", false);

        if (Vector3.Distance(target.position, gameObject.transform.position) < engaugeDistance)
        {
            anim.SetBool("IsIdle", false);

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);

            //get the direction of the target
            Vector3 direction = target.position - gameObject.transform.position;

            if (Mathf.Sign(direction.x) == 1 && facingLeft)
            {
                Flip();
            }
            else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
            {
                Flip();
            }

            if (direction.magnitude >= attackDistance)
            {
                anim.SetBool("IsWalking", true);
                // rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
                Debug.DrawLine(target.transform.position, gameObject.transform.position, Color.yellow);

                if (facingLeft)
                {
                    gameObject.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
                    // rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
                }
                else if (!facingLeft)
                {
                    gameObject.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
                    // rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
                }
            }
            if (direction.magnitude < attackDistance)
            {
                Debug.DrawLine(target.position, gameObject.transform.position, Color.red);
                anim.SetBool("IsAttacking", true);
                // ninjaController.GetComponentInChildren<PlayerHealth>().curHealth -= attackDamage;
            }
        }
        else if (Vector3.Distance(target.position, gameObject.transform.position) > engaugeDistance)
        {
            //do nothing
            Debug.DrawLine(target.position, gameObject.transform.position, Color.green);
        }
    }
    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Bullet")
    //     {
    //         health -= 10;
    //         healthBar.GetComponent<Transform>().localScale -= new Vector3(.1f, 0, 0);

    //         if (health <= 0)
    //         {
    //             anim.SetBool("IsDead", true);
    //             Destroy(gameObject, 2);
    //         }
    //     }
    // }
}