using Edgar.Unity;
using Edgar.Unity.Examples.Metroidvania;
using System;
using System.Linq;
using UnityEngine;

namespace SA.MapGenerator
{
	[CreateAssetMenu(fileName = "NewCavePostProcessing", menuName = "Data/Map Generator/Post Processing/Cave Post Processing")]
	public class CavePostProcessing : DungeonGeneratorPostProcessingGrid2D
	{
		[SerializeField] private GameObject enemy1;
		[SerializeField] private GameObject enemy2;
		[Range(0, 1f), SerializeField] private float spawnChance = 0.5f;

		[SerializeField] private GameObject[] Items;

		public bool spanEnemies;

		public override void Run(DungeonGeneratorLevelGrid2D level)
		{
			SetSpawnPosition(level);
			SetupLayers(level);

			if (spanEnemies)
				HandleEnemies(level);
		}

		/// <summary>
		/// 敌人生成
		/// </summary>
		/// <param name="level"></param>
		private void HandleEnemies(DungeonGeneratorLevelGrid2D level)
		{
			foreach (var roomInstance in level.RoomInstances)
			{
				var enemiesHolder = roomInstance.RoomTemplateInstance.transform.Find("EnemySpawnPoints");

				if (enemiesHolder == null)
				{
					continue;
				}

				foreach (Transform pos in enemiesHolder)
				{
					if (Random.NextDouble() >= spawnChance)
					{
						//TODO: 对象池
						Instantiate(enemy1, pos.position, Quaternion.identity, pos);
					}
					else
					{
						Instantiate(enemy2, pos.position, Quaternion.identity, pos);
					}
				}
			}
		}

		/// <summary>
		/// 移动角色位置到出生点
		/// </summary>
		/// <param name="level"></param>
		private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
		{

			var entranceRoomInstance = level.RoomInstances
				.FirstOrDefault(x => ((CaveRoom)x.Room).Type == CaveRoomType.Entrance);

			//	var entranceRoomInstance = level
			//.RoomInstances
			//.FirstOrDefault(x => x.RoomTemplatePrefab.name == "Enterence");

			if (entranceRoomInstance == null)
			{
				throw new InvalidOperationException("Could not find Entrance room");
			}

			var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;

			// Find the spawn position marker
			var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");

			// Move the player to the spawn position
			var player = GameObject.FindWithTag("Player");
			player.transform.position = spawnPosition.position;
		}

		/// <summary>
		/// 设置tilemap的layer
		/// </summary>
		/// <param name="level"></param>
		private void SetupLayers(DungeonGeneratorLevelGrid2D level)
		{
			var ground = LayerMask.NameToLayer("Ground");

			// Set the environment layer for all the instances of room templates
			foreach (var roomInstance in level.RoomInstances)
			{
				foreach (var tilemap in RoomTemplateUtilsGrid2D.GetTilemaps(roomInstance.RoomTemplateInstance))
				{
					if (tilemap.name == "Walls" || tilemap.name == "Platforms")
						tilemap.gameObject.layer = ground;
				}
			}

			// Set the environment layer for all the shared tilemaps
			foreach (var tilemap in level.GetSharedTilemaps())
			{
				if (tilemap.name == "Walls" || tilemap.name == "Platforms")
					tilemap.gameObject.layer = ground;
			}
		}
		
		/// <summary>
		/// 在商店房间生成商品
		/// </summary>
		private void SetShopItem(DungeonGeneratorLevelGrid2D level)
		{
			var shopRoomInstance = level.RoomInstances
				.FirstOrDefault(x => ((CaveRoom)x.Room).Type == CaveRoomType.Shop);

			if (shopRoomInstance == null)
			{
				throw new InvalidOperationException("Could not find Shop room");
			}

			var roomTemplateInstance = shopRoomInstance.RoomTemplateInstance;

			var spawnPosition = roomTemplateInstance.transform.Find("ItemListTransform");

			//TODO: 生成道具
		}
	}
}

