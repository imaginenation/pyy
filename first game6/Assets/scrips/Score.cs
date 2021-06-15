using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// player's 分数.


	private PlayerControl playerControl;	
	private int previousScore = 0;          // 前一回合的比分.


	void Awake ()
	{
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// 设置分数文本。
		GetComponent<Text>().text = "Score: " + score;

		// 如果比分变了
		if (previousScore != score)
			// ... play a taunt.
			//playerControl.StartCoroutine(playerControl.Taunt());

		// 将之前的分数设置为这一帧的分数。
		previousScore = score;
	}

}
