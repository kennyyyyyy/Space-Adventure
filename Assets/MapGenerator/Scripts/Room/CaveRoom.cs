using Edgar.Unity;
using Edgar.Unity.Examples.Metroidvania;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MapGenerator
{
	public class CaveRoom : RoomBase
	{
		public CaveRoomType Type;


		public override List<GameObject> GetRoomTemplates()
		{
			// We do not need any room templates here because they are resolved based on the type of the room.
			return null;
		}

		public override string GetDisplayName()
		{
			// Use the type of the room as its display name.
			return Type.ToString();
		}
	}
}
