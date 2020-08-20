using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//run jump attack needs to be fixed

public class Sukrit : MonoBehaviour
{
    #region initial values
    // saved for efficiency
    float colliderHalfWidth;
    float colliderHalfHeight;


    //movement detection when
    bool canMove = true;

    //roll time
    float rolltime = 0f;
    bool isRolling = false;

    bool isDefending = false; //for jump to not move while defending

    //attack values
    bool isRunAttack = false;
    float runAttackTime = 0.33f;

    //jump variables
    float maxJumpHeight = 0.8f;
    float groundHeight;
    Vector3 groundPos;
    float jumpSpeed = 3.0f;
    float fallSpeed = 2.0f;
    public bool inputJump = false;
    public bool grounded = true;
    public bool isjumping = false;


    // movement support
    const float MoveUnitsPerSecond = 1;
    const float verticalmultiplier = 0.7f;
    //run variables
    const float RunUnitsPerSecond = 2f;
    bool isRunning = false;
    float lastPressedRight = -999f;
    bool wasRightPressed = false;
    float rundirection = 0f;

    //animation
    public Animator animator;
    //sprite mirrorer
    private bool facingright;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        facingright = true;
        // save for efficiency
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderHalfWidth = collider.size.x / 2;
        colliderHalfHeight = collider.size.y / 2;
        animator = GetComponent<Animator>();

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (canMove == true)
        {
            #region Movement
            //
            #region WALK
            // move based on input walking
            Vector3 position = transform.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0)
            {
                position.x += horizontalInput * MoveUnitsPerSecond * Time.deltaTime;
                animator.SetFloat("Velocity", Math.Abs(horizontalInput));
                Flip(horizontalInput);
                if (Input.GetAxis("Defend") > 0f) { goto defanim; }//go to defend while he was walking
                if (Input.GetButtonDown("Attack")) { goto attackanim; }//go to attack while he was walking
            }
            if(!isjumping) //cant move vertically while jumping
            {
                if (verticalInput != 0)
                {
                    position.y += verticalInput * MoveUnitsPerSecond * Time.deltaTime * verticalmultiplier;
                    animator.SetFloat("Velocity", Math.Abs(verticalInput));
                    if (Input.GetAxis("Defend") > 0f) { goto defanim; }//go to defend while he was walking
                    if (Input.GetButtonDown("Attack")) { goto attackanim; }//go to attack while he was walking
                }
            }

            //diagonal fix
            //needs to fix diagonal velocity

            #endregion

            #region RUN
            //run anim
            //double tap to run
            bool isRightPressed = Math.Abs(Input.GetAxis("Horizontal")) > 0.1f;
            if (isRightPressed && !wasRightPressed)
            {
                if (Time.time < lastPressedRight + 0.5f)
                { // half a second window for double-tapping
                    isRunning = true;
                    animator.SetBool("run", true);
                    rundirection = Input.GetAxis("Horizontal");
                    if (rundirection > 0)
                    {
                        rundirection = 1f;
                    }
                    else
                    {
                        if (rundirection < 0)
                        {
                            rundirection = -1f;
                        }
                    }
                    print("running"); // debug remove later
                }
                lastPressedRight = Time.time;
            }
            wasRightPressed = isRightPressed;

            if (isRunning) //everything while running
            {
                if (Math.Abs(Input.GetAxis("Horizontal")) > 0.4f) //for stopping while running
                {
                    isRunning = false;
                    animator.SetBool("run", false);
                    print("not running"); // debug remove later
                }
                else
                    position.x += rundirection * RunUnitsPerSecond * Time.deltaTime;
            }

            //RUN END
            #endregion

            // move character to new position
            transform.position = position;

