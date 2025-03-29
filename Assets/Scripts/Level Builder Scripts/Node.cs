using UnityEngine;

//namespace Team3Proj3.LevelBuilder
//{
   // [SelectionBase]

    public class Node : MonoBehaviour
    {
        [HideInInspector] public BuildArea buildArea;

        private void OnDrawGizmos()
        {
            if (buildArea == null) return;

            Gizmos.color = new Color(0,0,0);
            Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(buildArea.tileSize * 0.75f, 0, buildArea.tileSize * 0.75f));
        }
    }
//}
