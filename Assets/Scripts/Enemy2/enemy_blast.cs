using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_blast : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myrigidbody;
    Animator animator;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        myrigidbody.velocity = direction * speed;


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player_blast"))
        {
            myrigidbody.velocity = direction * 0;
            animator.SetBool("ToDestroy", true);
            StartCoroutine(Death(0.267f));
        }

        if (other.gameObject.CompareTag("Player_big_blast"))
        {
            myrigidbody.velocity = direction * 0;
            StartCoroutine(Death(0));
        }
    }

    private void OnBecameInvisible()
    {
        //implement object pool instead of destroying
        Destroy(this.gameObject);
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
}
