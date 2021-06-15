using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreShadow : MonoBehaviour
{
	public GameObject guiCopy;      // 乐谱的副本。


	void Awake()
	{
		// 将位置设置为略微向下，并在其他gui的后面。
		Vector3 behindPos = transform.position;
		behindPos = new Vector3(guiCopy.transform.position.x, guiCopy.transform.position.y-0.005f, guiCopy.transform.position.z-1);
		transform.position = behindPos;
	}


	void Update ()
	{
		// 将文本设置为等于副本的文本。
		GetComponent<Text>().text = guiCopy.GetComponent<Text>().text;
	}
}
