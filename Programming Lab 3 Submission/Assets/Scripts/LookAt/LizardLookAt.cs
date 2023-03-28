using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardLookAt : MonoBehaviour
{
     public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((GameObject.Find("Character").transform.position - transform.position).magnitude <= 10)
        {
        Vector3 newPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(newPos);
        gameObject.transform.Rotate(-90,180,0);
        }
    }
    
}
