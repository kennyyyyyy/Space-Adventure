using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour
{
	//TODO:对象池
	private void FinishAnim()
	{
		Destroy(gameObject);
	}
}
