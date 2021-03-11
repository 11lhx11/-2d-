using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Move : MonoBehaviour
{
    private Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = transform.position;
        temp.x *= -1;
    }

    //鱼的运动方式
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,temp,10*Time.deltaTime);
    }
}
