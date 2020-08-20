using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy1_spriteRenderer : MonoBehaviour
{
    public string spritesheetname;
    void lateUpdate()
    {
        var subsprites = Resources.LoadAll<Sprite>(spritesheetname);

        foreach(var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;
            var newsprite = Array.Find(subsprites, item => item.name == spriteName);

            if(newsprite)
            {
                renderer.sprite = newsprite;
            }
        }

        
    }
}
