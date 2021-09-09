using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyControl : MonoBehaviour
{
    // Start is called before the first frame update
    //游戏运行开始前调用的方法
    void Start()
    {
        
    }

    // Update is called once per frame
    //  
    void Update()
    {
      Vector2 position = transform.position;
      float speed = 0.1f;
      if (position.x >= 3)
      {
        speed = -0.1f;
      }
      position.x = position.x + speed;
  
      transform.position  = position;
    }
}
