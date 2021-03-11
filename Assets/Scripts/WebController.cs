using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    public int damage;
    public float aliveTime;
    private void Start()
    {
        Destroy(gameObject,aliveTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            collision.SendMessage("MakeDamage",damage);
        }
    }
}
