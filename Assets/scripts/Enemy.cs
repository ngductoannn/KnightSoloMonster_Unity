using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    public void TakeDamage()
    {
        sr.color = Color.red;

        Invoke(nameof(RestColor), 2);
        
    }

    private void RestColor()
    {
        sr.color = Color.white;
    }
}
