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
	  //监听
	  horizontal = Input.GetAxis("Horizontal");
	  vertical = Input.GetAxis("Vertical");
	  //改变角色位置
	  //Time.deltaTime每帧所占用的时间
      position.x = position.x + speed * horizontal * Time.deltaTime;
	  position.y = position.y + speed * vertical * Time.deltaTime;
	  // 为角色赋值当前位置
      //transform.position  = position;
      
      //使用 rigidbody2d 定义角色当前位置  （物理系统的位移）
	  rigidbody2d.MovePosition(position);
	  
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
		    Debug.Log(isInvincible);
		    //判断是否无敌
		    if (isInvincible)
		    {
			    return;
		    }
			// 开启无敌
		    isInvincible = true;
		    invincibleTimer = timeInvincible;
		    Debug.Log("我无敌辣！！");
	    }

	    //改变血量
	    currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
	    Debug.Log(currentHealth+"/"+maxHealth);
    }
}
