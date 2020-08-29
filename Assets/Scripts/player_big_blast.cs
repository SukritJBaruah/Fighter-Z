using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_big_blast : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Death(0.476f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
}
