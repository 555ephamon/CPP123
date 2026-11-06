using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;
    public Transform target;
    public float cameraSpeed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player==null)
            {
                Debug.LogError("No Target assigned a splayer. Please include valid reference in the camera follow script");
                return;
            }
            target = player.transform;
        }
    }

    //regular update function is specifically linked to computer's tick rate - useful for input handling/rendering
    //fixed update runs on exact fixed time (60FPS = 1/60 = 0.016666s) - useful for physics as a proper fixed update that is decoupled from rendering and allows for more consistent physics behaviour
    //late update is the last update in a fram after all fixed update and update cycles have run. Unity specifies that LateUpdate is generally useful for camera movement as it is the last thing that will happen in a frame
    
    // Update is called once per frame
    void Update()
    {
        if (target == null) //this is an exit condition is object that needs to be run does not exist
        {
            return;
        }
     //need to capture current pos in order to modify x position for this update
        Vector3 currentPos = transform.position;
    //modify to a clamp of x min and max values... if you wanted more functionality - here is where should modify camera positions
        currentPos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);

     
        transform.position = Vector3.MoveTowards(transform.position, currentPos, cameraSpeed * Time.deltaTime);
    }
}
