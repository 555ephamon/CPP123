using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 1.0f; // Time in seconds between shots
    [SerializeField] private float detectionRange = 5;
    private float timeSinceLastShot = 0.0f;
    private Shoot shoot;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
       
        sr = GetComponent<SpriteRenderer>();

        shoot = GetComponent<Shoot>();

        if (fireRate <= 0)
        {
            fireRate = 1.0f; // Default fire rate if not set
            Debug.LogWarning("TurretEnemy: Fire rate not defined - setting to a default of 1 second");
        }

        shoot.OnShotFired += (velocity) => timeSinceLastShot = 0.0f; // Subscribe to the OnShotFired event
    }

    // Update is called once per frame
    void Update()
    { 
        GameObject player  = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 currentPos = transform.position;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        float distanceX = Mathf.Abs(player.transform.position.x - currentPos.x);

        if (stateInfo.IsName("Idle"))
        {
            timeSinceLastShot += Time.deltaTime;

            if(distanceX <= detectionRange && player.transform.position.x > currentPos.x)
            {
                if (timeSinceLastShot >= fireRate)
                {
                    anim.SetTrigger("fire");
                    timeSinceLastShot = 0.0f;
                    sr.flipX = false;
                }
            }

            else if (distanceX <= detectionRange && player.transform.position.x < currentPos.x)
            {
                if (timeSinceLastShot >= fireRate)
                {
                    anim.SetTrigger("fire");
                    timeSinceLastShot = 0.0f;
                    sr.flipX = true;
                }
            }
        }
    }
}
