using UnityEngine;
using UnityEngine.UI;

public class LanternWheelButtonController : MonoBehaviour
{
    public int id;
    private Animator anim;
    private bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {

        }
    }

    public void Selected()
    {
        selected = true;
    }

    public void Deselected()
    {
        selected = false;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
    }
}
