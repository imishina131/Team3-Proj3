using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.Loading;

public class LevelBuilder : EditorWindow
{
    int currentToolbarTabindex = 0;
    string[] toolbarTabs = new string[] { "Setup", "Add Tiles", "Add Props" };

    GUIStyle headerStyle;

    Vector3 originPos;
    string originName = "level";
    int buildAreaWidth;
    int buildAreaLength;
    float tileSize;

    int selectedTileIndex = 0;
    int selectedPropIndex = 0;

    //scrolling
   // Vector2 scrollPos = Vector2.zero;
    //Vector2 scrollPos2 = Vector2.zero;

    Node selectedNode;

    GUIContent originConfiglabel = new GUIContent("Origin Point Configuration");

    GUIContent sizeConfigLabel = new GUIContent("Size Configuration");

    [MenuItem("Level Builder/Open Builder")]

    static void ShowWindow()
    {
        LevelBuilder window = (LevelBuilder)GetWindow(typeof(LevelBuilder));
        //scrolling
        //window.minSize = new Vector2(250, 250);
        //window.maxSize = new Vector2(500, 500);
        window.Show();
    }

    private void OnEnable()
    {
        headerStyle = new GUIStyle() { wordWrap = true, normal = { textColor = Color.white }, fontStyle = FontStyle.Bold };
    }
    private void OnGUI()
    {
        
        currentToolbarTabindex = GUILayout.Toolbar(currentToolbarTabindex, toolbarTabs);
       // scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));
        
        //selectedTileIndex = EditorGUILayout.IntField(selectedTileIndex, GUILayout.ExpandHeight(true));
        //EditorGUILayout.EndScrollView();

       // GUILayout.FlexibleSpace();

        if (currentToolbarTabindex == 0) DrawSetupTab();
        else if (currentToolbarTabindex == 1) DrawASelectionTab("Tiles");
        else if (currentToolbarTabindex == 2) DrawASelectionTab("Props");


        //scrolling
       // scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));
        //EditorGUILayout.EndScrollView();
    }

    private void DrawSetupTab()
    {
        GUILayout.Label(originConfiglabel, headerStyle);

        originPos = EditorGUILayout.Vector3Field("Origin Location", originPos);
        originName = EditorGUILayout.TextField("Origin Name", originName);

        GUILayout.Space(10);
        GUILayout.Label(sizeConfigLabel, headerStyle);

        buildAreaWidth = EditorGUILayout.IntSlider("Level Width", buildAreaWidth, 1, 100);
        buildAreaLength = EditorGUILayout.IntSlider("Level length", buildAreaLength, 1, 100);
        tileSize = EditorGUILayout.Slider("Tile Size", tileSize, 1, 100);

        if (GUILayout.Button("Create Build Area")) CreateBuildArea();
    }

    private void CreateBuildArea()
    {
        GameObject buildArea = new GameObject();
        buildArea.transform.position = originPos;
        buildArea.name = originName;
        BuildArea area = buildArea.AddComponent<BuildArea>();
        area.size = new Vector2(buildAreaWidth, buildAreaLength);
        area.tileSize = tileSize;
        area.BuildNodes();
        EditorUtility.SetDirty(buildArea);

    }
    private void DrawASelectionTab(string tab)
    {
        if (!AssetDatabase.IsValidFolder($"Assets/LevelBuilder/{tab}"))
        {
            EditorGUILayout.HelpBox($"Following Folder Missing: Assets/LevelBuilder/{tab}", MessageType.Error);
            return;
        }

        GameObject[] prefabs = GetObjects(GetObjectsPath(tab));

        if (prefabs == null)
        {
            EditorGUILayout.HelpBox($"No {tab} where found at the following Directory: Assets/LevelBuilder/{tab}", MessageType.Error);
            return;
        }

        List<GUIContent> contents = new List<GUIContent>();

        foreach (GameObject prefab in prefabs)
        {
            if (prefab== null) continue;

            Texture2D previewTexture = GetPreviewTexture(prefab);
            contents.Add(new GUIContent(previewTexture, prefab.name));
        }

        GUIContent[] contentsArray = contents.ToArray();
        //contentsArray = EditorGUILayout.(contents, GUILayout.ExpandHeight(true));
        //GUIContent[] 
        //selectedTileIndex = EditorGUILayout.IntField(selectedTileIndex, GUILayout.ExpandHeight(true));
        if (tab == "Tiles") selectedTileIndex = GUILayout.SelectionGrid(selectedTileIndex, contentsArray, 3);
        else selectedPropIndex = GUILayout.SelectionGrid(selectedPropIndex, contentsArray, 3);
    }

