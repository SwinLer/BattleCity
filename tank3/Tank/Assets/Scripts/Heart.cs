using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite BrokenSprite;
    public GameObject ExplosionPrefab;
	public AudioClip dieAudio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //基地被摧毁
    private void GameOver()
    {
        sr.sprite = BrokenSprite;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
		AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
 
}
