using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_SortingOrder : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
