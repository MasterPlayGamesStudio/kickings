using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Characterscontainer : MonoBehaviour
{
    // Start is called before the first frame update

        

    public GameObject[] Allcharacters;

    private static Characterscontainer _instance;

    public static Characterscontainer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Characterscontainer();
            }
            return _instance;

        }
    }

    Gamemanager Currentgamemanager;
    Gamesoundmanager Currentgamesoundmanager;

    public Canvas Upgradecanvas;
    public CanvasScaler Upgradecanvasscaler;
    public GraphicRaycaster Upgradecanvasraycaster;

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);

    }


    private void OnEnable()
    {

     
        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
            Currentgamemanager.Isgamerunning = false;
        }

        Gamemanager.Inputrestricted = true;

        if(Gamesoundmanager.Instance)
        {
            Currentgamesoundmanager = Gamesoundmanager.Instance;
        }
        Setcanvasstatus(true);
    }

    private void OnDisable()
    {
        Setcanvasstatus(false);
        if(Currentgamemanager)
        Currentgamemanager.Isgamerunning = true;


    }
    public void Showselectedchar(int indexvalue)
    {
        //Debug.Log("Index value..." + indexvalue);
        for(int i=0;i<Allcharacters.Length;i++)
        {
            Allcharacters[i].SetActive(false);
        }

        Allcharacters[indexvalue-1].SetActive(true);

    }

    public void Closebuttonfunc()
    {
        //Debug.Log(":: " + Gamemanager.Createplayerindexvalue + " :: " + Database.Playerselectedindex);
        if(Gamemanager.Createplayerindexvalue!=Database.Playerselectedindex)
        {
           Currentgamemanager.Player.gameObject.SetActive(false);
            bool Playerfound=false;
            for(int i=0;i<Currentgamemanager.Allplayers.Count;i++)
            {
                if(Currentgamemanager.Allplayers[i].Playerindexvalue==Database.Playerselectedindex)
                {
                    Currentgamemanager.Allplayers[i].gameObject.SetActive(true);
                    Playerfound = true;
                    break;
                }
            }

            if(!Playerfound)
            Currentgamemanager.Playercreator.Createplayernow();

           // Debug.Log("Exists in if contn");
        }
        else
        {
            for (int i = 0; i < Currentgamemanager.Allplayers.Count; i++)
            {
                Currentgamemanager.Allplayers[i].gameObject.SetActive(false);

                if (Currentgamemanager.Allplayers[i].Playerindexvalue == Database.Playerselectedindex)
                {
                    Currentgamemanager.Allplayers[i].gameObject.SetActive(true);
                    break;
                }
            }

           // Debug.Log("Exists in else  contn");

        }
        Closeupgrade();
        
    }

    public void Closeupgrade()
    {
        gameObject.SetActive(false);
        Gamemanager.Inputrestricted = false;
        Currentgamesoundmanager.Playingameclapsound(false);

    }

    public void Showupgrade()
    {
        gameObject.SetActive(true);
    }

    void Setcanvasstatus(bool Status)
    {
        Upgradecanvas.enabled = Status;
        Upgradecanvasraycaster.enabled = Status;
        Upgradecanvasscaler.enabled = Status;
    }


    public Characterunlock Currentcharacterref;
    public void Selectbuttonclicked()
    {

        Currentcharacterref.Selectbuttonfunction();
        Closebuttonfunc();
    }

    public void Watchvideobuttonclicked()
    {
        Currentcharacterref.Unlockcharacter();
    }
}
