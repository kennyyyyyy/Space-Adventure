using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Transform respawnPoint;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private float respawnTime;

	private float respawnStart;

	private bool respawn;

	private CinemachineVirtualCamera playerCamera;

	private void Start()
	{
		playerCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
	}


	private void Update()
	{
		CheckRespawn();
	}

	public void Respawn()
	{
		respawnStart = Time.time;
		respawn = true;
	}

	private void CheckRespawn()
	{
		if(respawn && Time.time >= respawnStart + respawnTime)
		{
			var playerTemp = Instantiate(player, respawnPoint);
			playerCamera.m_Follow = playerTemp.transform;
			respawn = false; 
		}
	}

}
