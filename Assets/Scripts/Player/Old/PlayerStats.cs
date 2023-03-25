using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private GameObject
		deathChunkParticle,
		deathBloodParticle;

	private float curHeath;

	private GameManager gameManager;

	private void Start()
	{
		curHeath = maxHealth;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void DecreaseHealth(float amount)
	{
		curHeath -= amount;

		if(curHeath <= 0.0f)
		{
			Die();
		}
	}

	private void Die()
	{
		Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
		Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
		gameManager.Respawn();
		Destroy(gameObject);
	}
}
