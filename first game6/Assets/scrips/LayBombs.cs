using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LayBombs : MonoBehaviour
{
	[HideInInspector]
	public bool bombLaid = false;       // 现在是否安放了炸弹。
	public int bombCount = 0;           // 玩家拥有多少炸弹。
	public AudioClip bombsAway;         // 当玩家放炸弹的声音.
	public GameObject bomb;             // 预制炸弹.
	PlayerControl playerControl;

	private Text bombHUD;           // 抬头显示玩家是否拥有炸弹。


	void Awake ()
	{
		// Setting up the reference.
		bombHUD = GameObject.Find("ui_bombHUD").GetComponent<Text>();
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// 如果炸弹放置按钮被按下，炸弹没有被放置，而有一个炸弹要放置…
		if (Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
		{
			// 减少炸弹的数量
			bombCount--;

			// 将bombLaid设置为true.
			bombLaid = true;

			// 播放放置炸弹的声音.
			AudioSource.PlayClipAtPoint(bombsAway,transform.position);
			Vector3 direction = new Vector3(0, 0, 0);
			// 实例化炸弹预制件。
			if (playerControl.bFaceRight)
			{
				direction.y = 180;//旋转180°
				Instantiate(bomb, transform.position, Quaternion.Euler(direction));
			}
			else
			{
				Instantiate(bomb, transform.position, Quaternion.Euler(direction));
			}
		}

		// 如果玩家有炸弹，那么炸弹抬头显示应该是启用的，其他的应该是禁用的。
		//bombHUD.enabled = bombCount > 0;
	}
}
