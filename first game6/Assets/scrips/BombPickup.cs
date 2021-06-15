using UnityEngine;
using System.Collections;

public class BombPickup : MonoBehaviour
{
	public AudioClip pickupClip;        // 当炸弹箱被捡起来的声音。


	private Animator anim;				// Reference to the animator component.
	private bool landed = false;        //不管板条箱是否已经落地


	void Awake()
	{
		// Setting up the reference.
		anim = transform.root.GetComponent<Animator>();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// 如果玩家进入了触发区域
		if (other.tag == "Player")
		{
			// 播放拾音器的音效。
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			// 增加玩家拥有的炸弹数量。
			other.GetComponent<LayBombs>().bombCount++;

			// Destroy the crate.
			Destroy(transform.root.gameObject);
		}
		// 否则，如果板条箱落在地上。..
		else if (other.tag == "ground" && !landed)
		{
			// 设置动画触发参数land
			anim.SetTrigger("land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;		
		}
	}
}
