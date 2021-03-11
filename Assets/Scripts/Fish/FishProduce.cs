using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishProduce : MonoBehaviour
{
    //鱼群生成所在的父组件
    public Transform fishHolder;
    //鱼群生成点
    public Transform[] producePos;
    //鱼的类别
    public GameObject[] fishPrefabs;

    private void Start()
    {
        //每隔一段时间生成一波鱼
        InvokeRepeating("ProduceFish", 0, 0.5f);
    }

    public void ProduceFish()
    {
        //随机的生成点和鱼群种类
        int produceIndex = Random.Range(0,producePos.Length);
        int fishIndex = Random.Range(0, fishPrefabs.Length);
        //获取这波鱼可达到的最大速度和数量
        int maxSpeed = fishPrefabs[fishIndex].GetComponent<FishArray>().maxSpeed;
        int maxNum = fishPrefabs[fishIndex].GetComponent<FishArray>().maxNum;
        //鱼群的随机速度和数量
        int speed = Random.Range((maxSpeed/2)+1,maxSpeed);
        int num = Random.Range((maxNum / 2) + 1, maxNum);
        //随机的运动方式
        int mode = Random.Range(0, 2);
        int straightAngle;
        int cornerAngle;
        float moveTime = CountMoveTime(fishIndex, speed);
        //从某个角度进行直线行走
        if (mode == 0)
        {
            straightAngle = Random.Range(-20, 20);
           
            StartCoroutine(GenerateStraightFish(produceIndex, fishIndex, speed, num, straightAngle,moveTime));
        }
        //从某个角度进行偏移移动
        else
        {
            int m = Random.Range(0,2);
            if (m == 0)
            {
                cornerAngle = Random.Range(-15,-6);
            }
            else
            {
                cornerAngle = Random.Range(6,15);
            }
            StartCoroutine(GenerateRotateFish(produceIndex, fishIndex, speed, num, cornerAngle, moveTime));
        }
    }
    IEnumerator GenerateStraightFish(int posIndex,int fishIndex,int speed,int num,float straightAngle,float moveTime)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish= Instantiate(fishPrefabs[fishIndex]);
            fish.transform.SetParent(fishHolder,false);
            fish.transform.localPosition = producePos[posIndex].localPosition;
            fish.transform.rotation = producePos[posIndex].rotation;
            fish.transform.Rotate(0,0,straightAngle);
            fish.AddComponent<FishStraight>().speed = speed;
            yield return new WaitForSeconds(moveTime);
        }
    }
    IEnumerator GenerateRotateFish(int posIndex, int fishIndex, int speed, int num, int rotateSpeed, float moveTime)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = producePos[posIndex].localPosition;
            fish.transform.rotation = producePos[posIndex].rotation;
            fish.AddComponent<FishStraight>().speed = speed;
            fish.AddComponent<FishRotate>().angleSpeed = rotateSpeed;
            yield return new WaitForSeconds(moveTime);
        }
    }
    //每条鱼生成的间隔时间
    float CountMoveTime(int fishIndex,int speed)
    {
        
        Vector2 temp= fishPrefabs[fishIndex].GetComponent<SpriteRenderer>().bounds.size;
        float time = ((temp.x / 2) / speed);// * fishPrefabs[fishIndex].transform.localScale.x;
        //Debug.Log(time);
        
        return  time/10;
    }
}