    private void DrawPropsTab()
    {
        
    }

    private Texture2D GetPreviewTexture(GameObject prefab)
    {
        return AssetPreview.GetAssetPreview(prefab);
    }

    private string[] GetObjectsPath(string folder)
    {
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new string[] { $"Assets/LevelBuilder/{folder}" });

        if (guids.Length <= 0) return null;

        List<string> paths = new List<string>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            paths.Add(path);
        }
        return paths.ToArray();
    }

    private GameObject[] GetObjects(string[] paths)
    {
        if(paths == null || paths.Length <= 0) return null;

        List<GameObject> foundObjects = new List<GameObject>();
        foreach (string path in paths)
        {
            GameObject tileObject = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            foundObjects.Add(tileObject);
        }
        return foundObjects.ToArray();
    }

    private bool CheckTilePropability()
    {
        Tile tile = null;
        Debug.Log("Check Propability");

        for (int x = 0; x < selectedNode.transform.childCount; x++)
        {
            GameObject child = selectedNode.transform.GetChild(x).gameObject;
            tile = child.GetComponent<Tile>();
            if (tile != null && tile.propability == Tile.Propability.Propable)
            {
                return true;
            }
            else if (tile != null && tile.propability == Tile.Propability.NonPropable)
            {
                Debug.LogWarning("Clicked tile type isn't propable!");
                return false;
            }
        }
        Debug.LogWarning("Clicked tiletype isn't propable!");
        return false;
    }

    private void ClearOldObjects(Node node, string folder)
    {
        for (int x = 0; x < node .transform.childCount; x++)
        {
            GameObject child = node.transform.GetChild(x).gameObject;

            if (folder == "Props" && child.GetComponent<Prop>() == null) continue;
            if (folder == "Tiles" && child.GetComponent<Tile>() == null) continue;
            
            DestroyImmediate(child);
            
        }
    }

    private void PlaceNewObject<T>(string folder)
    {
        GameObject[] prefabs = GetObjects(GetObjectsPath(folder));
        int index = typeof(T) == typeof(Prop) ? selectedPropIndex : selectedTileIndex;
        GameObject selectedObject = prefabs[index];

        Vector3 objectPositionOffset;

        T typeScript = selectedObject.GetComponent<T>();

        if (typeScript == null)
        {
            Debug.LogWarning($"{folder} requires the {folder} script!");
            return;
        }

        if (folder == "Tiles") ClearOldObjects(selectedNode, "Props");
        ClearOldObjects(selectedNode, folder);

        if (typeof(T) == typeof(Prop))
        {
            if (!CheckTilePropability())
            {
                return;
            }
            objectPositionOffset = selectedNode.transform.position + new Vector3(0, selectedObject.GetComponent<Prop>().bottom, 0);
            Debug.Log("Prop Placed");
        }
        else
        {
            objectPositionOffset = selectedNode.transform.position + new Vector3(0, selectedObject.GetComponent<Tile>().top, 0);
        }

        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(selectedObject, selectedNode.transform);
        obj.transform.position = objectPositionOffset;

        if (typeof(T) == typeof(Prop)) obj.GetComponent<Prop>().buildArea = selectedNode.buildArea;
        else obj.GetComponent<Tile>().buildArea = selectedNode.buildArea;
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject != null)
        {
            Node area = Selection.activeGameObject.GetComponent<Node>();

            if (area != null)
            {
                selectedNode = area;

                if (currentToolbarTabindex == 1)
                {
                    PlaceNewObject<Tile>("Tiles");
                }

                if (currentToolbarTabindex == 2)
                {
                    PlaceNewObject<Prop>("Props");
                }
            }
            else
            {
                selectedNode = null;
            }
        }
    }
}
