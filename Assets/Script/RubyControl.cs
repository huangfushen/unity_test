using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyControl : MonoBehaviour
{
	private Rigidbody2D rigidbody2d;
	// 最大生命值
	public int maxHealth = 5;
	//当前生命值
	private int currentHealth;
	//获取当前生命值
	public int Health
	{
		get { return currentHealth; }
	}
	//速度
	public int speed = 5;
	// 无敌时间
	public float timeInvincible = 2.0f;
	// 无敌状态
	private bool isInvincible;
	// 计时器
	private float invincibleTimer;
	//动画方向
	private Vector2 lookDirection = new Vector2(1,0);
	private Animator animator;
	//定义子弹
	public GameObject projectilePrefab;
	
	// Start is called before the first frame update
    //游戏运行开始前调用的方法
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // 将当前血量赋值为最大血量
        currentHealth = maxHealth;
        // 将无敌时间计时器时间赋值为 无敌时间默认值
        invincibleTimer = timeInvincible;
		// 无敌状态
        isInvincible = true;
        // Debug.Log(currentHealth+"/"+maxHealth);
        Debug.Log("我是无敌的！！！");

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //  
    void Update()
    {
	    // 主角当前位置
      Vector2 position = transform.position;
      
      // 键盘左右键变量
	  float horizontal;
	  // 键盘上下键变量
	  float vertical;
	  //监听按键输入
	  horizontal = Input.GetAxis("Horizontal");
	  vertical = Input.GetAxis("Vertical");
	  // 保存位置
	  Vector2 move = new Vector2(horizontal,vertical);
	  //判断输入不趋近于0
	  if (!Mathf.Approximately(move.x,0) || !Mathf.Approximately(move.y,0))
	  {
		  lookDirection.Set(move.x,move.y);
		  lookDirection.Normalize();
	  }
	  animator.SetFloat("Look X",lookDirection.x);
	  animator.SetFloat("Look Y",lookDirection.y);
	  // 取模长，只要不为0，就有移动
	  animator.SetFloat("Speed",move.magnitude);
	  //改变角色位置
	  //Time.deltaTime每帧所占用的时间
	  // position.x = position.x + speed * horizontal * Time.deltaTime;
	  // position.y = position.y + speed * vertical * Time.deltaTime;
	  // 为角色赋值当前位置
      //transform.position  = position;
      position = position + move * speed * Time.deltaTime;
      //使用 rigidbody2d 定义角色当前位置  （物理系统的位移）
	  rigidbody2d.MovePosition(position);
	  
	  //监听按键事件发射子弹
	  if (Input.GetKeyDown(KeyCode.H))
	  {
		  Launch();
	  }
	  //角色无敌逻辑
	  if (isInvincible)
	  {
		  invincibleTimer = invincibleTimer - Time.deltaTime;
		  // 计时结束改变无敌状态
		  if (invincibleTimer <= 0)
		  {
			  isInvincible = false;
			  Debug.Log("糟糕！无敌时间结束了。");
		  }
	  }
    }
	//改变主角生命值方法
    public void ChangeHealth(int amount)
    {
	    //判断是否为受到伤害
	    if (amount < 0)
	    {
		    //判断是否无敌
		    if (isInvincible)
		    {
			    return;
		    }
		    animator.SetTrigger("Hit");
			// 开启无敌
		    isInvincible = true;
		    invincibleTimer = timeInvincible;
		    Debug.Log("我无敌辣！！");
	    }

	    //改变血量
	    currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
	    Debug.Log(currentHealth+"/"+maxHealth);
    }

    private void Launch()
    {
	    // 实例化一枚子弹
	    GameObject projectileObject = Instantiate(projectilePrefab,rigidbody2d.position,Quaternion.identity);
	    Projectile projectile = projectileObject.GetComponent<Projectile>();
	    projectile.Launch(lookDirection,300);
	    animator.SetTrigger("Launch");
    }
}
