using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 2, 0);
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log(playerTransform.position);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(playerTransform.position);
        transform.position = playerTransform.position + offset;
    }
}
