using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
	public float healthBonus=20f;               // 箱子带给玩家的生命值。
	public AudioClip collect;               // 收集板条箱的声音

	private PickUpSpawner pickupSpawner;    // 参考拾取刷出。
	private Animator anim;                  // 引用动画器组件。
	private bool landed;                    // 板条箱是否落地。
	void Awake ()
	{
		// Setting up the references.
		pickupSpawner = GameObject.Find("PickupManarger").GetComponent<PickUpSpawner>();
		anim = transform.root.GetComponent<Animator>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// 如果玩家进入了触发区域
		if (other.tag == "Player")
		{
			// 获得玩家生命值脚本的参考
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			// 增加玩家的生命值，但保持在100。
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);
			// 更新生命条。
			playerHealth.UpdateHealthBar();
			// 触发一个新的交付。
			pickupSpawner.StartCoroutine(pickupSpawner.spawnPickup());
			// 播放收集的声音
			AudioSource.PlayClipAtPoint(collect,transform.position);
			// 销毁医疗箱
			Destroy(transform.root.gameObject);
		}
		// 否则，如果板条箱掉到地上...
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;	
		}
	}
}
