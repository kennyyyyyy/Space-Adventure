using Edgar.Unity;
using Edgar.Unity.Examples.Metroidvania;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.MapGenerator
{
	[CreateAssetMenu(menuName = "Data/Map Generator/Cave Input Setup", fileName = "NewCaveInputSetup")]
	public class CaveCustomInputSetupTask : DungeonGeneratorInputBaseGrid2D
	{
		public LevelGraph LevelGraph;

		public CaveRoomTemplatesConfig RoomTemplates;

		/// <summary>
		/// 将LevelGraph转换为LevelDescription
		/// </summary>
		protected override LevelDescriptionGrid2D GetLevelDescription()
		{
			var levelDescription = new LevelDescriptionGrid2D();

			// 遍历每个房间并将其添加到level description
			foreach (var room in LevelGraph.Rooms.Cast<CaveRoom>())
			{
				levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
			}

			// 遍历每个连接并将其添加到level description
			foreach (var connection in LevelGraph.Connections.Cast<CaveConnection>())
			{
				var corridorRoom = ScriptableObject.CreateInstance<CaveRoom>();
				corridorRoom.Type = CaveRoomType.Corridor;
				levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.CorridorRoomTemplates.ToList());
			}

			return levelDescription;
		}
	}
}
