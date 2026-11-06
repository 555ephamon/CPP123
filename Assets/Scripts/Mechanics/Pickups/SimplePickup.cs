using UnityEngine;

public class SimplePickup : MonoBehaviour
{
    public enum PickupType
    {
        Health,
        JumpBoost,
    }

    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();

            switch (type)
            {

                case PickupType.Health:
                    //C++ style
                    //controller.SetLives(controller.GetLives() +1
                    controller.lives++;
                    break;


                case PickupType.JumpBoost:
                    controller.JumpForceChange();
                    break;
            }

            Destroy(gameObject);
        }

    }
}