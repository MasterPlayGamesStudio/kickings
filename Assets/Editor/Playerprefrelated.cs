using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Playerprefrelated : EditorWindow
{
    // Start is called before the first frame update
    public enum Datatype
    {
        None,Levels,Characters
    }
    public Datatype CurrentDatatype;

    private SerializedObject so;
    private SerializedProperty stringsProp;

    private void OnEnable()
    {
        so = new SerializedObject(this);
        stringsProp = so.FindProperty("CurrentDatatype");


    }
    [MenuItem("MY CUSTOM/DATA RELATED %t")]
    public static void Showwindow()
    {
        EditorWindow.GetWindow(typeof( Playerprefrelated));
    }

    public int Levelnumber;


   

    private void OnGUI()
    {
       
        
        GUILayout.Label("UNLOCK DATA ", EditorStyles.boldLabel);

      

        EditorGUILayout.PropertyField(stringsProp, true);
        so.ApplyModifiedProperties();

        if (CurrentDatatype == Datatype.Levels)
        {
            Levelnumber = EditorGUILayout.IntField("Assingn Level Number :: ", Levelnumber);

            if (GUILayout.Button("SET LEVEL DATA"))
            {
                Levelnumber = Mathf.Clamp(Levelnumber, 1, 500);
                Database.Levelsnumber = Levelnumber;
                this.Close();
            }
        }
        if (CurrentDatatype == Datatype.Characters)
        {
            if (GUILayout.Button("UNLOCK ALL CHARACTERS"))
            {

                Database.Unlockallcharacters();
                this.Close();

            }
        }
    }
}
