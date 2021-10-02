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



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX",direction);
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //计时器（控制方向）
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = -direction;
            animator.SetFloat("MoveX",direction);
            animator.SetBool("Vertical",vertical);
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
}
