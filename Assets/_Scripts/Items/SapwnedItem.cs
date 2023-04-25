using SA.MEntity;
using SA.MEntity.CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Items
{
	public class SapwnedItem : MonoBehaviour
	{

		private bool buttomPressed;
		private bool stayExcuted;

		protected Transform lootParent;
		protected SpriteRenderer spriteRenderer;

		protected Core core;
		protected Stats stats;

		protected Rigidbody2D rb;

		protected virtual void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();

			lootParent = GameObject.Find("LootsParent").transform;
		}

		protected virtual void Update()
		{
			//执行一些动画或特效逻辑

			if(Input.GetKeyDown(KeyCode.E))
			{
				buttomPressed = true;
			}
			if(Input.GetKeyUp(KeyCode.E))
			{
				buttomPressed = false;
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				if (core == null)
				{
					core = other.transform.GetComponentInChildren<Core>();
					stats = core.GetCoreComponent<Stats>();
				}
			}
		}

		protected virtual void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				if (buttomPressed && !stayExcuted)
				{
					OnPressedHandler(other);
					stayExcuted = true;
				}
			}
		}

		protected virtual void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				stayExcuted = false;
			}
		}

		/// <summary>
		/// 按钮按下触发
		/// </summary>
		protected virtual void OnPressedHandler(Collider2D other)
		{
			Debug.Log("OnPressedHandler");
		}

	}
}
