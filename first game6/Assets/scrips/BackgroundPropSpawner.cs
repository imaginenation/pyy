using UnityEngine;
using System.Collections;

public class BackgroundPropSpawner : MonoBehaviour
{
	public Rigidbody2D backgroundProp;      // 要实例化的道具。
	public float leftSpawnPosX;             // 位置的x坐标如果它在左边被实例化。
	public float rightSpawnPosX;            // 位置的x坐标，如果它在右边被实例化。
	public float minSpawnPosY;              // 位置的最小可能的y坐标。
	public float maxSpawnPosY;              // 位置的最高可能的y坐标。
	public float minTimeBetweenSpawns;      // 刷出之间最短的时间。
	public float maxTimeBetweenSpawns;      // 最长的刷出间隔。
	public float minSpeed;                  // 这是道具的最低速度。
	public float maxSpeed;                  // 这是道具的最高速度。

	void Start ()
	{
		// 设置随机种子，让每个游戏都不一样。
		Random.InitState(System.DateTime.Today.Millisecond);

		// 启动衍生协程。
		StartCoroutine("Spawn");
	}


	IEnumerator Spawn ()
	{
		// 在道具被实例化之前创建一个随机的等待时间。
		float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

		// 等待指定的时间。
		yield return new WaitForSeconds(waitTime);

		// 随机决定道具是面向左边还是右侧。
		bool facingLeft = Random.Range(0,2) == 0;

		// 如果道具朝向左边，它应该从右边开始，否则它应该从左边开始。
		float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;

		// 为道具创建一个随机的y坐标。
		float posY = Random.Range(minSpawnPosY, maxSpawnPosY);

		// 设置道具生成的位置。
		Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);

		// 在期望的位置实例化道具。
		Rigidbody2D propInstance = Instantiate(backgroundProp, spawnPos, Quaternion.identity) as Rigidbody2D;

		// 精灵为道具全部脸左。因此，如果支柱应该面向右……
		if (!facingLeft)
		{
			// 在x轴上翻转比例
			Vector3 scale = propInstance.transform.localScale;
			scale.x *= -1;
			propInstance.transform.localScale = scale;
		}

		// 创造一个随机速度。
		float speed = Random.Range(minSpeed, maxSpeed);

		// 这些速度会自然地让道具向右移动，所以如果它向左，就把速度乘以-1。
		speed *= facingLeft ? -1f : 1f;

		// 设置道具在x轴上的速度为这个速度。
		propInstance.velocity = new Vector2(speed, 0);

		// 重新启动协程以生成另一个道具。
		StartCoroutine(Spawn());

		// 当道具存在的时候
		while (propInstance != null)
		{
			// 如果它朝左
			if (facingLeft)
			{
				// 如果它在左边刷出位置之外
				if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
					// 破坏支撑
					Destroy(propInstance.gameObject);
			}
			else
			{
				//否则，如果道具面向右侧，并且超出了右侧刷出位置
				if (propInstance.transform.position.x > rightSpawnPosX + 0.5f)
					// 破坏支撑
					Destroy(propInstance.gameObject);
			}

			// 在下一次更新之后返回到这一点
			yield return null;
		}
	}
}
