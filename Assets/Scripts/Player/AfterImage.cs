using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
	[SerializeField]
	private float activeTime = 0.1f;
	private float timeActivated;
	private float alpha;
	[SerializeField]
	private float alphaSet = 0.8f;
	[SerializeField]
	private float alphaDecay = 0.85f;


	private Transform player;
	private SpriteRenderer SR;
	private SpriteRenderer playerSR;

	private Color color;


	private void OnEnable()
	{
		SR = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerSR = player.GetComponent<SpriteRenderer>();

		alpha = alphaSet;
		SR.sprite = playerSR.sprite;
		transform.position = player.position;
		transform.rotation = player.rotation;
		timeActivated = Time.time;
	}

	private void Update()
	{
		alpha -= alphaDecay * Time.deltaTime;
		color = new Color(1, 1, 1, alpha);
		SR.color = color;

		if(Time.time >= (timeActivated + activeTime))
		{
			AfterImagePool.Instance.AddToPool(gameObject);
		}
	}
}
