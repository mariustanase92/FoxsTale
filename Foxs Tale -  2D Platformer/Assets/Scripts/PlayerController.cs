using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;

    private bool _isGrounded;
    public bool _isOnTopOfEnemy;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    [SerializeField] private LayerMask _whatIsEnemy;

    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;

    public float knockBackLength;
    public Vector2 knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!PauseMenu.Instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
                _isOnTopOfEnemy = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, _whatIsEnemy);

                if (_isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (_isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.Instance.PlaySFX(10);
                    }

                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.Instance.PlaySFX(10);
                        }
                    }
                }

                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                  theRB.velocity = new Vector2(-knockBackForce.x, knockBackForce.y);
                 }
                else
                {
                  theRB.velocity = new Vector2(knockBackForce.x, knockBackForce.y);
                }

            }  
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", _isGrounded);
    }

    public void KnockBack( )
    {
        knockBackCounter = knockBackLength;
        anim.SetTrigger("isHurt");

        //if (other.transform.position.x > transform.position.x)
           // theRB.velocity = knockBackForce * new Vector2(-1f, 1f);
        //else
           // theRB.velocity = knockBackForce;
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.Instance.PlaySFX(10);
    }

    public void StopMoving()
    {
        theRB.velocity = new Vector2(0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(this.gameObject != null)
        {
            if (other.gameObject.tag == "Platform")
            {
                transform.parent = other.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(this.gameObject != null)
        {
            if (other.gameObject.tag == "Platform")
            {
                transform.parent = null;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

 
}
