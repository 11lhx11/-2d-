using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //子弹飞行速度
    public int speed;
    //子弹造成的伤害
    public int damage;
    //子弹展开时的网
    public GameObject web;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞到外墙，销毁自身
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        //碰撞到鱼，销毁自身，并展开网
        else if (collision.tag == "Fish")
        {
            GameObject webclone = Instantiate(web);
            webclone.transform.SetParent(transform.parent,false);
            webclone.transform.position = transform.position;
            webclone.GetComponent<WebController>().damage = damage;
            Destroy(gameObject);
        }
    }
}
