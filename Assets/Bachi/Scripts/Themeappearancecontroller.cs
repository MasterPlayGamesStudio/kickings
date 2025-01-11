using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Themeappearancecontroller : MonoBehaviour
{

    [HeaderAttribute(" ASSIGN MAIN CAMERA")]
    [Space(10)]

    public Camera MainCameraobj;
    public Light Directionlightref;
    //[Space(10)]
    //[HeaderAttribute(" ALL THEME RENDER OBJS RELATEED")]
    //public MeshRenderer[] Allrenderobjects;

    [System.Serializable]
    public class Themeproperties
    {
        [HideInInspector]
        public string name;
        [HeaderAttribute(" CAMERA RELATEED")]
        public CameraClearFlags currentcamerflag;
        public Color Applycolortocamera;

        public float Lightintensityvalye = 1;

        [Space(10)]
        [HeaderAttribute(" RENDERSETTINGS RELATEED")]
        public Material Skyboxmaterial;
        public Color Fogcolor;


        [Space(30)]
        [HeaderAttribute(" THEME MATERIALS RELATEED")]
        public Meshrendererproperties[] Allrenderobjs;


        [Space(20)]
        [HeaderAttribute(" THEME ENBALE RELATED")]
        public GameObject[] Themeobjectstoenable;

      
    }

    [System.Serializable]
    public class Meshrendererproperties
    {
        [HeaderAttribute(" RENDER RELATED ")]
        public MeshRenderer Currentrednderobj;
        public Material[] currentobjmaterials;

    }


  


    [Space(10)]
    [HeaderAttribute(" ASSIGN THEME MODES")]   
    public Themeproperties[] Themepropertiesref;


    public static int indexvalue=0;
    public static int Themeindexvalue = -1;

    // Start is called before the first frame update
    void Start()
    {
        if(Gamemanager.Instance)
        {
            MainCameraobj = Gamemanager.Instance.Cameraaimator.GetComponent<Camera>();
        }


        if (Themeindexvalue<0)
        {
            Themeindexvalue = Database.Getthemenumber;
            indexvalue=0;
            Debug.Log("Exist in theme indexvalue init...");
        }
        else
        {
           // Debug.Log("Exist in theme indexvalue init..."+Themeindexvalue+" GET ::"+Database.Getthemenumber);

            if (Themeindexvalue!=Database.Getthemenumber)
            {
                Themeindexvalue = Database.Getthemenumber;
                indexvalue = 0;
            }
        }
        if(Database.Levelsnumber%3==1)
        {
            indexvalue = 0;
        }

        if (Database.Levelsnumber % 3 == 2)
        {
            indexvalue = 1;
        }

        if (Database.Levelsnumber % 3 == 0)
        {
            indexvalue = 2;
        }


#if UNITY_EDITOR

          Testmodevalue = Mathf.Clamp(Testmodevalue, 0, 3);
          indexvalue = Testmodevalue;

#endif

        indexvalue = Mathf.Clamp(indexvalue, 0, Themepropertiesref.Length);
        Themeproperties temp = Themepropertiesref[indexvalue];

        if (MainCameraobj)
        {
            MainCameraobj.clearFlags = temp.currentcamerflag;
            MainCameraobj.backgroundColor = temp.Applycolortocamera;
        }

        if (Directionlightref)
        {
            Directionlightref.intensity = temp.Lightintensityvalye;
        }


        if (temp.Allrenderobjs.Length>0)
        {
            for(int i=0;i<temp.Allrenderobjs.Length;i++)
            {
                temp.Allrenderobjs[i].Currentrednderobj.sharedMaterials = temp.Allrenderobjs[i].currentobjmaterials;
            }
        }
        if (temp.Themeobjectstoenable.Length > 0)
        {
            for (int i = 0; i < temp.Themeobjectstoenable.Length; i++)
            {
                temp.Themeobjectstoenable[i].gameObject.SetActive(true);
            }
        }

        if(temp.Skyboxmaterial!=null)//&& temp.currentcamerflag==CameraClearFlags.Skybox
        {
            RenderSettings.skybox = temp.Skyboxmaterial;
        }

        RenderSettings.fogColor = temp.Fogcolor;

        indexvalue++;
        indexvalue = Mathf.Clamp(indexvalue, 0, 3);
    }


    [Space(20)]
    [HideInInspector]
    public bool Setlenghtnow;

    [Space(20)]
    [HideInInspector]

    public bool Getallobjectsnow;

    [Space(20)]
    [HideInInspector]

    public bool  Getdefaultmaterial;

    public MeshRenderer[] Allobjs;

    [Space(50)]

    public int Testmodevalue = 0;
    [HideInInspector]
    public int Testthemepropertyvalue = 0;

    private void Update()
    {
        if (Allobjs.Length<=0)
            return;


        if(Setlenghtnow)
        {
            Setlenghtnow = false;
            Themeproperties temp = Themepropertiesref[Testthemepropertyvalue];
           

            temp.Allrenderobjs = new Meshrendererproperties[Allobjs.Length];
          
            
        }


        if (Getallobjectsnow)
        {


            Themeproperties temp = Themepropertiesref[Testthemepropertyvalue];


           
            for (int i = 0; i < Allobjs.Length; i++)
            {
               
                MeshRenderer tempmeshrereder = Allobjs[i].GetComponent<MeshRenderer>();
                temp.Allrenderobjs[i].Currentrednderobj = tempmeshrereder;
              



            }
            Getallobjectsnow = false;



        }
        if (Getdefaultmaterial)
        {
           

            Themeproperties temp = Themepropertiesref[Testthemepropertyvalue];


                
            for(int i=0;i<Allobjs.Length;i++)
            {
             
                MeshRenderer tempmeshrereder = Allobjs[i].GetComponent<MeshRenderer>();
               
                temp.Allrenderobjs[i].currentobjmaterials = new Material[tempmeshrereder.sharedMaterials.Length];
               temp.Allrenderobjs[i].currentobjmaterials = tempmeshrereder.sharedMaterials;

               
               
            }
            Getdefaultmaterial = false;



        }
    }


}
