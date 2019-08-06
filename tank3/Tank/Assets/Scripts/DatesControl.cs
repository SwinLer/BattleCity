using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DatesControl : MonoBehaviour
{
    private static DatesControl instance;

    public int saveLifeValue = 3;
    public int savePlayerScore = 0;


    public bool saveIsDead=false;
    public bool saveIsDefeat;
    public int howManyPlayers;

    public float gameTime = 0;

    public int level = 0;
    public int redTankRate=5;
    public int createEnemySpeed=5;


    public static DatesControl Instance
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
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GameTime();   
    }

    private void GameTime()
    {
        if (gameTime < 40)
        {
            gameTime = gameTime + Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
            Level();
            gameTime = 0;
        }
    }

    private void Level()
    {
        if (level > 3&&saveLifeValue>1)
        { 
            saveLifeValue--;
            redTankRate = 4;
            createEnemySpeed = 4;
        }
        if (level > 5&&saveLifeValue>=2)
        {
            saveLifeValue--;
            redTankRate = 3;
            createEnemySpeed = 3;
        }
        savePlayerScore++;
        level++;
    }
    
}
