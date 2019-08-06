using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //爆炸特效持续时间
        Destroy(gameObject, 0.167f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
