using Unity.VisualScripting;
using UnityEngine;

//ALWAYS REQUIRE THESE COMPONENTS
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters

    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    #endregion
    #region Ground Check

   
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.01f;
    private bool isGrounded;

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    #endregion

    #region Component References

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;

    #endregion



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DECLARE
        rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(CalculateGroundCheckPos(), groundCheckRadius, groundLayer);
        }

        //FILL REFERENCE

        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");
        bool attackPressed = Input.GetButtonDown("Fire1");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (moveX != 0f) SpriteFlip(moveX);

        anim.SetBool("isGrounded", isGrounded);
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

}
