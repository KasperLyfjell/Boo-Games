#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorTool : MonoBehaviour
{
    public bool CustomSpawnPos;

    private void Update()
    {
        
        if(CustomSpawnPos && Camera.current != null)
        {
            /*
            Ray worldPoint = Camera.current.ViewportPointToRay(new Vector3(0, 0, 50f));
            RaycastHit hit;
            if (Physics.Raycast(worldPoint, out hit))
            {
                Debug.DrawRay(Camera.current.transform.position, hit.transform.position, Color.red);
                Debug.Log(hit.transform.position);
            }
            else
                Debug.Log("nothing in range");
            */

        }
        
    }
}
#endif