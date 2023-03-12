using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using QTool.Reflection;
using System.Threading.Tasks;

namespace QTool.Inspector
{
    #region 自定义显示效果
	[CustomPropertyDrawer(typeof(QNameAttribute))]
    public class QNameDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (label.text == property.displayName)
			{
				label.text = property.QName();
			}
			property.Draw(position, label);
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return property.GetHeight();
		}
	}
	[CustomPropertyDrawer(typeof(QReadonlyAttribute))]
	public class QReadonlyDrawer : QNameDrawer
	{
	}
	[CustomPropertyDrawer(typeof(QOnChangeAttribute))]
	public class QOnChangeDrawer : QNameDrawer
	{
		
	}
	[CustomPropertyDrawer(typeof(QIdObject))]
    public class QIdObjectReferenceDrawer : PropertyDrawer
    {
		public static string Draw(string lable, string id, Type type, Rect? rect = null, params GUILayoutOption[] options)
		{
			var name = lable + "【" + (id == null ? "" : id.Substring(0, Mathf.Min(4, id.Length))) + "~】";
			var oldObj = QIdObject.GetObject(id, type);
			var newObj = oldObj;
			if (rect == null)
			{
				newObj = EditorGUILayout.ObjectField(name, oldObj, type, true);
			}
			else
			{
				newObj = EditorGUI.ObjectField(rect.Value, name, oldObj, type, true);
			}
			if (newObj != oldObj)
			{
				id = QIdObject.GetId(newObj);
			}
			return id;
		}
        public static QIdObject Draw(string lable, QIdObject ir, params GUILayoutOption[] options)
        {
			var newId = Draw(lable, ir.id, typeof(UnityEngine.Object), null, options);
			if (newId != ir.id)
			{
				ir.id = newId;
			}
			return ir;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
			var id = property.FindPropertyRelative(nameof(QIdObject.id));
			id.stringValue= Draw(label.text, id.stringValue,typeof(UnityEngine.Object),position);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
   
   
   
    [CustomPropertyDrawer(typeof(QToggleAttribute))]
    public class QToggleDrawer : PropertyDrawer
	{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Boolean)
            {
				position.height = 30;
				QGUI.PushColor(property.boolValue ? Color.black : Color.white);
                property.boolValue = EditorGUI.Toggle(position, property.boolValue, QGUI.BackStyle);
				QGUI.PopColor();
				var style = EditorStyles.largeLabel;
                style.alignment = TextAnchor.MiddleCenter;
                EditorGUI.LabelField(position, (attribute as QToggleAttribute).name, style);
            }
            else
            {
                property.Draw(position,label);
            }

        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
			if (property.propertyType == SerializedPropertyType.Boolean)
			{
				return 30;
			}
			else
			{
				return property.GetHeight();
			}
		}
    }


	public class QEnumListData
	{
		static QDictionary<string, QEnumListData> DrawerDic = new QDictionary<string, QEnumListData>((key) => new QEnumListData());

		public List<GUIContent> List = new List<GUIContent>();
		public int SelectIndex = 0;
		public string SelectValue
		{
			get
			{
				if (SelectIndex >= 0 && SelectIndex < List.Count)
				{
					return List[SelectIndex].text == "null" ? null : List[SelectIndex].text;
				}
				else
				{
					if (SelectIndex < 0)
					{
						SelectIndex = 0;
					}
					return default;
				}
			}
		}
	
		public void UpdateList(string key)
		{
			if (key == "null"|| key.IsNull())
			{
				SelectIndex = 0;
			}
			else
			{
				SelectIndex = List.FindIndex((obj)=>obj.text== key);
			}
		}
		public static QEnumListData Get(SerializedProperty property, string funcKey)
		{
			if (property.propertyType == SerializedPropertyType.ObjectReference)
			{
				return Get(QReflection.ParseType(property.type.SplitEndString("PPtr<$").TrimEnd('>')), funcKey);
			}
			else
			{
				return Get((object)property, funcKey);
			}
		}
	
		public static QEnumListData Get(object obj, string funcKey)
		{
			Type type = null;
			if (obj is Type)
			{
				type = obj as Type;
			}
			else
			{
				type = obj?.GetType();
			}
			var drawerKey = funcKey;
			if (drawerKey.IsNull())
			{
				if(obj is SerializedProperty property)
				{
					drawerKey = property.propertyType + "_" + property.name;
				}
				else
				{
					drawerKey = type + "";
				}
			}
			var drawer = DrawerDic[drawerKey];
			if (!funcKey.IsNull())
			{
				if (obj.InvokeFunction(funcKey) is IList itemList)
				{
					if (drawer.List.Count != itemList.Count)
					{
						drawer.List.Clear();
						foreach (var item in itemList)
						{
							drawer.List.Add(new GUIContent(item.ToGUIContent()));
						}
					}
				}
			}
			else if (drawer.List.Count == 0)
			{
				if (type.IsAbstract)
				{
					foreach (var childType in type.GetAllTypes())
					{
						drawer.List.Add(new GUIContent(childType.Name));
					}
				}
				else if (obj is SerializedProperty property)
				{
					drawer.List.Clear();
					switch (property.propertyType)
					{
						case SerializedPropertyType.Enum:
							{
								foreach (var item in property.enumNames)
								{
									drawer.List.Add(new GUIContent(item));
								}
							}
							break;
						default:
							break;
					}
				}
				else
				{
					EditorGUILayout.LabelField("错误函数" + funcKey);
				}
			}
			if (drawer.List.Count <= 0)
			{
				drawer.List.AddCheckExist(new GUIContent("null"));
			}
			return drawer;
		}
	}
    [CustomPropertyDrawer(typeof(QEnumAttribute))]
    public class QEnumDrawer : PropertyDrawer
    {
		public static object Draw(object obj, QEnumAttribute att)
		{
			var str = obj.ToGUIContent().text;
			using (new GUILayout.HorizontalScope())
			{
				var data = QEnumListData.Get(obj?.GetType(), att.funcKey);
				data.UpdateList(str);
				if (data.SelectIndex < 0)
				{
					data.SelectIndex = 0;
					str = data.SelectValue;
				}
				var newIndex = EditorGUILayout.Popup(data.SelectIndex, data.List.ToArray());
				if (newIndex != data.SelectIndex)
				{
					data.SelectIndex = newIndex;
					if (data.SelectIndex >= 0)
					{
						str = data.SelectValue;
					}
				}
			}
			return str;
		}
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!property.IsShow()) return;
			var data = QEnumListData.Get(property, (attribute as QEnumAttribute).funcKey);
			var value = property.propertyType == SerializedPropertyType.String?property.stringValue: property.objectReferenceValue?.GetType()?.Name;
			data.UpdateList(value);
			EditorGUI.LabelField(position.HorizontalRect(0f, 0.3f), property.QName());
			var newIndex = EditorGUI.Popup(position.HorizontalRect(0.7f, 1), data.SelectIndex, data.List.ToArray());
			if (newIndex != data.SelectIndex||value.IsNull())
			{
				data.SelectIndex = newIndex;
				if (property.propertyType== SerializedPropertyType.String)
				{
					property.stringValue = data.SelectValue;
				}
				else
				{
					var gameObject = (property.serializedObject.targetObject as MonoBehaviour)?.gameObject;
					if (gameObject != null) 
					{
						if (!value.IsNull())
						{
							GameObject.DestroyImmediate(gameObject.GetComponent(QReflection.ParseType(value)));
						}
						value = data.SelectValue;
						if (!value.IsNull())
						{
							property.objectReferenceValue=gameObject.GetComponent(QReflection.ParseType(value));
							if(property.objectReferenceValue == null)
							{
								property.objectReferenceValue = gameObject.AddComponent(QReflection.ParseType(value));
							}
						}
					}
					
				}
			}
		
		
		}
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.GetHeight();
        }



	}

	[CustomPropertyDrawer(typeof(QToolbarAttribute))]
	public class QToolbarDrawer : QNameDrawer
	{
		static QDictionary<int, int> IndexCache = new QDictionary<int, int>((key) => -1);
		const int Height = 30;
		public static void Draw(QToolbarAttribute toolbar , Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = Height;
			var listData = QEnumListData.Get(property, toolbar.getList);
			if (listData.List.Count == 0)
			{
				GUI.Toolbar(position,0, new string[] { "无"+toolbar.name }); 
			}
			else if (toolbar.pageSize<=0)
			{
				property.intValue = GUI.Toolbar(position,property.intValue, listData.List.ToArray());
			}
			else
			{
				var pageList = new List<GUIContent>();
				for (int i = 0; i < listData.List.Count / toolbar.pageSize + 1; i++)
				{
					pageList.AddObject(i);
				}
				if (IndexCache[listData.List.GetHashCode()] < 0)
				{
					IndexCache[listData.List.GetHashCode()] = property.intValue / toolbar.pageSize;
				}
				var start = IndexCache[listData.List.GetHashCode()] * toolbar.pageSize;
				var tempList = new List<GUIContent>();
				for (int i = start; i < Mathf.Min(start + toolbar.pageSize, listData.List.Count); i++)
				{
					tempList.Add(listData.List[i]);
				}
				if (listData.List.Count > toolbar.pageSize)
				{
					position.height = 20;
					IndexCache[listData.List.GetHashCode()] = GUI.Toolbar(position,IndexCache[listData.List.GetHashCode()], pageList.ToArray());
					position.y += 20;
					position.height = Height ;
				}
				if (IndexCache.Count == 0)
				{
					IndexCache[listData.List.GetHashCode()] = 0;
				}
				else
				{
					var intValue = start + GUI.Toolbar(position,property.intValue - start, tempList.ToArray());
					if (property.intValue != intValue)
					{
						property.intValue = intValue;
					}
				}

			}

		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var toolbar = (attribute as QToolbarAttribute);
			return property.IsShow() ?(toolbar.pageSize > 0&& QEnumListData.Get(property, toolbar.getList).List.Count> toolbar.pageSize ? Height +20:Height) : 0;
		}
	}
	#endregion


	[CustomEditor(typeof(UnityEngine.Object), true, isFallback = true)]
	[CanEditMultipleObjects]
	public class QInspectorEditor : Editor
	{
		public QInspectorType typeInfo { get; private set; }
		protected virtual void OnEnable()
		{
			typeInfo = QInspectorType.Get(target.GetType());
			InvokeQInspectorState(QInspectorState.OnEnable);
			EditorApplication.playModeStateChanged += OnPlayMode;
		}

		protected virtual void OnDestroy()
		{
			InvokeQInspectorState(QInspectorState.OnDisable);
			EditorApplication.playModeStateChanged -= OnPlayMode;
		}
		public override void OnInspectorGUI()
		{
			if (target == null)
			{
				GUILayout.Label("脚本丢失");
				return;
			}
			var iterator = serializedObject.GetIterator();
			if (iterator.NextVisible(true))
			{
				do
				{
					if ("m_Script".Equals(iterator.name))
					{
						GUI.enabled = false;
					}
					if (iterator.IsShow())
					{
						EditorGUILayout.PropertyField(iterator, new GUIContent(iterator.QName()));
					}
					if ("m_Script".Equals(iterator.name))
					{
						GUI.enabled = true;
					}
				} while (iterator.NextVisible(false));
				serializedObject.ApplyModifiedProperties();
			}
			DrawButton();
		}
		private void OnSceneGUI()
		{
			MouseCheck();
		}
	

		public void DrawButton()
		{
			foreach (var kv in typeInfo.buttonFunc)
			{
				var att = kv.Value;

				if (att.Active(target))
				{
					var name = att.name.IsNull() ? kv.Key.Name : att.name;

					if (att is QSelectObjectButtonAttribute)
					{
						if (GUILayout.Button(name))
						{
							EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "", name.GetHashCode());

						}
						if (Event.current.commandName == "ObjectSelectorClosed")
						{
							if (EditorGUIUtility.GetObjectPickerControlID() == name.GetHashCode())
							{
								var obj = EditorGUIUtility.GetObjectPickerObject();
								if (obj != null)
								{
									kv.Key.Invoke(target, obj);
								}

							}
						}
					}
					else
					{
						if (GUILayout.Button(name))
						{
							kv.Key.Invoke(target);
						}
					}

				}
			}
		}
		public void MouseCheck()
		{
			if (typeInfo.mouseEventFunc.Count <= 0) return;
			HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
			Event input = Event.current;
			if (!input.alt)
			{
				if (input.button == 0)
				{
					var mouseRay = HandleUtility.GUIPointToWorldRay(input.mousePosition);
					foreach (var kv in typeInfo.mouseEventFunc)
					{
						if (input.type == kv.Key.eventType)
						{
							if (input.isMouse)
							{
								if ((bool)kv.Value.Invoke(target, mouseRay))
								{
									input.Use();
								}
							}
							else
							{
								if ((bool)kv.Value.Invoke(target))
								{
									input.Use();
								}
							}
						}
					}
				}
			}
		}
		void InvokeQInspectorState(QInspectorState state)
		{
			foreach (var kv in typeInfo.inspectorState)
			{
				if (kv.Key.state == state)
				{
					var result = kv.Value.Invoke(target);
					if (result is Task task)
					{
						_ = task.Run();
					}
				}
			}
		}
		void OnPlayMode(PlayModeStateChange state)
		{
			QOnPlayModeAttribute.CurrentrState = (PlayModeState)(byte)state;
			foreach (var kv in typeInfo.playMode)
			{
				if ((byte)kv.Key.state == (byte)state)
				{
					kv.Value.Invoke(target);
				}
			}
		}

	}

}
