using UnityEngine;

public class Prop : MonoBehaviour
{
    public float bottom = 0;

    [HideInInspector] public BuildArea buildArea;

    private void OnDrawGizmos()
    {
        if (buildArea != null && buildArea.showPropsBottomGizmos)
        {
            Vector3 startPos = transform.position - new Vector3(0, bottom, 0);

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
