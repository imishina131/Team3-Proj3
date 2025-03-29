using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Propability { Propable, NonPropable }

    public Propability propability = Propability.Propable;

    public float top = 0;

    [HideInInspector] public BuildArea buildArea;

    private void OnDrawGizmos()
    {
        if (buildArea != null && buildArea.showTileTopGizmo)
        {
            Vector3 startPos = transform.position - new Vector3(0, top, 0);

            Gizmos.DrawSphere(startPos, 0.025f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPos, startPos + new Vector3(0, -0.5f, 0));

            Gizmos.DrawLine(startPos, startPos + new Vector3(0, 0, 0.5f));
            Gizmos.DrawLine(startPos, startPos + new Vector3(0, 0, -0.5f));
            Gizmos.DrawLine(startPos, startPos + new Vector3(0.5f, 0, 0));
            Gizmos.DrawLine(startPos, startPos + new Vector3(-.5f, 0, 0));
        }
    }

}
