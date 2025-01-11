using UnityEngine;
using UnityEditor;
using UnityEngineInternal;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

public class Createcharacter : EditorWindow
{
    public string Playernametobe = "Commonplayer";
    public Object Player;
     Object Animatorcontroller;


    // Add menu item named "My Window" to the Window menu
    //new GUIContent ("MenuItem1 %g"), false, Callback, "item 1");
    //[MenuItem("Window/Upgrade Character")]
   
    [MenuItem("MY CUSTOM/UPGRADE CHARACTER/CREATE CHARACTER %g")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(Createcharacter));
    }

    [MenuItem("MY CUSTOM/CLEAR PLAYERPREF %e")]
    public static void Donothing()
    {
        //Show existing window instance. If one doesn't exist, make one.
        Debug.Log("Playerpref deleted successfully");
        EditorUtility.DisplayDialog("Playerpref related", "Deleted successfully", "OK");
        PlayerPrefs.DeleteAll();
    }

    /*
    public string Keyvalue = "";
    [MenuItem("CUSTOM/Upgrade Character/Delete1 %w")]
    public static void SetPlayerpref()
    {
        //Show existing window instance. If one doesn't exist, make one.
        Debug.Log("checking the working for this...");
        int num = EditorUtility.DisplayDialogComplex("setkey", "setting key", "Ok", "cancel", "");
        switch(num)
        {
            case 0:
                break;

            case 1:
                break;
            case 2:
                break;
             default:
                break;

        }
    }
    */

    void OnGUI()
    {
        GUILayout.Label("Character settings", EditorStyles.boldLabel);
        Playernametobe = EditorGUILayout.TextField("Assingn Player Name :: ", Playernametobe);

        Player = EditorGUILayout.ObjectField(Player, typeof(Object), true);
       // Animatorcontroller = EditorGUILayout.ObjectField(Animatorcontroller, typeof(Object), true);
              
        if (GUILayout.Button("Create"))
        {   

            Createplayenow();
        }
    }

    void Createplayenow()
    {


        GameObject obj = (GameObject)Instantiate(Player);
        obj.transform.gameObject.layer = 8;
        obj.transform.name = Playernametobe;
        List<Rigidbody> Allbodypartsnew=new List<Rigidbody>();
        if(obj.GetComponent<Animator>())
        {

            var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/Animation.controller");


            obj.GetComponent<Animator>().runtimeAnimatorController = controller;
           
            foreach(Rigidbody rr in obj.GetComponentsInChildren<Rigidbody>())
            {
                Allbodypartsnew.Add(rr);
            }
        }
        if(obj.GetComponent<CapsuleCollider>()==false)
        {
            obj.AddComponent<CapsuleCollider>();
            obj.GetComponent<CapsuleCollider>().radius = 2;
            obj.GetComponent<CapsuleCollider>().height = 6;
            Vector3 center = obj.GetComponent<CapsuleCollider>().center;
            center = new Vector3(0, 3, 0);
            obj.GetComponent<CapsuleCollider>().center = center;

        }

        if(obj.GetComponent<Playercontroller>()==false)
        {
            obj.AddComponent<Playercontroller>();
            obj.GetComponent<Playercontroller>().Playeranim=obj.GetComponent<Animator>();
            obj.GetComponent<Playercontroller>().Currentplayercollider = obj.GetComponent<CapsuleCollider>();
            obj.GetComponent<Playercontroller>().Allbodyparts = Allbodypartsnew.ToArray();
            foreach (var rr in Allbodypartsnew)
            {
                rr.gameObject.layer = 9;
            }
        }



        if (obj.GetComponent<AIplayercontroller>() == false)
        {
            obj.AddComponent<AIplayercontroller>();
            obj.GetComponent<AIplayercontroller>().Playeranim = obj.GetComponent<Animator>();
            obj.GetComponent<AIplayercontroller>().Currentplayercollider = obj.GetComponent<CapsuleCollider>();
            obj.GetComponent<AIplayercontroller>().Allbodyparts = Allbodypartsnew.ToArray();

            foreach (var rr in Allbodypartsnew)
            {
                rr.gameObject.layer = 10;
            }

        }
    }
}
