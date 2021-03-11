using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private GameObject moneyCollector;
    public int money;
    // Start is called before the first frame update
    void Start()
    {
        moneyCollector = GameObject.Find("MoneyCollector");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moneyCollector.transform.position, 20 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MoneyCollector")
        {

            UIManager.Instance.AddGold(money);
            AudioManager.Instance.PlayAudioClip(AudioManager.Instance.GoldClip);
            Destroy(gameObject);
        }

    }
}
