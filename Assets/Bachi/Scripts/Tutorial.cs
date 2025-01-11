using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private static Tutorial _instance;
    public static Tutorial Instance
    {
        get => _instance;
        
    }

    #region LEVEL 2 RELATED

    [Space(10)]
    public Button Helathupgradebutton, Powerupgradebutton;
    [Space(10)]
    public GameObject Healthupgradeset,Powerupgradeset;
    public GameObject Blackimg;

    #endregion


    #region LEVEL 3 RELATED

  



    #endregion
    void Awake()
    {
            if(Database.Levelsnumber==2 
            || Database.Levelsnumber==3
            || Database.Levelsnumber==6
            || Database.Levelsnumber==8)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Blackimg.gameObject.SetActive(false);
        Healthupgradeset.SetActive(false);
        Powerupgradeset.SetActive(false);
        if(Database.Levelsnumber==2 )
        {
            
            if (Database.Gethealthtutorialstatus != "true")
            {
                Gamemanager.Inputrestricted = true;
                Invoke("Checknow", 2.1f);
            }


        }
        else if (Database.Levelsnumber == 3)
        {
            if (Database.Getpowertutorialstatus != "true")
            {
                Gamemanager.Inputrestricted = true;
                Invoke("Checknow", 2.1f);
            }

        }
        else if(Database.Levelsnumber == 8)
        {
            if (Database.Gethealthtutorialstatus == "true")
            {
                Gamemanager.Inputrestricted = true;
                Invoke("Checknow", 2.1f);
            }
        }
        else if (Database.Levelsnumber == 6)
        {
            if (Database.Getpowertutorialstatus == "true")
            {
                Gamemanager.Inputrestricted = true;
                Invoke("Checknow", 2.1f);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    void Checknow()
    {
        if (Database.Levelsnumber == 2 || Database.Levelsnumber == 8)
        {
            Powerupgradebutton.gameObject.SetActive(false);
            Blackimg.gameObject.SetActive(true);
            if (Database.Totalcoins < Database.Playerhealthlevelvalue * 50)
            {
                Database.Totalcoins += Database.Playerhealthlevelvalue * 50;
                if(UIcontrol.Obj)
                {
                    UIcontrol.Obj.Checkplayerhealthpriceandleveltext();
                }
            }
            Healthupgradeset.SetActive(true);
        }

        if (Database.Levelsnumber == 3 || Database.Levelsnumber == 6)
        {
            Helathupgradebutton.gameObject.SetActive(false);
            Blackimg.gameObject.SetActive(true);
            if (Database.Totalcoins < Database.Playerpowervalue * 50)
            {
                Database.Totalcoins += Database.Playerpowervalue * 50;
                if (UIcontrol.Obj)
                {
                    UIcontrol.Obj.CheckPlayerpowerandleveltext();
                }
            }
            Powerupgradeset.SetActive(true);
        }
    }
    public void Healthupgradepurchased()
    {
        Helathupgradebutton.interactable = false;
        Powerupgradebutton.interactable = false;
        Blackimg.gameObject.SetActive(false);
        Healthupgradeset.SetActive(false);
        Helathupgradebutton.gameObject.SetActive(false);

        Powerupgradebutton.gameObject.SetActive(false);

        Gamemanager.Inputrestricted = false;

        if (Database.Levelsnumber == 2 )
            Database.Gethealthtutorialstatus = "true";
        else if (Database.Levelsnumber == 8)
            Database.Gethealthtutorialstatus = "false";

        //Debug.Log("Exist in this...00000");

    }


    public void Powerupgradepurchased()
    {
        Helathupgradebutton.interactable = false;
        Powerupgradebutton.interactable = false;
        Blackimg.gameObject.SetActive(false);
        Powerupgradeset.SetActive(false);

        Powerupgradebutton.gameObject.SetActive(false);

        Helathupgradebutton.gameObject.SetActive(false);


        Gamemanager.Inputrestricted = false;

        if (Database.Levelsnumber == 3)
            Database.Getpowertutorialstatus = "true";
        else if (Database.Levelsnumber == 6)
            Database.Getpowertutorialstatus = "false";

       // Debug.Log("Exist in this...");


    }

}
