using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishArray : MonoBehaviour
{
    //鱼的生命值
    public int Hp;
    //掉落的金币数
    public int gold;
    //击杀所获得的经验
    public int exp;
    //鱼群的最大移动速度
    public int maxSpeed;
    //鱼群的最大数量
    public int maxNum;
    public GameObject deadAni;
    public GameObject moneyPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //鱼碰撞到外墙销毁自身
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall") Destroy(gameObject);
    }

    //鱼受到伤害
    void MakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            //生命值降至0，触发死亡动画，生成金钱
            MakeDeadAni();
            MakeMoney();
            if (gameObject.GetComponent<Fish_Effect>() != null)
                gameObject.GetComponent<Fish_Effect>().PlayEffect();
            UIManager.Instance.AddExp(exp);
            Destroy(gameObject);
        }
    }
    private void MakeDeadAni()
    {
        GameObject dead = Instantiate(deadAni);
        dead.transform.SetParent(transform.parent, false);
        dead.transform.position = transform.position;
        dead.transform.rotation = transform.rotation;
    }
    private void MakeMoney()
    {
        GameObject money = Instantiate(moneyPrefab);
        money.GetComponent<MoneyController>().money = gold;
        money.transform.SetParent(transform.parent, false);
        money.transform.position = transform.position;
        money.transform.rotation = transform.rotation;
        
    }
}
