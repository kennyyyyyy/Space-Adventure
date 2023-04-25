using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MapGenerator
{
	[Serializable]
	public class CaveRoomTemplatesConfig
	{
		public GameObject[] DefaultRoomTemplates;

		public GameObject[] ShopRoomTemplates;

		public GameObject[] TeleportRoomTemplates;

		public GameObject[] TreasureRoomTemplates;

		public GameObject[] EntranceRoomTemplates;

		public GameObject[] ExitRoomTemplates;

		public GameObject[] CorridorRoomTemplates;

		public GameObject[] GetRoomTemplates(CaveRoom room)
		{
			switch (room.Type)
			{
				case CaveRoomType.Shop:
					return ShopRoomTemplates;

				case CaveRoomType.Teleport:
					return TeleportRoomTemplates;

				case CaveRoomType.Treasure:
					return TreasureRoomTemplates;

				case CaveRoomType.Entrance:
					return EntranceRoomTemplates;

				case CaveRoomType.Exit:
					return ExitRoomTemplates;

				default:
					return DefaultRoomTemplates;
			}
		}
	}
}
