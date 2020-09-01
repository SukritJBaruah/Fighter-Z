using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_big_blast : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    public GameObject expPrefab;

    private Rigidbody2D myrigidbody;
    Animator animator;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Play(AudioClipName.bigbang_fire);
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
        Vector3 expSpawn = transform.position;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player_big_blast"))
        {
            myrigidbody.velocity = direction * 0;
            animator.SetBool("ToDestroy", true);
            expSpawn.y += 0.55f;
            GameObject tmp = (GameObject)Instantiate(expPrefab, expSpawn, Quaternion.identity);
            StartCoroutine(Death(0.266f));
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
