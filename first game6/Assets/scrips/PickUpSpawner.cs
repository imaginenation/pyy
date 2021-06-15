using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject[] pickups;
    public float intervalTime = 5f;
    public float leftX ;
    public float rightX ;

    //float health = 100f;
    public float highHealthThreshold = 75f;   
    public float lowHealthThreshold = 25f;
    private PlayerHealth playerHealth;
    //private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = pickups[1].GetComponent<Animator>();//获取动画控制器
        StartCoroutine(spawnPickup());
       // InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }    
    // anim.SetTrigger("land"); 
    public IEnumerator spawnPickup()
    {
        yield return new WaitForSeconds(intervalTime);

        float randomx = Random.Range(leftX, rightX);
        Vector3 randomPos = new Vector3(randomx, 15, 0);

        if (playerHealth.health >= highHealthThreshold)
            
            Instantiate(pickups[0], randomPos, Quaternion.identity);
        
        else if (playerHealth.health <= lowHealthThreshold)
           
            Instantiate(pickups[1], randomPos, Quaternion.identity);
        else
        {
            int index = Random.Range(0, pickups.Length);
            Instantiate(pickups[index], randomPos, Quaternion.identity);
        }
    }
    void Update()
    {
       
    }
}
