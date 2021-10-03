using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 速度
    public int speed = 2;
    //轴向
    public bool vertical;
    //方向控制
    private int direction = 1;
    //方向改变时间
    public float changeTime = 3;
    //计时器
    private float timer;
    //刚体
    private Rigidbody2D rigidbody2d;
    //动画组件
    private Animator animator;
    //修复状态
    private bool broken;



    // Start is called before the first frame update
    void Start()
    {
        broken = true;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayMoveAnimation();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        //计时器（控制方向）
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = -direction;
            PlayMoveAnimation();
            timer = changeTime;
        }
        Vector2 position = rigidbody2d.position;
        
        // 轴向控制
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        
        rigidbody2d.MovePosition(position);
    }
    
    // 碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyControl rubyControl = collision.gameObject.GetComponent<RubyControl>();
        if (rubyControl != null)
        {
            rubyControl.ChangeHealth(-1);
        }
    }

    private void PlayMoveAnimation()
    {
        if (vertical)
        {
            animator.SetFloat("MoveX",0);
            animator.SetFloat("MoveY",direction);
        }
        else
        {
            animator.SetFloat("MoveX",direction);
            animator.SetFloat("MoveY",0);
        }
    }
    // 修复机器人
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
