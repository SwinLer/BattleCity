using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    public float h;
    public float v;
    public float moveSpeed = 3;
    private Vector3 bullectEularAngles;
    private SpriteRenderer sr;


    private float timeVal;
    private float timeDirection=4;

    public Sprite[] tankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

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
    {
        //玩家攻击
        AttackCD();
    }

    private void FixedUpdate()
    {
        //玩家移动
        Move();
    }

    //玩家移动
    private void Move()
    {
        if (timeDirection > 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = -1;
            }
            else if (num > 3 && num <= 5)
            {
                v = 0;
                h = 1;
            }
            timeDirection = 0;
        }
        else if (timeDirection <= 4)
        {
            timeDirection += Time.deltaTime;
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
    }


    //玩家攻击
    private void Attack()
    {
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEularAngles));
        timeVal = 0;

    }
    //玩家攻击时间间隔
    private void AttackCD()
    {
        if (timeVal > 3)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    //敌人死亡
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        DatesControl.Instance.savePlayerScore = PlayerManager.Instance.playerScore;

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            timeDirection = 4;
        }
    }
}
