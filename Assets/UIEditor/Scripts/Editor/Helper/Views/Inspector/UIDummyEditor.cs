
#if UNITY_EDITOR


// using System.Globalization;
// using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.UIEditor
{
    [CustomEditor(typeof(UIDummy))]
    public class UIDummyEditor : UnityEditor.Editor
    {
        
        private static bool IsPrefab(GameObject go)
        {
            var correspondingObject = PrefabUtility.GetCorrespondingObjectFromSource(go);
            var instanceHandle = PrefabUtility.GetPrefabInstanceHandle(go);
            return correspondingObject != null && instanceHandle != null;
        }

        //
        // public UIDummy Dummy
        // {
        //     get
        //     { 
        //         var mono = target as UIDummy;
        //         if (mono == null || IsPrefab(mono.gameObject))
        //         {
        //             return null;
        //         }
        //                 return mono;
        //     } 
        // }
        
        private readonly GUIContent DummyLabel = new GUIContent("锚点列表");

        public override void OnInspectorGUI()
        {
            var dummyMono = target as UIDummy;
            if (dummyMono == null)
            {
                return;
            }

            if (IsPrefab(dummyMono.gameObject))
            {
                EditorGUILayout.LabelField("请前往Prefab实例内编辑锚点");
                return;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("锚点列表");
            if (GUILayout.Button("添加"))
            {
                dummyMono.Dummys.Add(new DummyDefine());
            }

            EditorGUILayout.EndHorizontal();

            var pos = new Vector2(0, 20);
            EditorGUILayout.BeginScrollView(pos);
            {
                var dummyArr = new List<DummyDefine>(dummyMono.Dummys.ToArray());
                var index = 1;
                foreach (var define in dummyArr)
                {
                    define.Offset = EditorGUILayout.Vector2Field("偏移", define.Offset);
                    define.Type = (DummyType) EditorGUILayout.EnumPopup("类型", define.Type);
                    define.AliasName = EditorGUILayout.TextField("别名", define.AliasName);
                    if (GUILayout.Button("删除"))
                    {
                        dummyArr.Remove(define);
                    }

                    EditorGUILayout.Separator();
                    index++;
                }
                dummyMono.Dummys = dummyArr;
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif