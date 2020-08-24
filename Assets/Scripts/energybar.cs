using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energybar : MonoBehaviour
{
    Vector3 localscale;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localscale.x = Sukrit.energy / 500;
        if (localscale.x <= 0)
        {
            localscale.x = 0;
        }
            transform.localScale = localscale;

    }
}
