using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public Camera m_Camera;
    bool radius;
    public GameObject textPopup;
    MeshRenderer text;


    private void Start()
    {
        text = textPopup.GetComponent<MeshRenderer>();
        text.enabled = false;
    }
    void OnTriggerStay(Collider hitBox)
    {
        if (hitBox.gameObject.CompareTag("Player"))
        {
            radius = true;
            text.enabled = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            radius = false;
            text.enabled = false;

        }
    }
    private void Update()
    {
        if (radius == true)
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
        }
    }
}
