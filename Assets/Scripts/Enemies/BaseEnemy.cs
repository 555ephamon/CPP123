using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(Animator))]
public class BaseEnemy : MonoBehaviour
{
    //private - private to the class only
    //public - public to all classes
    //protected - protected to the class and ANY derived (children) classes

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
        {
            maxHealth = 3;

            Debug.LogWarning("BaseEnemy: maxHealth not defined - setting to default of 3");        
        }
        health = maxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        health -= damage;
        if(health <=0)
        {
            anim.SetTrigger("death");
            //destroy the gameObject after a certain period of time to allow the death animation to play

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0.5f);
            }
            else
            {
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
 public enum DamageType
    {
        Default,
        JumpedOn,
    }