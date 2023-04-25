using SA.MEntity.CoreComponents;
using SA.Utilities;
using UnityEngine;

namespace SA.Items
{
	public class Loots : MonoBehaviour
	{
		private Rigidbody2D rb;
		private bool magnet = false;
		private bool timerDone = false;

		[SerializeField]
		private float range = 1;
		[SerializeField]
		private float speed = 20;
		[SerializeField]
		private Transform owner;

		protected Stats stats;

		private Timer timer;

		private void Awake()
		{
			timer = new Timer(0.8f);
			timer.OnTimerDone += OnTimerDone;

			rb = GetComponent<Rigidbody2D>();

			if (owner == null)
			{
				owner = GameObject.FindWithTag("Player").transform;
				stats = owner.GetComponentInChildren<Stats>();
			}
		}

		private void Start()
		{
			timer.StartTimer();
		}

		private void Update()
		{
			timer.Tick();

			if (timerDone && !magnet && Vector3.Distance(transform.position, owner.position) <= range)
			{
				magnet = true;
			}

			if(magnet)
			{
				Vector3 dir = Vector3.MoveTowards(transform.position, owner.position, speed * Time.deltaTime);
				rb.MovePosition(dir);
				if (Vector2.Distance(transform.position, owner.position) <= 0.1f)
				{
					Picked();
				}
			}
		}

		/// <summary>
		/// 被拾取后的逻辑
		/// </summary>
		protected virtual void Picked()
		{

		}

		public void AddForce(Vector2 dir, float force)
		{
			if (rb == null)
			{
				rb = GetComponent<Rigidbody2D>();
			}
			rb.velocity = dir * force;
			//rb.AddForce(dir * force, ForceMode2D.Impulse);
		}

		private void OnTimerDone()
		{
			timerDone = true;
		}
	}
}
