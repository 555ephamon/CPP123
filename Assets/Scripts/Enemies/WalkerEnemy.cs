using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkerEnemy : BaseEnemy
{
    [SerializeField] private float xVel = 2.0f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("KoopaWalk"))
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("KoopaWalk"))
            {
                //if(sr.flipX)
                //{
                //    rb.linearVelocityX =-xVel;
                //}
                //else
                //{
                //    rb.linearVelocityX = xVel;
                //}

                //Ternary operator allows for above if else statement in one line:
                //essentially, if true we go in one direction, if not, we got he other direction

                rb.linearVelocityX = sr.flipX ? -xVel : xVel;

            }
        }
    }

    public override void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("KoopaDeath") || stateInfo.IsName("KoopaSquish"))
        {
            //if the enemy is already in death animation then ignore further damage
            return;
        }

        if (damageType == DamageType.JumpedOn)
        {
            //logic for disabling movement or other death functions can be added here
            anim.SetTrigger("squish");
            //destroy the gameObject after a certain period of time to allow the squish animation to play
            Destroy(transform.parent.gameObject, 0.5f); 
            //exit method to prevent further damage
            return;
        }
        else
        {
            anim.SetTrigger("death");
        }

        base.TakeDamage(damage, damageType);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("turn");
            sr.flipX = !sr.flipX;
        }
    }
}
