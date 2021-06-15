using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100f;
    public float hurtBloodPoint = 20f;
    SpriteRenderer healthbar;
    Vector3 healthbarScale;

    public float damageRepeat = 0.5f;
    private float lastHurt;
    private Animator anim;
    public AudioClip[] ouchClips;
    public float hurtForce = 100f;
    void Start()
    {
        healthbar = GameObject.Find("Health").GetComponent<SpriteRenderer>();
        healthbarScale = healthbar.transform.localScale;
        lastHurt = Time.time;//游戏启动后运行了多少毫秒
        anim = GetComponent<Animator>();//获取动画控制器
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(Time.time>lastHurt+damageRepeat)
            {
                if (health > 0)
                {
                    //减血
                    TakeDamage(collision.gameObject.transform);
                    lastHurt = Time.time;
                    if(health<=0)
                    {
                        //播放死亡动画，掉到河里
                        anim.SetTrigger("die");
                        //掉入河中
                        Collider2D[] colliders = GetComponents<Collider2D>();
                        foreach (Collider2D c in colliders)//用c遍历collider
                            c.isTrigger = true;

                        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sp)
                            s.sortingLayerName = ("UI");//设置为最上一层

                        GetComponent<PlayerControl>().enabled = false;
                        GetComponentInChildren<Gun>().enabled = false;
                    }
                }
                else
                {
                    //播放死亡动画
                    anim.SetTrigger("die");
                    //掉入河中
                    Collider2D[] colliders = GetComponents<Collider2D>();
                    foreach (Collider2D c in colliders)//用c遍历collider
                        c.isTrigger = true;

                    SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer s in sp)
                        s.sortingLayerName = ("UI");//设置为最上一层

                    GetComponent<PlayerControl>().enabled = false;
                    GetComponentInChildren<Gun>().enabled = false;
                }
            }
            
        }
    }
    void TakeDamage(Transform enemy)
    {
        health -= hurtBloodPoint;
        //更新血条状态
        UpdateHealthBar();
        Vector3 hurtVector = transform.position - enemy.position+Vector3.up;//加了个向上的向量
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }
    public void UpdateHealthBar()
    {
        healthbar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);//血条颜色变化
        healthbar.transform.localScale = new Vector3(health * 0.01f, 1, 1);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
