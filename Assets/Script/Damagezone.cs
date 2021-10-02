using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagezone : MonoBehaviour
{
    private void OnTriggerStay2D (Collider2D collision)
    {
        //获取主角对象
        RubyControl rubyController = collision.GetComponent<RubyControl>();
        // 判断是否获取到了主角的对象
        if (rubyController != null)
        {
            Debug.Log("被扎了，血量-1");
            rubyController.ChangeHealth(-1);
        }
    }
}
