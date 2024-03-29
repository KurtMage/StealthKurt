﻿using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour
{
	public bool requireKey;
	public AudioClip doorSwishClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private HashIDs hash;
	private GameObject player;
	private PlayerInventory playerInventory;
	private int count;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
		count = 0;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			if (requireKey)
			{
				if (playerInventory.hasKey)
				{
					count++;
				}
				else
				{
					audio.clip = accessDeniedClip;
					audio.Play();
				}
			}
			else
			{
				count++;
			}
		}
		else if (other.gameObject.tag == Tags.enemy)
		{
			if (other is CapsuleCollider) // This is the enemy, as apposed to the large sphere collider that is their hearing
			{
				count++;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider)) //is enemy, not just enemyHearing
		{
			count = Mathf.Max(0, count - 1);
		}
	}

	void Update()
	{
		anim.SetBool (hash.openBool, count > 0);
		if (anim.IsInTransition(0) && !audio.isPlaying)
		{
			audio.clip = doorSwishClip;
			audio.Play();
		}
	}

}
