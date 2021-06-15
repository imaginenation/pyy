using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;


	void OnTriggerEnter2D(Collider2D col)
	{
		// 若有人掉落
		if (col.gameObject.tag == "Player")
		{
			// .. 停止摄像机追踪玩家
	    	//GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = false;

			//  停止跟随玩家的命值条
			if (GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... 在玩家掉落的地方实例化飞溅。
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... 毁掉英雄.
			Destroy (col.gameObject);
			// ... 重新加载水平.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// ... 在敌人掉落的地方实例化溅射。
			Instantiate(splash, col.transform.position, transform.rotation);

			// 摧毁敌人。
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
