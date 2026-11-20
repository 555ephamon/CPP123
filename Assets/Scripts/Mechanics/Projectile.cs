using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5.0f;

    [SerializeField]
    private ProjectileFaction faction = ProjectileFaction.Player;

    [SerializeField]
    private int damage = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);        
    }

    // Update is called once per frame
    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    //collision detection code
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (faction == ProjectileFaction.Player && collision.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with enemy
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage, DamageType.Default);
            Destroy(gameObject);
        }
        else if (faction == ProjectileFaction.Enemy && collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with player
            collision.gameObject.GetComponent<PlayerController>().lives--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}

public enum ProjectileFaction
{
    Player,
    Enemy
}