using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy1 : MonoBehaviour
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

    #endregion

    //animation
    protected Animator animator;
    //sprite mirrorer
    private bool facingright;

    private IEnemyStates currentstate;

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


        ChangeState(new IIdealState());

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        currentstate.Execute();

        ClampInScreen();

    } // update end


    //AI Movements
    public void Move()
    {
        animator.SetFloat("Velocity", 1);

        transform.Translate(GetDirection() * MoveUnitsPerSecond * Time.deltaTime);
    }




    public Vector2 GetDirection()
    {
        return facingright ? Vector2.right : Vector2.left;
    }






    #region Functions (screenclamp and mirror)

    //sprite mirrorer
    //equates for the direction the player is facing
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingright || horizontal < 0 && facingright)
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
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
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


    public void ChangeState(IEnemyStates newstate)
    {
        if(currentstate !=null)
        {
            currentstate.Exit();
        }

        currentstate = newstate;
        currentstate.Enter(this);
    }

    public Animator Animator { get {return animator;} }

}
