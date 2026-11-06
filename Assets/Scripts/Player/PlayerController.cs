using System.Collections;
using UnityEngine;

//ALWAYS REQUIRE THESE COMPONENTS
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator), typeof(GroundCheck))]

public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumps = 2;
    public float initialPowerupDuration = 5;

  
    #endregion

    #region Getters and Setters
    [SerializeField]


    //C++ style getter/setter

    //public void SetLives(int currentLives)
    //{
    //    if (currentLives > maxLives)
    //    {
    //        lives = maxLives;
    //    }
    //    else if (currentLives < 0) 
    //    {
    //        lives = 0;
    //        //game over here
    //    }
    //    else if (lives > currentLives)
    //    {
    //        lives = currentLives;
    //        //lost a life - respawn
    //    }
    //}

    //public int GetLives()
    //{
    //    return lives;   
    //}
    private int maxLives = 9;
    private int _lives = 3; //internal lives value
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value >= maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                lives = 0;
                //game over
                Debug.Log("Game Over");
            }
            else if (lives > value)
            {
                _lives = value;
                //respawn
                Debug.Log("Respawn");
            }
            else
            {
                _lives = value;
            }
            Debug.Log($"Lives were changed to {_lives}");
        }
    } //finalized life gating

    private int _score;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log($"Lives were changed to {_lives}");
        }
    }

    #endregion


    #region Component References

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    private GroundCheck gndChk;

    #endregion

    #region Properties
    private int currentJumps = 0;
    private float currentPowerupDuration = 0;
    private float initialJumpForce;
    private float powerupJumpForce = 15;
    private Coroutine jumpForceCoroutine = null;

    
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DECLARE
        rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>();
        gndChk = GetComponent<GroundCheck>();

        gndChk.Init(bc, rb);

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        initialJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        //if (rb.linearVelocityY <= 0f)
        //{
        //    bool isGroundedthisFrame = gndChk.CheckGround();
        //}

        //FILL REFERENCE

        gndChk.CheckGround();

        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");
        bool attackPressed = Input.GetButtonDown("Fire1");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed && currentJumps < maxJumps)
        {
            currentJumps++;
            gndChk.SetGroundAfterJump();
            rb.linearVelocityY = 0f;
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }

        if (moveX != 0f) SpriteFlip(moveX);
        if (gndChk.isGrounded) currentJumps = 0;

        anim.SetBool("isGrounded", gndChk.isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));

        if (attackPressed)
        {
            anim.SetTrigger("attackPressed");
        }

        //if (attackPressed && !isGrounded)
        //{
        //    anim.SetTrigger("attackPressed");
        //}

    }
    
    //FUNCTION THAT DETECTS LEFT OR RIGHT MOVEMENT, IF RIGHT, STATEMENT IS FALSE AND SPRITE DOES NOT FLIP, OTHERWISE CHANGE TO FALSE
    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
    //{
    //    if (sr.flipX && moveX > 0f || !sr.flipX && moveX <0f)
    //    {
    //        sr.flipX = !sr.flipX;
    //   }
    //}

    public void JumpForceChange()
    {
        if (jumpForceCoroutine != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = initialJumpForce;
        }
        jumpForceCoroutine = StartCoroutine(JumpForceChangeCoroutine());

        IEnumerator JumpForceChangeCoroutine()
        {
            currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
            jumpForce = powerupJumpForce;

            while (currentPowerupDuration > 0)
            {
                currentPowerupDuration -= Time.deltaTime;
                Debug.Log($"Jump Power Up time remaining {currentPowerupDuration}");
                yield return null;
            }
            jumpForce = initialJumpForce;
            jumpForceCoroutine = null;
            currentPowerupDuration = 0f;
        }
    }
}
