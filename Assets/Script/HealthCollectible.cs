using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("发生了碰撞");
    }
}
