using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energybar : MonoBehaviour
{
    Vector3 localscale;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        localscale = transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 loc = player.transform.position;
        loc.x -= 0.25f;
        loc.y += 0.45f;
        transform.position = loc;
        localscale.x = Sukrit.energy / 500;
        if (localscale.x <= 0)
        {
            localscale.x = 0;
        }
        transform.localScale = localscale;

    }
}
