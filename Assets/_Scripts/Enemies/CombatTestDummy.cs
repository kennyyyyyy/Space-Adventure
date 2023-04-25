using SA.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable, IKnockBackable
{
	[SerializeField] private GameObject hitParticles;

	private Vector2 workSpace;
	private Animator anim;
	private Rigidbody2D RB;

	public void Damage(float amount)
	{
		Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
		anim.SetTrigger("damage");
		//Destroy(gameObject);
	}

	public void KnockBack(Vector2 angle, float strength, int direction)
	{
		angle.Normalize();
		workSpace.Set(angle.x * strength * direction, angle.y * strength);
		RB.velocity = workSpace;
	}

	private void Awake()
	{
		anim = GetComponent<Animator>();
		RB = GetComponent<Rigidbody2D>();
	}
}
