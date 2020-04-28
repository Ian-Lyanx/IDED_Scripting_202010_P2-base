using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour 
{  
    private void OnCollisionEnter(Collision other)
    {       
        int layerBala = other.gameObject.layer;

        if (layerBala.Equals(Utils.LimiteBalaLayer))
        {
            gameObject.SetActive(false);
        }

        if (layerBala.Equals(Utils.TargetLayer))
        {
            gameObject.SetActive(false);
        }
    }
  
}
