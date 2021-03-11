using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GunController : MonoBehaviour
{
    //5把枪
    public GameObject[] gun_num;
    private int gunindex;
    public Text guncost;
    //每把枪对应的子弹类型，每把枪有10种颜色的子弹
    public GameObject[] gun1Bullet;
    public GameObject[] gun2Bullet;
    public GameObject[] gun3Bullet;
    public GameObject[] gun4Bullet;
    public GameObject[] gun5Bullet;

    public Text level_Text;

    public Transform BulletHolder;
    //当前枪对应的子弹价格挡位
    private int bulletindex;
    //每把枪有4种价格挡位，总共20种。
    public int[][] bulletcost= new int[][]{new int[] { 5, 10, 20, 30 },
        new int[]{ 40,50,60,70 },new int[]{ 80,90,100,200 },
        new int[]{ 300,400,500,600 },new int[]{ 700,800,900,1000 } };

    public GameObject ChangeGunEffectPrefab;
    public GameObject FireEffectPrefab;
    void Update()
    {
        Fire();
        MouseChangeBullet();
        
    }
    private void Fire()
    {
       
        if (Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (!UIManager.Instance.ReduceGold(bulletcost[gunindex][bulletindex])) return;
            //获取当前的枪的子弹规格数组
            GameObject[] gunbullet = gun5Bullet;
            switch (gunindex)
            {
                case 0:gunbullet = gun1Bullet; break;
                case 1: gunbullet = gun2Bullet; break;
                case 2: gunbullet = gun3Bullet; break;
                case 3: gunbullet = gun4Bullet; break;
                case 4: gunbullet = gun5Bullet; break;
                default: gunbullet = gun5Bullet;break;
            }
            //获取当前等级对应的子弹颜色
            int level = int.Parse(level_Text.text);
            int bulletColor = (level % 10)>9?9:(level%10);
            GameObject bullet= Instantiate(gunbullet[bulletColor]);
            //设置子弹的属性
            bullet.transform.SetParent(BulletHolder,false);
            bullet.transform.position = gun_num[gunindex].transform.Find("BulletPos").transform.position;
            bullet.transform.localRotation = gun_num[gunindex].transform.Find("BulletPos").transform.rotation;
            bullet.AddComponent<FishStraight>().speed = bullet.GetComponent<BulletController>().speed; ;
            bullet.GetComponent<FishStraight>().direction = Vector3.up;
            bullet.GetComponent<BulletController>().damage = bulletcost[gunindex][bulletindex];
            Instantiate(FireEffectPrefab, bullet.transform.position,Quaternion.identity);
            AudioManager.Instance.PlayAudioClip(AudioManager.Instance.FireClip);
        }

    }
    //鼠标滑轮滑动，枪支进行切换。每隔4个价格挡位，切换一把枪
    private void MouseChangeBullet()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonAdd(); 
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonReduce();
        }

    }
    public void OnButtonReduce()
    {
        gun_num[gunindex].SetActive(false);
        bulletindex--;
        //价格挡位小于0，切换下一把枪
        if (bulletindex <0)
        {
            bulletindex = bulletcost[0].Length-1;
            gunindex--;
            if (gunindex <0) gunindex =bulletcost.Length-1;
        }
        gun_num[gunindex].SetActive(true);
        guncost.text = "$"+bulletcost[gunindex][bulletindex].ToString();
        Instantiate(ChangeGunEffectPrefab, guncost.transform.position, Quaternion.identity);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.ChangeClip);
    }
    public void OnButtonAdd()
    {
        gun_num[gunindex].SetActive(false);
        bulletindex++;
        //价格挡位超过当前长度，切换下一把枪
        if (bulletindex >= bulletcost[gunindex].Length) 
        {
            bulletindex = 0;
            gunindex++;
            if (gunindex >= bulletcost.Length) gunindex = 0;
        }
        gun_num[gunindex].SetActive(true);
        guncost.text = "$" + bulletcost[gunindex][bulletindex].ToString();
        Instantiate(ChangeGunEffectPrefab,guncost.transform.position,Quaternion.identity);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.ChangeClip);
    }
}
