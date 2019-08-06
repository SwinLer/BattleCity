using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{

    public float moveSpeed = 10;
    public bool isPlayerBullect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //子弹移动
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
       
    }

    //子弹遇到不同的物体时的不同状态
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                {
                    if (!isPlayerBullect)
                    {
                     collision.SendMessage("Die",1);
                     Destroy(gameObject);
                    }
                    break;
                }
            case "Tank2":
                {
                    if (!isPlayerBullect)
                    {
                        collision.SendMessage("Die", 2);
                        Destroy(gameObject);
                    }
                    break;
                }
            case "Heart":
                {
                    collision.SendMessage("GameOver");
                    break;
                }
			case "Barrier":
                { 
					collision.SendMessage("PlayAudio");
					Destroy(gameObject);
					break;
				}
            case "Wall":
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    break;
                }
            case "Enemy":
                {
                    if (isPlayerBullect)
                    {
                        collision.SendMessage("Die");
                        Destroy(gameObject);
                    } 
                    break;
                }
            default:
                { 
                    break;
                }
        }
    }

}
