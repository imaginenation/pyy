using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D heroBody;
    public float moveForce = 100;
    private float fInput = 0.0f;
    public float maxSpeed = 5;
    private bool bFaceRight = true;
    private bool bGrounded = false;
    public float jumpForce = 500;
    Transform mGroundCheck;
    // Start is called before the first frame update
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();//获得Rigidbody
        mGroundCheck = transform.Find("GroundCheck");
    }
    
    // Update is called once per frame
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        if (fInput < 0 && bFaceRight)
            flip();
        else if (fInput > 0 && !bFaceRight)
            flip();
        bGrounded=Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //射线检测
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)
            heroBody.AddForce(fInput * moveForce * Vector2.right);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,heroBody.velocity.y);
        bool bJump = false;
        if(bGrounded)//判断是否在地上
        {
            bJump=Input.GetKeyDown(KeyCode.Space);//判断是否按了跳跃键
            Vector2 upForce = new Vector2(0,1);
            if(bJump)
               heroBody.AddForce(upForce * jumpForce);//jumpForce可扩大位置上的力
        }
       
    }
    void flip()//转身
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
   
}
