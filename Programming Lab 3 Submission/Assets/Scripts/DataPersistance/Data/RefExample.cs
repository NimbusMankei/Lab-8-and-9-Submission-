using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefExample : MonoBehaviour
{
    int a = 10;
    int b = 20;
    // Start is called before the first frame update
    void Start()
    {
        AddValue(a);
        SubtractValue(ref b);

        Debug.Log(a);
        Debug.Log(b);
    }

    void AddValue(int a)
    {
        a += 1;
    }

    void SubtractValue(ref int b)
    {
        b -= 15;
    }
}
