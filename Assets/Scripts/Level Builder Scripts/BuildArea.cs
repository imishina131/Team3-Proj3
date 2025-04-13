using UnityEngine;


public class BuildArea : MonoBehaviour
{
    [HideInInspector] public float tileSize;
    [HideInInspector] public Vector3 size;

    public bool showBuildAreaGizmo = true;
    public bool showTileTopGizmo = true;
    public bool showPropsBottomGizmos = true;

    public void BuildNodes()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
               
                
               Vector3 pos = new Vector3(transform.position.x + (x * tileSize), transform.position.y, transform.position.z + (z * tileSize));
               GameObject node = new GameObject();
               node.transform.parent = transform;
               node.transform.position = pos;
               node.name = $"Build Node (X: {x}, Z: {z}),";
               Node nodescript = node.AddComponent<Node>();
               nodescript.buildArea = this;
                    
#if UNITY_EDITOR
               UnityEditor.EditorUtility.SetDirty(node);
#endif
               
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showBuildAreaGizmo) return;
        for (int x = 0;x < size.x; x++)
        {
            for (int z = 0;z < size.y; z++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Gizmos.DrawWireCube(pos, new Vector3(tileSize, 0, tileSize));
                }
            }
        }
    }
}
