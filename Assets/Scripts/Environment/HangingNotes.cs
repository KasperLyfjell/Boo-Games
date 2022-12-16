using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingNotes : MonoBehaviour
{
    [Tooltip("This is for all the empty, physical game objects hanging in the scene. They will recieve a random texture")]
    public List<GameObject> Notes;
    [Tooltip("A set of textures which 'Notes' will randomly pick one from")]
    public List<Material> Materials;

    private void Start()
    {
        foreach(GameObject note in Notes)
        {
            note.GetComponent<MeshRenderer>().material = Materials[Random.Range(0, Materials.Count)];
        }
    }
}
