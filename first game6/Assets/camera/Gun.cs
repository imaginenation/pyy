using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;
    public float fSpeed = 10;
    PlayerControl playerControl;
    private Animator anim;
    private AudioSource ac;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = transform.root.GetComponent<PlayerControl>();
        anim = transform.root.GetComponent<Animator>();
        ac = GetComponent<AudioSource>();//平级组件不用transform 获得组件
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0)) ;
        if(Input.GetButtonDown("Fire1"))
        {
            ac.Play();
            anim.SetTrigger("Shoot");
            Vector3 direction = new Vector3(0, 0, 0);
            if(playerControl.bFaceRight)
            {
                Rigidbody2D RockectInstance = Instantiate(rocket, transform.position, Quaternion.Euler(direction));
                RockectInstance.velocity = new Vector2(fSpeed, 0);
            }
            else
            {
                direction.z = 180;//旋转180°
                Rigidbody2D RockectInstance = Instantiate(rocket, transform.position, Quaternion.Euler(direction));
                RockectInstance.velocity = new Vector2(-fSpeed, 0);
            }
        }
    }
}
