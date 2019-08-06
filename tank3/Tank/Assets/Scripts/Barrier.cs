using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
	public AudioClip hitAudio;

	private void PlayAudio()
	{
		AudioSource.PlayClipAtPoint(hitAudio, transform.position);
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
