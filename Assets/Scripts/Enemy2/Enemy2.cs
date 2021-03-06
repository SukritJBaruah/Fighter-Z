﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
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
    float meleeRange = 0.5f;

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
    public const float MoveUnitsPerSecond = 0.8f;
    const float verticalmultiplier = 0.6f;
    //run variables
    const float RunUnitsPerSecond = 2f;
    bool isRunning = false;
    float lastPressedRight = -999f;
    bool wasRightPressed = false;
    float rundirection = 0f;

    #endregion

    [SerializeField]
    public GameObject blastPrefab;

    //attack punch
    public BoxCollider2D enemy_punch;

    //damage
    int hitstaken = 0;
    BoxCollider2D colliderhitbox;

    //enemy1 stats
    public static float health;
    public static float energy;

    bool isDead = false;
    float dietimer = 0f;

    //animation
    protected Animator animator;
    //sprite mirrorer
    public bool facingright;
    private bool playerup;

    private IEnemy2States currentstate;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        health = 500;
        energy = 500;

        facingright = true;
        // save for efficiency
        colliderhitbox = GetComponent<BoxCollider2D>();
        colliderHalfWidth = colliderhitbox.size.x / 2;
        colliderHalfHeight = colliderhitbox.size.y / 2;
        animator = GetComponent<Animator>();


        enemy_punch.enabled = false;


        ChangeState(new IIdeal2State());

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("player_punch") && hitstaken!=2)
        {
            enemy_punch.enabled = false;
            health -= 30f;
            hitstaken += 1;
            animator.SetFloat("Damage", 30);
            //print("damage");
        }
        else if(other.gameObject.CompareTag("player_punch") && hitstaken == 2)
        {
            enemy_punch.enabled = false;
            health -= 30f;
            hitstaken = 0;
            animator.SetFloat("Damage", 90);
            StartCoroutine(collidertoggle(0));
            StartCoroutine(collidertoggle(4));
            //print("fall");
        }

        if(other.gameObject.CompareTag("player_kick"))
        {
            enemy_punch.enabled = false;
            health -= 80f;
            hitstaken = 0;
            animator.SetFloat("Damage", 80);
            StartCoroutine(collidertoggle(0));
            StartCoroutine(collidertoggle(4));
        }

        if (other.gameObject.CompareTag("Player_blast"))
        {
            enemy_punch.enabled = false;
            health -= 40f;
            hitstaken = 0;
            animator.SetFloat("Damage", 80);
            StartCoroutine(collidertoggle(0));
            StartCoroutine(collidertoggle(4));
            //print("Blast fall");
        }

        if (other.gameObject.CompareTag("Player_big_blast"))
        {
            enemy_punch.enabled = false;
            health -= 180f;
            hitstaken = 0;
            animator.SetFloat("Damage", 180);
            StartCoroutine(collidertoggle(0));
            StartCoroutine(collidertoggle(4));
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player_punch") || other.gameObject.CompareTag("player_kick") || other.gameObject.CompareTag("Player_blast") || other.gameObject.CompareTag("Player_big_blast"))
        {
            animator.SetFloat("Damage", 0);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy2_blast"))
        {
            animator.SetBool("isblastanim", false);
        }

        //energy regen
        if (energy < 500)
        {
            energy += 0.02f;
        }

        if (health > 0)
        {
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_fall"))
            {
                currentstate.Execute();
            }
        }
        else if(health <= 0 && isDead == false)
        {
            animator.SetFloat("Damage", 90);
            isDead = true;

        }

        if(isDead == true)
        {
            dietimer += Time.deltaTime;
            if(dietimer >=3)
            {
                Destroy(this.gameObject);
            }    

        }

        //print(health);


        ClampInScreen();

    } // update end

    #region Move, LocatePlayer
    //AI Movements
    public void Move()
    {
        animator.SetFloat("Velocity", 1);

        transform.Translate(GetDirection() * MoveUnitsPerSecond * Time.deltaTime);
        transform.Translate(GetUpDown() * MoveUnitsPerSecond * Time.deltaTime * verticalmultiplier);
    }

    public void LocatePlayer()
    {
        //print("locating");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 loc = player.transform.position;
        if (loc.x - this.transform.position.x > 0)
        {
            if(!facingright)
            {
                Flip();
            }
        }
        else if (loc.x - this.transform.position.x < 0)
        {
            if(facingright)
            {
                Flip();
            }
        }

        if (loc.y - this.transform.position.y > 0)
        {
            playerup = true;
        }
        else
            playerup = false;
    }

    private Vector2 GetUpDown()
    {
        if (playerup == true)
        {
            return Vector2.up;
        }
        else
            return Vector2.down;
    }

    public Vector2 GetDirection()
    {
        return facingright ? Vector2.right : Vector2.left;
    }

    #endregion



    #region attack1
    public bool InMeleeRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>().meleeRange 
            && Math.Abs(transform.position.y - player.transform.position.y) <= GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>().meleey)
        {
            return true;
        }

        return false;
    }

    #endregion


    #region damage and death func

    IEnumerator collidertoggle(float time)
    {
        yield return new WaitForSeconds(time);

        colliderhitbox.enabled = !colliderhitbox.enabled;
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }

    #endregion


    public IEnumerator blast(int value)
    {
        int x = value;

        if(energy>=70)
        {
            energy -= 70;
            while (x > 0)
            {
                Vector3 blastSpawn = transform.position;
                System.Random rnd = new System.Random();

                if (facingright)
                {

                    blastSpawn.x += 0.15f;
                    blastSpawn.y -= 0.06f;
                    blastSpawn.y += ((float)(rnd.Next(0, 11) - rnd.Next(0, 11)) / 100);
                    GameObject tmp = (GameObject)Instantiate(blastPrefab, blastSpawn, Quaternion.identity);
                    tmp.GetComponent<enemy_blast>().Initialize(Vector2.right);
                }
                else
                {
                    blastSpawn.x -= 0.15f;
                    blastSpawn.y -= 0.06f;
                    blastSpawn.y += ((float)(rnd.Next(0, 11) - rnd.Next(0, 11)) / 100);
                    GameObject tmp = (GameObject)Instantiate(blastPrefab, blastSpawn, Quaternion.Euler(new Vector3(0, 0, -180)));
                    tmp.GetComponent<enemy_blast>().Initialize(Vector2.left);
                }

                x -= 1;
                yield return new WaitForSecondsRealtime(0.291f);
            }

        }
    }

    public bool ifELeft()
    {
        if (energy >= 70)
        {
            return true;
        }
        else
            return false;
    }


    #region Functions (screenclamp and mirror)

    //sprite mirrorer
    //equates for the direction the player is facing
    private void Flip()
    {
        facingright = !facingright;
        transform.localScale = new Vector3(transform.localScale.x * -1,1,1);

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


    public void ChangeState(IEnemy2States newstate)
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
