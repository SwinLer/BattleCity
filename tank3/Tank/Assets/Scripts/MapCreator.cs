using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    //用来初始化地图所需预制体的数组
    //0.基地  1.墙  2.障碍  3.出生效果  4.河流  5.草  6.空气墙
    public GameObject[] item;

    //已被占用位置列表
    public List<Vector3> itemPositionList = new List<Vector3>();


    public List<GameObject> itemPretectHeartWall = new List<GameObject>();
    public float pretectBarrierTime;
    public bool isHasPretectBarrier=false;

    private static MapCreator instance;
    public static MapCreator Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
		InitMap();
    }

	private void InitMap()
	{
        //创建基地
         CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
       
        InitPretectWall(1);

		//实例化基地外围墙
		for (int i = -11; i < 12; i++)
		{
			CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
		}
		for (int i = -11; i < 12; i++)
		{
			CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
		}
		for (int i = -8; i < 9; i++)
		{
			CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
		}
		for (int i = -8; i < 9; i++)
		{
			CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
		}

		//初始化玩家

		GameObject go1 = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
		go1.GetComponent<Born>().isPlayer = true;
        go1.GetComponent<Born>().whichPlayer = 1;
        itemPositionList.Add(go1.transform.position);

        if(DatesControl.Instance.howManyPlayers==2)
        {
            GameObject go2 = Instantiate(item[3], new Vector3(2, -8, 0), Quaternion.identity);
            go2.GetComponent<Born>().isPlayer = true;
            go2.GetComponent<Born>().whichPlayer = 2;
            itemPositionList.Add(go2.transform.position);
        }

		//产生敌人
		CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
		CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
		CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

		InvokeRepeating("CreateEnemy", 4, DatesControl.Instance.createEnemySpeed);

		//实例化地图
		{
			for (int i = 0; i < 60; i++)
			{
				CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
			}
			for (int i = 0; i < 20; i++)
			{
				CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
			}
			for (int i = 0; i < 20; i++)
			{
				CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
			}
			for (int i = 0; i < 20; i++)
			{
				CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
			}
		}
	}

    //初始化地图函数
    private void CreateItem(GameObject createGameobject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject createItem = Instantiate(createGameobject, createPosition, createRotation);
        createItem.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    //产生随机位置
    private Vector3 CreateRandomPosition()
    {
        //不生成隔断地图
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-8, 8), 0);
            if(!HasThePosition(createPosition))
            {
                return createPosition;
            }  
        }
    }

    //判断生成位置是否位于位置列表中
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i=0; i < itemPositionList.Count ; i++)
        {
            if(createPos==itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //产生敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if(num==1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }

    private void CreatePretectWall(GameObject createGameobject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject createPretectWall = Instantiate(createGameobject, createPosition, createRotation);
        createPretectWall.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
        itemPretectHeartWall.Add(createPretectWall);
    }

    public void InitPretectWall(int num)
    {
      
        CreatePretectWall(item[num], new Vector3(-1, -8, 0), Quaternion.identity);
        CreatePretectWall(item[num], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            CreatePretectWall(item[num], new Vector3(-1 + i, -7, 0), Quaternion.identity);
        }
        if (num == 2)
        {
            pretectBarrierTime = 10;
            isHasPretectBarrier = true;
        }
    }

}
