using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 1.0f; // Time in seconds between shots
    private float timeSinceLastShot = 0.0f;
    private Shoot shoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        shoot = GetComponent<Shoot>();

        if (fireRate <= 0)
        {
            fireRate = 1.0f; // Default fire rate if not set
            Debug.LogWarning("TurretEnemy: Fire rate not defined - setting to a default of 1 second");
        }

        //shoot.OnShotFired += (velocity) => timeSinceLastShot = 0.0f; // Subscribe to the OnShotFired event
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= fireRate)
            {
                anim.SetTrigger("fire");
            }
        }
    }
}
