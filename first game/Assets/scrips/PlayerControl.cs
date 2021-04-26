using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D heroBody;
    public float force = 100;
    // Start is called before the first frame update
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        heroBody.AddForce(Vector2.right * h * force);
    }
}
