using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    float health = 100f;
    public float hurtBloodPoint=20f;
    SpriteRenderer healthbar;
    Vector3 healthbarScale;
    void Start()
    {
        healthbar = GameObject.Find("Health").GetComponent<SpriteRenderer>();
        healthbarScale = healthbar.transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(health>0)
            {
                //减血
                TakeDamage();
             
            }
            else
            {
                //播放死亡动画，掉到河里
            }
        }
    }
    void TakeDamage()
    {
        health -= hurtBloodPoint;
        //更新血条状态
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        healthbar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);//血条颜色变化
        healthbar.transform.localScale = new Vector3(health * 0.01f, 1, 1);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
