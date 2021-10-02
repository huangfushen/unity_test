using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerStay2D (Collider2D collision)
    {
        //获取主角对象
        RubyControl rubyController = collision.GetComponent<RubyControl>();
        // 判断是否获取到了主角的对象
        if (rubyController != null)
        {
            // 当主角的血量小于最大血量是才执行
            if (rubyController.Health < rubyController.maxHealth)
            {
                Debug.Log("吨吨吨，血量加1");
                rubyController.ChangeHealth(1);
                Destroy(gameObject); 
            }

           
        }
    }
}
