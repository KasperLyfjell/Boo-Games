using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SUPERCharacter;


[ExecuteInEditMode]
public class WorldText : MonoBehaviour
{
    private TextMeshProUGUI TextObj;
    private bool IsInside;
    private GameObject player;

    public List<Material> TextMat = new List<Material>();
    [SerializeField] TextColor color = new TextColor();
    private TextColor currentColor;
    public float OriginWidth;
    public float OriginHeight;

    public float MinDetectionRange;
    public float MaxDetectionRange;

    private float ZeroPoint = 100;//This is a constant when the text is completely gone
    private float StartWidth;


    private float Coeffcient;

    enum TextColor
    {
        Green,
        Violet,
        Red
    }


    private void Start()
    {
        UpdateEditor();

        StartWidth = OriginWidth;

        Coeffcient = (ZeroPoint - StartWidth) / (MaxDetectionRange - MinDetectionRange);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdateEditor();//Visualization in editor

        if (IsInside /*|| player.GetComponent<SUPERCharacterAIO>().GameStarted == false*/)//Editor support broken for now 3:
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if(distance > MinDetectionRange - (MinDetectionRange * 0.1))
                OriginWidth = (StartWidth - (MinDetectionRange * Coeffcient) + (distance * Coeffcient));
        }
        else if (player.GetComponent<SUPERCharacterAIO>().GameStarted)
        {
            OriginWidth = ZeroPoint;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MaxDetectionRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MinDetectionRange);
    }

    private void UpdateEditor()
    {
        if (TextObj == null)
            TextObj = GetComponent<TextMeshProUGUI>();

        if (currentColor != color)//Update color in editor
        {
            ChangeColor();
        }

        Vector4 boundSize = new Vector4(OriginWidth, OriginHeight, OriginWidth, OriginHeight);

        TextObj.margin = boundSize;

        GetComponent<SphereCollider>().radius = MaxDetectionRange / transform.localScale.x;
    }

    private void ChangeColor()
    {
        Debug.Log(color);

        /*
        switch (color)
        {
            case TextColor.Green:
                GetComponent<TextMeshProUGUI>().material = TextMat[0];
            break;

            case TextColor.Violet:
                GetComponent<TextMeshProUGUI>().material = TextMat[1];
                break;

            case TextColor.Red:
                GetComponent<TextMeshProUGUI>().material.TextMat[2];
                break;

            default:
                break;
        }
        */

        Debug.Log(GetComponent<TextMeshProUGUI>().material);
        currentColor = color;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            IsInside = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            IsInside = false;
        }
    }
}