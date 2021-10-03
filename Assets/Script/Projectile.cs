using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 当长度达到一定长时销毁子弹
        if (transform.position.magnitude > 100)
        {
            Destroy(gameObject);
        }
    }
    
    //子弹发射
    public void Launch(Vector2 direction,float force)
    {
        rigidbody2D.AddForce(direction * force);
    }
    // 子弹碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }
        Debug.Log("子弹碰撞到了"+collision.gameObject);
        Destroy(gameObject);
    }
}
