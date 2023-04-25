using System;
using UnityEngine;
using UnityEditor;
using SA.Weapons.Data;
using System.Collections.Generic;
using SA.Weapons.Components;
using UnityEditor.Callbacks;
using System.Linq;

namespace SA.Weapons
{
	/// <summary>
	/// 编辑器脚本
	/// CustomEditor 表示显示在（SO_WeaponData）的Inspector上
	/// </summary>
	[CustomEditor(typeof(SO_WeaponData))]
	public class WeaponDataEditor : Editor
	{
		private static List<Type> dataComponentTypes = new List<Type>();

		private SO_WeaponData SO_Data;

		private bool showUpdataButtons;
		private bool showAddButtons;

		private void OnEnable()
		{
			SO_Data = target as SO_WeaponData;
		}

		/// <summary>
		/// Inspector绘制
		/// </summary>
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if(GUILayout.Button("设置攻击次数"))
			{
				foreach (var item in SO_Data.ComponentData)
				{
					item.InitAttackData(SO_Data.NumberOfAttacks);
				}
			}

			showAddButtons = EditorGUILayout.Foldout(showAddButtons, "添加组件按钮");

			if(showAddButtons)
			{
				foreach (var dataType in dataComponentTypes)
				{
					if (GUILayout.Button("Add " + dataType.Name))
					{
						var comp = Activator.CreateInstance(dataType) as ComponentData;

						if (comp == null)
						{
							return;
						}

						comp.InitAttackData(SO_Data.NumberOfAttacks);

						SO_Data.AddData(comp);
					}
				}
			}

			showUpdataButtons = EditorGUILayout.Foldout(showUpdataButtons, "更新名称按钮");

			if (showUpdataButtons)
			{
				if (GUILayout.Button("Updata Component Data Name"))
				{
					foreach (var item in SO_Data.ComponentData)
					{
						item.SetComponentName();
					}
				}

				if (GUILayout.Button("Updata Attack Data Name"))
				{
					foreach (var item in SO_Data.ComponentData)
					{
						item.SetAttackDataNames();
					}
				}
			}

		}

		/// <summary>
		/// 重新编译时调用
		/// 用于获得所有的ComponentData，后续添加的Data会自动添加到List中
		/// </summary>
		[DidReloadScripts]
		private static void OnRecompile()
		{
			// 获得当前应用程序中所有程序集的列表
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			// 在列表中获得所有的Type
			var types = assemblies.SelectMany(assembly => assembly.GetTypes());
			
			var filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);

			dataComponentTypes = filteredTypes.ToList();
		}
	}


}
