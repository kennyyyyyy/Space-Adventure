using UnityEngine;

namespace SA.MEntity.CoreComponents
{
	public class ParticleManager : CoreComponent
	{
		private Transform particleContainer;

		protected override void Awake()
		{
			base.Awake();

			particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
		}

		public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
		{
			//TODO: 对象池
			return Instantiate(particlePrefab, position, rotation, particleContainer);
		}

		public GameObject StartParticles(GameObject particlePrefab)
		{
			return StartParticles(particlePrefab, transform.position, Quaternion.identity);
		}

		public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
		{
			var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
			return StartParticles(particlePrefab, transform.position, randomRotation);

		}
	}
}
