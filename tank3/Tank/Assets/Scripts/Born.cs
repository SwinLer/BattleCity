using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public bool isPlayer;
    public GameObject[] enemy;
    public GameObject[] players;
    public int whichPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1);
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //玩家坦克出生特效
    private void BornTank()
    {
        if (isPlayer)
        {
            if (whichPlayer == 1)
            {
                Instantiate(players[0], transform.position, Quaternion.identity);
            }
            else if(whichPlayer==2)
            {
                Instantiate(players[1], transform.position, Quaternion.identity);
            }
        }
        else
        {
            int num = Random.Range(0, DatesControl.Instance.redTankRate);
            Instantiate(enemy[num], transform.position, Quaternion.identity);
        }
    }
}
