using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 3;
    private Vector3 bullectEularAngles;
    private SpriteRenderer sr;
    private float timeVal;
    private bool isdefended = true;
    //无敌时间
    private float defendTime=3;    
    public Sprite[] tankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendedEffectPrefab;

    public int whichPlayer;
    public float moveTime = 30;
 


	public AudioSource moveAudio;
	public AudioClip[] tankAudio;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();              
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  //判断玩家坦克是否无敌
        Isdefended();

        IsHasPretectBarrier();
        //玩家攻击
        AttackCD();
        MoveTime();
    }

    private void FixedUpdate()
    {
        //游戏失败，玩家禁止移动
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }

        //玩家移动
        Move();
    }


    //玩家移动
    private void Move()
    {
        float h, v;
        if (whichPlayer == 1)
        {
            h = Input.GetAxisRaw("Horizontal");
        }
        else 
        {
            h = Input.GetAxisRaw("Horizontal2");
        } 

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);      //调用translate方法改变位置，Time.deltaTime 为每帧所用时间        
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEularAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEularAngles = new Vector3(0, 0, -90);
        }
		if (Mathf.Abs(h) > 0.05f)
		{
			moveAudio.clip = tankAudio[1];
			if (!moveAudio.isPlaying)
			{
				moveAudio.Play();
			}
		}
		if (h != 0)
        {
            return;
        }

        if (whichPlayer == 1)
        {
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            v = Input.GetAxisRaw("Vertical2");
        }
        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEularAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEularAngles = new Vector3(0, 0, 0);
        }
		if (Mathf.Abs(v) > 0.05f)
		{
			moveAudio.clip = tankAudio[1];
			if (!moveAudio.isPlaying)
			{
				moveAudio.Play();
			}
		}
		else
		{
			moveAudio.clip = tankAudio[0];
			if (!moveAudio.isPlaying)
			{
				moveAudio.Play();
			}
		}
	}
    //玩家攻击
    private  void Attack()
    {
        if (whichPlayer == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEularAngles));
                timeVal = 0;
            }
        }
        else if (whichPlayer==2)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEularAngles));
                timeVal = 0;
            }
        }
        
    }
    
    //玩家攻击CD
    private void AttackCD()
    {
        if (timeVal > 0.5)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }
        
    //玩家死亡
    private void Die(int whichPlayerIsDead)
    {
        if (isdefended)
        {
            return;
        }

        PlayerManager.Instance.whichPlayerIsDead = whichPlayerIsDead;
        PlayerManager.Instance.isDead = true;

        Instantiate(explosionPrefab,transform.position, transform.rotation);
        Destroy(gameObject);
    }
    //玩家是否处于无敌状态
    private void Isdefended()
    {
        if (isdefended)
        {
            defendedEffectPrefab.SetActive(true);
            defendTime -= Time.deltaTime;
            if (defendTime <= 0)
            {
                isdefended = false;
                defendedEffectPrefab.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            switch (collision.collider.tag)
            {
                case "LifeRewards":
                    {
                        Destroy(collision.gameObject);
                        Debug.Log(1);
                        PlayerManager.Instance.lifeValue++;
                        DatesControl.Instance.saveLifeValue = PlayerManager.Instance.lifeValue;
                        break;
                    }
                case "PretectRewards":
                    {
                        Destroy(collision.gameObject);
                        Debug.Log(1);
                        isdefended = true;
                        defendTime = 7;
                        break;
                    }
                case "BarrierRewards":
                    {
                        Destroy(collision.gameObject);
                       for(int i = 0; i < MapCreator.Instance.itemPretectHeartWall.Count;i++)
                        {
                            Destroy(MapCreator.Instance.itemPretectHeartWall[i]);
                        }

                        MapCreator.Instance.InitPretectWall(2);
                            
                        break;
                    }
                case "AddMoveTime":
                    {
                        Destroy(collision.gameObject);
                        moveTime = moveTime + 20;
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }
    }

    private void IsHasPretectBarrier()
    {
        if (MapCreator.Instance.isHasPretectBarrier)
        {
            if (MapCreator.Instance.pretectBarrierTime >= 0)
            {
                MapCreator.Instance.pretectBarrierTime = MapCreator.Instance.pretectBarrierTime - Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < MapCreator.Instance.itemPretectHeartWall.Count; i++)
                {
                    Destroy(MapCreator.Instance.itemPretectHeartWall[i]);
                }
                MapCreator.Instance.InitPretectWall(1);
                MapCreator.Instance.isHasPretectBarrier = false;
            }
        }
        
    }

    private void MoveTime()
    {
        moveTime = moveTime - Time.deltaTime;
        if (moveTime > 20)
        {
            moveSpeed = 3;
        }
       else if (moveTime > 10 && moveTime < 20)
        {
            moveSpeed = 1.5F;
        }
        else if (moveTime > 0 && moveTime < 10)
        {
            moveSpeed = 0.8F;
        }
        else if (moveTime < 0)
        {
            moveSpeed = 0;
        }
    }

}
