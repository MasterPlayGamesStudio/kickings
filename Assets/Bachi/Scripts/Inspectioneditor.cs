using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Inspectioneditor : MonoBehaviour
{

    [Header("Text attributes")]
    [TextArea]
    [Tooltip("Dummy")]
    [SerializeField]
    private string str = "Text demo";

    [Multiline]
    [Tooltip("multi line")]
    [SerializeField]
    private string Multiline = "asdf";

    [Header("Float values")]
    [Range(-10, 10)]
    [SerializeField]

    private float rangevalue =0;

    [Space]
    [Header("int range")]
    [Range(-10, 10)]

    [SerializeField]
    private int intvalue = 0;



    [Header("Color Attributes")]
    [SerializeField]
    private Color colorNormal;


    [ColorUsage(false)]
    [SerializeField]
    private Color colorNoAlpha;

    [ColorUsage(true, true, 0.0f, 0.5f, 0.0f, 0.5f)]
    [SerializeField]
    private Color colorHdr;


    [ContextMenu("Choose Random Values")]
    private void ChooseRandomValues()
    {
        rangevalue = Random.Range(-5f, 5f);
        intvalue = Random.Range(-5, 5);
    }


    [Header("Context Menu Items")]
    [ContextMenuItem("RandomValue", "RandomizeValueFromRightClick")]
    [SerializeField]
    private float randomValue;

    private void RandomizeValueFromRightClick()
    {
        randomValue = Random.Range(-5f, 5f);
    }
}
