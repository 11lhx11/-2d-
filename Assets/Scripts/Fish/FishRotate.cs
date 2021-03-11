using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRotate : MonoBehaviour
{
    public int angleSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, angleSpeed * Time.deltaTime);
    }
}