            #endregion
        }

        #region Jump
        if(!isjumping)
        {
            maxJumpHeight = 0.8f;
            groundPos = transform.position;
            groundHeight = transform.position.y;
            maxJumpHeight = transform.position.y + maxJumpHeight;
        }

        if(!isDefending)
        {
            if (Input.GetButtonDown("Jump") && !isjumping)
            {
                groundPos = transform.position;
                inputJump = true;
                isjumping = true;
                animator.SetBool("jumping", true);
                StartCoroutine("Jump");
            }
        }


        #endregion


        #region defend
    defanim:
            if(Input.GetAxis("Defend")>0f && rolltime <= 0)
            {
                #region Roll if hit defend while running
                //ROLL code while running
                if (isRunning && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_roll"))
                {
                    //roll code goes here, make him untouchable

                    //end

                    //reset run anim
                    isRunning = false;
                    animator.SetBool("run", false);

                    isRolling = true;
                    animator.SetBool("roll", true);
                    rolltime = 0.75f;
                    print("rolling"); // debug
                    //rolls

                }
                #endregion
                else
                {
                    //set defend code

                    //end
                    animator.SetBool("defend", true);
                    canMove = false;
                    isDefending = true;
                }
            }
            else
            {
                animator.SetBool("defend", false);
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_attack1")) //used for attack variable
                {
                    canMove = true;
                    isDefending = false;
                }
            }

            #endregion

        #region attack
        attackanim:
        if(Input.GetButtonDown("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_RunAttack"))
        {
            #region Run_attack
            //Run attack
            if (isRunning)
            {
                //runn attack code goes here

                //end

                //reset run anim
                isRunning = false;
                animator.SetBool("run", false);

                isRunAttack = true;
                animator.SetBool("runattack", true);
                print("run attack"); // debug

            }
            #endregion
            else
            {
                //set attack code

                //end
                animator.SetBool("Attack1", true);
                canMove = false;

                print("Attack 1");
            }
        }
        else
        {
            animator.SetBool("Attack1", false);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_attack1"))
            {
                canMove = true;
            }

        }


        #endregion


        #region rolling and stop rolling
        //keep rolling
        if (isRolling)
        {
            rolltime -= Time.deltaTime;
            if (rolltime > 0f)
            {
                Vector3 position = transform.position;
                position.x += rundirection * RunUnitsPerSecond * Time.deltaTime;
                transform.position = position;
            }
            //roll stop
            if (rolltime <= 0f)
            {
                isRolling = false;
                animator.SetBool("roll", false);
            }
        }
        #endregion

        
        #region run attack
        if(isRunAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_RunAttack"))
        {
            //Hit code goes here

            //end
            runAttackTime -= Time.deltaTime;
            if (runAttackTime > 0f)
            {
                Vector3 position = transform.position;
                position.x += rundirection * RunUnitsPerSecond * Time.deltaTime;
                transform.position = position;
            }
            // stop
            if (runAttackTime <= 0f)
            {
                animator.SetBool("runattack", false);
                isRunAttack = false;
                runAttackTime = 0.33f;
            }
        }

        #endregion


        //clamp to screen
        if(!isjumping)//check for jump
        {
            ClampInScreen();
        }

    } // update end

    #region Functions (screenclamp and mirror)

    //sprite mirrorer
    //equates for the direction the player is facing
    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingright || horizontal < 0 && facingright)
        {
            facingright = !facingright;

            Vector3 mirrorscale = transform.localScale;
            mirrorscale.x *= -1;
            transform.localScale = mirrorscale;
        }
    }

    /// <summary>
    /// Clamps the character in the screen
    /// </summary>
    void ClampInScreen()
    {
        // clamp position as necessary
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (position.x + colliderHalfWidth > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenRight - colliderHalfWidth;
        }
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop )
        {
            position.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }
        transform.position = position;
    }
    #endregion


    #region jump functionality
    IEnumerator Jump()
    {
        while (true)
        {
            if (transform.position.y >= maxJumpHeight)
            {
                inputJump = false;
            }
            if (inputJump)
            {
                transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
            }
            else if (!inputJump)
            {
                transform.Translate(Vector3.down * fallSpeed * Time.smoothDeltaTime);
                if (transform.position.y < groundPos.y)
                {
                    Vector3 pos = transform.position;
                    pos.y = groundPos.y;
                    pos.x = transform.position.x;
                    transform.position = pos;
                    isjumping = false;
                    isRunning = false; // stop running after run jump
                    animator.SetBool("run", false); // stop run anim
                    animator.SetBool("jumping", false);
                    StopAllCoroutines();
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
    #endregion


}
