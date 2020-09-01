using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Play(AudioClipName.explosion);
        StartCoroutine(Death(1.0f));
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
}
