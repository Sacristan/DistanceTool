using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Sacristan.DistanceTool._Editor
{
    public class DistanceToolMenu
    {
        static GameObject DistanceToolGameObject => GameObject.Find("DistanceTool");

        [MenuItem("GameObject/DistanceTool/Create")]
        static void CreateDistanceTool()
        {
            if (Selection.activeGameObject != null)
            {
                //Did the user select a DistanceTool?
                if (Selection.activeGameObject.name == "DistanceTool")
                {
                    addNewDistanceTool(Selection.activeGameObject);
                }
                else
                {
                    if (DistanceToolGameObject != null)
                    {
                        EditorUtility.DisplayDialog("Distance Tool Warning", "Oops, You need to select a Distance Tool to add an additional copy of the tool.", "OK");
                    }
                    else
                    {
                        createNewDistanceTool();
                    }
                }
            }
            else
            {
                if (DistanceToolGameObject != null)
                {
                    addNewDistanceTool(DistanceToolGameObject);
                }
                else
                {
                    createNewDistanceTool();
                }
            }
        }

        static void createNewDistanceTool()
        {
            GameObject go = new GameObject("DistanceTool");
            go.transform.position = Vector3.zero;
            go.AddComponent(typeof(Sacristan.DistanceTool.Runtime.DistanceTool));
        }

        static void addNewDistanceTool(GameObject go)
        {
            go.AddComponent(typeof(Sacristan.DistanceTool.Runtime.DistanceTool));
        }
    }

}