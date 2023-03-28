using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((GameObject.Find("Character").transform.position - transform.position).magnitude <= 10)
        {
        Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(newPos);
        }
        
    }
}
