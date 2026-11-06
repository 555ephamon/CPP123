using UnityEngine;

//"abstract means that its components can be derived from, but it will not show up as a component itself
public abstract class Pickup : MonoBehaviour
{
    abstract public void OnPickup(GameObject interactor);
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //use || function to add more objects it can collide with
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
