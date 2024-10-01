using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    [SerializeField] PathManager pathManager;
    [SerializeField] List<Waypoint> ThePath;
    List<int> toDelete;

    Waypoint selectedPoint = null;
    bool doRepaint = true;

    private void OnSceneGUI()
    {
        ThePath = pathManager.GetPath();
        DrawPath(ThePath);
    }

    private void OnEnable()
    {
        pathManager = target as PathManager;
        toDelete = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();
        ThePath = pathManager.GetPath();

        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path");

        DrawGUIForPoints();

        if(GUILayout.Button("Add point to path"))
        {
            pathManager.CreateAddPoint();
        }

        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();
    }

    void DrawGUIForPoints()
    {
        if(ThePath != null && ThePath.Count > 0)
        {
            for(int i = 0; i < ThePath.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = ThePath[i];

                Color c = GUI.color;
                if(selectedPoint == p)
                {
                    GUI.color = Color.green;
                }

                Vector3 oldPos = p.GetPos();
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if(EditorGUI.EndChangeCheck())
                {
                    p.SetPos(newPos);
                }

                if(GUILayout.Button("-", GUILayout.Width(25)))
                {
                    toDelete.Add(i);
                }

                GUI.color = c;
                
                EditorGUILayout.EndHorizontal();
            }
        }

        if(toDelete.Count > 0)
        {
            foreach(int i in toDelete)
            {
                ThePath.RemoveAt(i);
                toDelete.Clear();
            }
        }
    }

    public void DrawPath(List<Waypoint> path)
    {
        if(path != null)
        {
            int current = 0;
            foreach(Waypoint  wp in path)
            {
                doRepaint = DrawPoint(wp);
                int next = (current + 1) % path.Count;
                Waypoint wpNext = path[next];
                DrawPathLine(wp, wpNext);
                current += 1;
            }

            if(doRepaint)
            {
                Repaint();
            }
        }
    }

    public void DrawPathLine(Waypoint p1, Waypoint p2)
    {
        Color c = Handles.color;
        Handles.color = Color.gray;
        Handles.DrawLine(p1.GetPos(), p2.GetPos());
        Handles.color = c;
    }

    public bool DrawPoint(Waypoint p)
    {
        bool isChanged = false;

        Vector3 currPos = p.GetPos();
        float handleSize = HandleUtility.GetHandleSize(currPos);
        if(Handles.Button(currPos, Quaternion.identity, 0.25f * handleSize, 0.25f * handleSize, Handles.SphereHandleCap))
        {
            isChanged = true;
            selectedPoint = p;
        }
        return isChanged;
    }
}
