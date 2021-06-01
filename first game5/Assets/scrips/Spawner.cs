using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;        // 每次刷出间隔的时间。
	public float spawnDelay = 3f;       // 产卵开始前的时间。
	public GameObject[] enemies;        //敌人预制件阵列。


	void Start ()
	{
		// 在延迟后重复调用Spawn函数。
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// 实例化一个随机敌人。
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

		// 播放所有粒子系统的生成效果。
		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
