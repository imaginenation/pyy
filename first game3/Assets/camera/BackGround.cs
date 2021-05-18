using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour//背景运动
{
    // Start is called before the first frame update
    public Transform[] backGrounds;
    public float fParallax = 0.4f;
    public float fParallay = 0.4f;
    public float layerFraction = 5f;//每层之间相对运动量
    public float fSmooth = 5f;//控制每一帧平滑程度
    Transform cam;
    Vector3 previousCamPos;//Start函数中赋初值
    private void Awake()
    {
        cam = Camera.main.transform;//找到摄像机位置
    }
    private void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        //x方向
        float fParallaxX = (previousCamPos.x - cam.position.x) * fParallax;
        for(int i=0;i<backGrounds.Length;i++)//计算每一层背景运动距离
        {
            float fNewX = backGrounds[i].position.x + fParallaxX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backGrounds[i].position.y, backGrounds[i].position.z);//新位置
            //所有运动都需要线性差值平滑用lerp
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos, fSmooth * Time.deltaTime);
        }
        //y方向
        float fParallaxY = (previousCamPos.y - cam.position.y) * fParallay;
        for (int i = 0; i < backGrounds.Length; i++)//计算每一层背景运动距离
        {
            float fNewY = backGrounds[i].position.y + fParallaxY * (1 + i * layerFraction);-0
            Vector3 newPos1 = new Vector3(backGrounds[i].position.x,fNewY, backGrounds[i].position.z);//新位置
            //所有运动都需要线性差值平滑用lerp
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos1, fSmooth * Time.deltaTime);
        }
        previousCamPos = cam.position;//确定新位置
    }
}
