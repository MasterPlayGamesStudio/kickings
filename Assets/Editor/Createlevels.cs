using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class Createlevels : EditorWindow
{

    #region CREATE LEVEL WINDOW

        [MenuItem("MY CUSTOM/LEVEL EDITOR %l")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(Createlevels));        
        }

    #endregion


    #region LEVEL DATA RELATED

        public int Startinglevelindex;
        public int Endinglevelindex;

        public int initialhealthvalue;
        public int Healthincreaseratio;
        public int iniitalrewardvalue;
        public int AIplayerindexvalue;

        private int _templevelindexvalue;

        public bool Modifyhealth;
        public bool Modifydifficulty;
        public bool Modifyhealthincreaseratio;
        public bool ModifyAIplayerindexvalue;
        public bool Modifyrewardvalue;
   
        public enum Playedifficulty
        {
            Easy, Medium, Difficult
        }
       

        public Playedifficulty[] Alllevel;

    #endregion

    private void OnEnable()
    {
        so = new SerializedObject(this);
        stringsProp = so.FindProperty("Alllevel");
    }

    private SerializedObject so;
    private SerializedProperty stringsProp;

    void OnGUI()
    {
        GUILayout.Label("LEVEL CREATOR ", EditorStyles.boldLabel);

        Leveldata dataobj =(Leveldata) AssetDatabase.LoadAssetAtPath("Assets/Bachi/Level Data.asset",typeof(Leveldata));
       
        object obj = EditorGUILayout.ObjectField( dataobj, typeof(object), true);

        Startinglevelindex = EditorGUILayout.IntField(" Start Level Number :: ", Startinglevelindex);
        Endinglevelindex = EditorGUILayout.IntField(" End level index :: ", Endinglevelindex);
        initialhealthvalue = EditorGUILayout.IntField(" Initialhelath :: ", initialhealthvalue);
        Healthincreaseratio = EditorGUILayout.IntField(" Increase value :: ", Healthincreaseratio);

        iniitalrewardvalue = EditorGUILayout.IntField(" Initial reward :: ", iniitalrewardvalue);

        ModifyAIplayerindexvalue = EditorGUILayout.Toggle(" Modify Playerindex :: ", ModifyAIplayerindexvalue);
        Modifydifficulty = EditorGUILayout.Toggle(" Modify Difficulty :: ", Modifydifficulty);
        Modifyhealth = EditorGUILayout.Toggle(" Modify Health :: ", Modifyhealth);
        Modifyhealthincreaseratio = EditorGUILayout.Toggle(" Modify Increaseratio :: ", Modifyhealthincreaseratio);
        Modifyrewardvalue = EditorGUILayout.Toggle(" Modify Reward :: ", Modifyrewardvalue);        

        EditorGUILayout.PropertyField(stringsProp, true);

        so.ApplyModifiedProperties();
                
        if (GUILayout.Button("CREATE LEVELS"))
        {
           // return;
            _templevelindexvalue = 0;
            int tempcount = 0;
            int templvlcount = 0;
            if (Startinglevelindex> dataobj.Alllevesinfos.Length || Endinglevelindex >dataobj.Alllevesinfos.Length)
            {
               

                Leveldata.Levelinfo[] oldarray = dataobj.Alllevesinfos;
                Leveldata.Levelinfo[] Newarray = new Leveldata.Levelinfo[Endinglevelindex];

                System.Array.Copy(oldarray, 0, Newarray, 0, oldarray.Length);
                dataobj.Alllevesinfos = Newarray;

                EditorUtility.SetDirty(dataobj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            Leveldata.Levelinfo[] Allleveldata = dataobj.Alllevesinfos;

            int tempnewindexvalue = 0;
            int assingcharhelathvalue = 0;

            for (int i=Startinglevelindex-1;i< Endinglevelindex; i++)
            {
              
                _templevelindexvalue = i;



                Debug.Log("Index value" + Allleveldata[_templevelindexvalue]);

                int previouslevelcharindexx = Allleveldata[i-1].Characterindexvalue;
                int assigncharindexvalue = Randomindexvalue;
             
                //***********************AI HEALTH MODIFIED**************************************
                if (Modifyhealth && Modifyhealthincreaseratio)
                {
                    //assingcharhelathvalue = initialhealthvalue + (Healthincreaseratio * templvlcount);
                    if(assingcharhelathvalue==0)
                    assingcharhelathvalue = initialhealthvalue + (Healthincreaseratio + tempnewindexvalue); 
                    else
                        assingcharhelathvalue += (Healthincreaseratio + tempnewindexvalue);

                    Allleveldata[_templevelindexvalue].AIplayerhealth = assingcharhelathvalue;

                    int randomvalu = UnityEngine.Random.Range(1, 100);
                    if(randomvalu>10)
                    {
                        tempnewindexvalue++;
                    }

                }

                //***********************AI INDEXVALUES**************************************

                if (ModifyAIplayerindexvalue && i%5!=0)
                {
                    Allleveldata[_templevelindexvalue].Characterindexvalue = assigncharindexvalue;
                }

                //***********************LEVEL REWARD VALUES**************************************

                if (Modifyrewardvalue)
                {
                    int assingrewardvalue =  (100 * (templvlcount+2));
                    Allleveldata[_templevelindexvalue].Levelrewardvalue = assingrewardvalue;

                }

                //***********************AI DIFFICULTY**************************************

                if (Modifydifficulty)
                {
                    if (Alllevel[tempcount] == Playedifficulty.Easy)
                    {
                        Allleveldata[_templevelindexvalue].CurrentAIplayerLevel = Leveldata.Levelinfo.AIlevel.Easy;

                    }

                    if (Alllevel[tempcount] == Playedifficulty.Medium)
                    {
                        Allleveldata[_templevelindexvalue].CurrentAIplayerLevel = Leveldata.Levelinfo.AIlevel.Medium;

                    }

                    if (Alllevel[tempcount] == Playedifficulty.Difficult)
                    {
                        Allleveldata[_templevelindexvalue].CurrentAIplayerLevel = Leveldata.Levelinfo.AIlevel.Difficult;

                    }

                    tempcount++;
                   
                    if (tempcount > 4)
                    {
                        tempcount = 0;
                    }
                }

                templvlcount++;
            }

            EditorUtility.SetDirty(dataobj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();           

        }

        //**********************BONUS LEVELS********************************

        if (GUILayout.Button("BONUS LEVLES"))
        {
            return;
            _templevelindexvalue = 0;
            int indexcount = 0;
            int tempbonuslevel = 0;
            
            Leveldata.Levelinfo[] Allleveldata = dataobj.Alllevesinfos;

            for (int i = Startinglevelindex; i < Endinglevelindex; i++)
            {
                if (i % 5 == 0)
                {

                    if (indexcount == 0)
                    {
                        indexcount = 100;
                        if (Startinglevelindex > 5)
                        {
                           indexcount= Allleveldata[Startinglevelindex - 6].Characterindexvalue;
                           if(indexcount>111)
                            {
                                indexcount = 100;
                            }
                           else
                            {
                                indexcount++;
                            }
                        }
                       // initialhealthvalue= Allleveldata[Startinglevelindex - 6].AIplayerhealth+ Healthincreaseratio;
                    }
                    _templevelindexvalue = i-1;

                    int assingcharhelathvalue = 0;

                   

                    Debug.Log("Index value" + i+ " "+_templevelindexvalue);

                    

                    //***********************AI HEALTH MODIFIED**************************************
                    if (Modifyhealth && Modifyhealthincreaseratio)
                    {
                        assingcharhelathvalue = initialhealthvalue + (Healthincreaseratio * tempbonuslevel);
                        Allleveldata[_templevelindexvalue].AIplayerhealth = assingcharhelathvalue;

                    }

                    //***********************AI INDEXVALUES**************************************

                    if (ModifyAIplayerindexvalue)
                    {
                        Allleveldata[_templevelindexvalue].Characterindexvalue = indexcount;
                        Debug.Log("Exists in this condtn"+ indexcount);

                    }

                    //***********************LEVEL REWARD VALUES**************************************

                    if (Modifyrewardvalue)
                    {
                        int assingrewardvalue = iniitalrewardvalue + (100 * tempbonuslevel);
                        Allleveldata[_templevelindexvalue].Levelrewardvalue = assingrewardvalue;

                    }

                    //***********************AI DIFFICULTY**************************************



                    indexcount++;
                    if(indexcount>111)
                    {
                        indexcount = 100;
                    }
                    tempbonuslevel ++;
                }
            }

            EditorUtility.SetDirty(dataobj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        //*******************

        if (GUILayout.Button("REDUCE REWARDS"))
        {
            return;
            _templevelindexvalue = 0;
       

            Leveldata.Levelinfo[] Allleveldata = dataobj.Alllevesinfos;

            for (int i = Startinglevelindex; i < Endinglevelindex; i++)
            {


                Allleveldata[i].Levelrewardvalue = Allleveldata[i].Levelrewardvalue / 10;
            }

            EditorUtility.SetDirty(dataobj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
    }

    int Randomindexvalue => UnityEngine.Random.Range(1, 16);

    

}
