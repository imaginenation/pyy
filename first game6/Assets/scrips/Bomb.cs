using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	public float bombRadius = 10f;          // 杀死敌人的半径范围。
	public float bombForce = 100f;          // 从爆炸中抛出敌人的力量。
	public AudioClip boom;                  // Audioclip爆炸。
	public AudioClip fuse;                  // Audioclip保险丝。
	public float fuseTime = 1.5f;
	public GameObject explosion;            // 预制爆炸效果


	private LayBombs layBombs;              // 参考玩家的laybomb脚本。
	private PickUpSpawner pickupSpawner;    // 引用PickupSpawner脚本。
	private ParticleSystem explosionFX;     // 参考粒子系统的爆炸效果


	void Awake ()
	{
		// Setting up references.
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		pickupSpawner = GameObject.Find("PickupManarger").GetComponent<PickUpSpawner>();
		if (GameObject.FindGameObjectWithTag("Player"))
			layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
	}

	void Start ()
	{

		// 如果炸弹没有父炸弹，它是由玩家放置的，应该被引爆。
		if (transform.root == transform)
			StartCoroutine(BombDetonation());
	}


	IEnumerator BombDetonation()
	{
		// 播放保险丝音频夹
		AudioSource.PlayClipAtPoint(fuse, transform.position);

		// 等待2秒
		yield return new WaitForSeconds(fuseTime);

		//炸弹爆炸
		Explode();
	}


	public void Explode()
	{

		// 玩家现在可以自由放置炸弹了
		layBombs.bombLaid = false;

		//让拾取刷出器开始传送新的拾取物。
		pickupSpawner.StartCoroutine(pickupSpawner.spawnPickup());

		// 在bombRadius中找到敌人层的所有碰撞器
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemy"));

		// For each collider...
		foreach(Collider2D en in enemies)
		{
			// 检查它是否具有刚体(因为在父类中每个敌人只有一个)。
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if(rb != null && rb.tag == "Enemy")
			{
				// 找到敌人脚本并将敌人的生命值设置为0
				rb.gameObject.GetComponent<Enemy>().HP = 0;

				// 找到从炸弹到敌人的方向.
				Vector3 deltaPos = rb.transform.position - transform.position;

				// 向这个方向施加一个力，大小为炸弹力
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce(force);
			}
		}

		// 设置爆炸效果的位置为炸弹的位置，并播放粒子系统
		explosionFX.transform.position = transform.position;
		explosionFX.Play();

		// 实例化爆炸预制
		Instantiate(explosion,transform.position, Quaternion.identity);

		// 播放爆炸音效.
		AudioSource.PlayClipAtPoint(boom, transform.position);

		// 破坏了炸弹.
		Destroy(gameObject);
	}
}
