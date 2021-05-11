using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    public Transform playerTran;//主角的Transform 找到它的位置
    public float maxDistanceX = 2;
    public float maxDistanceY = 2;
    public float xSpeed = 4;
    public float ySpeed = 4;
    public Vector2 maxXandY;
    public Vector2 minXandY = new Vector2(-8, 8);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        playerTran = GameObject.Find//GameObject表示类，Find函数找到主角
                ("Hero").transform;
      //  playerTran = GameObject.FindGameObjectsWithTag("Player").transform;
    }
    private bool NeedMoveX()
    {   //x方向是否需要移动摄像机
        bool bMove = false;
        if (Mathf.Abs(transform.position.x - playerTran.position.x) > maxDistanceX)
            bMove = true;
        else
            bMove = false;
        return bMove;
    }
    bool NeedMoveY()
    {   //Y方向是否需要移动摄像机
        bool bMove = false;
        if (Mathf.Abs(transform.position.y - playerTran.position.y) > maxDistanceY)
            bMove = true;
        else
            bMove = false;
        return bMove;
    }
    private void FixedUpdate()
    {
        TrackPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void TrackPlayer()
    {
        float newX = transform.position.x;//设置当前摄像机的位置
        float newY = transform.position.y;
        if (NeedMoveX()) //计算新摄像机位置
            newX = Mathf.Lerp(transform.position.x, playerTran.position.x, xSpeed * Time.deltaTime);
        //Lerp差值函数（当前位置，目的位置，差值） 速度xSpeed乘以时间间隔Time.deltaTime
        if (NeedMoveY())
            newY = Mathf.Lerp(transform.position.y,playerTran.position.y, xSpeed * Time.deltaTime);
        newX = Mathf.Clamp(newX, minXandY.x, maxXandY.x);//固定摄像机位置
        newY = Mathf.Clamp(newY, minXandY.y, maxXandY.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
   
}
