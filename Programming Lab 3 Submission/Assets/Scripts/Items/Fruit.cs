using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    //public int score;
    Collectable fruit;

    private void Awake()
    {
        fruit = new Collectable("fruit", 1, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("A point earned");
            //ScoreManager.scoreManager.UpdateScore(score);
            //Debug.Log("Collided");
            fruit.UpdateScore();
            Destroy(gameObject);
        }
    }
}
