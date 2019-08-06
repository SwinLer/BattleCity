using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //玩家属性值
    public int lifeValue;
    public int playerScore;
    public bool isDead;
    public bool isDefeat;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text PlayerLifeValueText;
    public GameObject isDefeatUI;

    public int whichPlayerIsDead;

    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance
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

    void Start()
    {
        lifeValue = DatesControl.Instance.saveLifeValue;
        playerScore = DatesControl.Instance.savePlayerScore;
        isDead = false;
    }

    //实时监听
    void Update()
    {
        if(isDefeat)
        {
            isDefeatUI.SetActive(true);
			Invoke("ReturnToTheMenu", 3);
			return;
        }

        if(isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        PlayerLifeValueText.text = lifeValue.ToString();

    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            //游戏失败返回主界面
            isDefeat = true;
			Invoke("ReturnToTheMenu", 3);
        }
        else
        {
            lifeValue--;
            DatesControl.Instance.saveLifeValue = lifeValue;
            if (whichPlayerIsDead == 1)
            {
                GameObject go1 = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
                go1.GetComponent<Born>().isPlayer = true;
                go1.GetComponent<Born>().whichPlayer = 1;
            }
            else if (whichPlayerIsDead == 2)
            {
                GameObject go2 = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
                go2.GetComponent<Born>().isPlayer = true;
                go2.GetComponent<Born>().whichPlayer = 2;
            }
            isDead = false;
        }
    }

	private void ReturnToTheMenu()
	{
		SceneManager.LoadScene(0);
	}
}
