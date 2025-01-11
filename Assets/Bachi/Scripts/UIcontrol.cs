using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIcontrol Obj;
    public Camera UIcamera;
    [Space(10)]


    #region PLAYERS RELATED STUFF

        [Header("PLAYER RELATED")]

        public Image Playerhealthbar;
        public Image AIplayerhealthbar;
        public Image Playerpicture, AIplayerpicture;

    [Space(10)]

        public Text Playerhealthtext;
        public Text AIplayerheathtext,Playerdamagetext,AIplayerdamagetext,Playerhealthbarincreasetext;
        public Text Playerhealthleveltext, PlayerPowerleveltext;
        public Text PlayerHelathprictext, PlayerPowerpricetext;

    [Space(10)]

        public GameObject Playerhealthsetup;
         public GameObject AIplayerhealthsetup;
     
        public GameObject Playerhealthpricesetup, Playerhealthwatchvideosetup;
        public GameObject Playerpowerpricesetup, Playerpowerwatchvideosetup;
          
    [Space(20)]

    #endregion

    public Text Coinstext;

    public Text Levelnumbertext;
    public GameObject Taptokickobj;

    [Space(10)]
    [Header("LEVEL COMPLETE RELATED")]

    public GameObject Lcset;
    public GameObject Lfset;
    public Image Lcrandomimg;
    public Sprite[] Allrandomlcimgs;

    [Space(20)]
    public Text Levelstatstext, Buttontext;
    public GameObject Loadingset;
    public GameObject Paperblast;

    public GameObject KOset;

    [Space(10)]
    #region UI RELATED STUFF
    public GameObject Header, Footer, Leftset, Rightset;

    #endregion


    [Space(10)]
    #region UI RELATED STUFF

    public GameObject Bounsulevelstrip;
    public Image[] Availablekickimgs;

    #endregion

    Gamemanager Currentgamemanager;

    Gamesoundmanager Currentgamesoundmanager;

    public static bool Isupgradeshown=false;

    void Awake()
    {
        Obj = this;

        Physics.IgnoreLayerCollision(8, 8, false);
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 10, true);

        Physics.IgnoreLayerCollision(9, 10, false);

        Physics.IgnoreLayerCollision(9, 9, false);
        Physics.IgnoreLayerCollision(10, 10, false);

        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }
    }


    private void Start()
    {
       

        Levelnumbertext.text = "LEVEL " + Database.Levelsnumber;
        Lcset.SetActive(false);
        Lfset.SetActive(false);

        Loadingset.SetActive(false);

        Coinstext.text = Database.Totalcoins.ToString();

        Checkplayerhealthpriceandleveltext();
        CheckPlayerpowerandleveltext();

        currentscene = SceneManager.GetActiveScene();

        SetInUIelelments();

        if(Gamesoundmanager.Instance)
        {
            Currentgamesoundmanager = Gamesoundmanager.Instance;
        }


        Bounsulevelstrip.SetActive(false);

        // UNLOCK PURPOSE COMMENT IT LATER
        //Database.Unlockallcharacters();
        //Database.Totalcoins = 1000000;

        if (Database.Levelsnumber % 3 == 1 && Database.Levelsnumber > 1 && Isupgradeshown==false)
        {
            Invoke("Showupgradepage", 0.5f);
            Isupgradeshown = true;
        }
#if ADSETUP_ENABLED
        if (AdManager.instance)
        {
            AdManager.instance.RunActions(AdManager.PageType.InGame, Database.Levelsnumber);
        }
#endif
    }

    int value = 25;
    private void LateUpdate()
    {
        if(Watchenablepower)
        {
            Gamemanager.Instance.Player.EnablePowerattack();
            Watchenablepower = false;
        }

        if(Watchenabledefence)
        {
            Gamemanager.Instance.Player.EnablePowerdefense();
            Watchenabledefence = false;
        }

        if(Watchincreasehealth)
        {
            Healthincreasesuccesscallback();
            Watchincreasehealth = false;
        }
        if(Watchincreasepower)
        {
            Watchincreasepower = false;
            Powerincreasesuccesscallback();

        }

    
        if ( Currentgamemanager.AIplayer==null || Currentgamemanager.Player==null)
            return;

        
        Playerhealthbar.fillAmount = Mathf.Lerp(Playerhealthbar.fillAmount,  Playerfillvalue, speedvalue);

        Playerhealthbar.fillAmount = Mathf.Clamp(Playerhealthbar.fillAmount, 0.1f, Playerfillvalue);
      
        AIplayerhealthbar.fillAmount = Mathf.Lerp(AIplayerhealthbar.fillAmount, AIplayerfillvalue, speedvalue );

        AIplayerhealthbar.fillAmount = Mathf.Clamp(AIplayerhealthbar.fillAmount, 0.1f, AIplayerfillvalue);

        if (CheckIncreaseplayerhealth)
            {
           
                Currentgamemanager.Player.Healthvalue++;

               
                if(value>0)
                    value--;
                Playerhealthbarincreasetext.text=" + "+value;

                if (Currentgamemanager.Player.Healthvalue == Currentgamemanager.Player.Initialhealthvalue+Database.Increasehealthvalue)
                {
                    CheckIncreaseplayerhealth = false;
                    Currentgamemanager.Player.Initialhealthvalue = Currentgamemanager.Player.Healthvalue;
                     Playerhealthbarincreasetext.gameObject.SetActive(false);
                     value = 25;
                }
                Playerhealthtext.text = Currentgamemanager.Player.Healthvalue.ToString();
                //IntLerp(Currentgamemanager.Player.Healthvalue, Currentplayerhealth, 3).ToString();
                return;
            }
       
            //if(lerp<duration)
            lerp += Time.deltaTime / duration;


            if (Checkplayer)
            {
                Playerdamagetext.text = "- " + ((int)Mathf.Lerp(Playerdamage, 0, lerp));

                Currentplayerhealth = Currentgamemanager.Player.Initialhealthvalue * Playerfillvalue;
                if (Currentplayerhealth < 0)
                {
                    Currentplayerhealth = 0;
                }
                Playerhealthtext.text = ((int)Mathf.Lerp(Playerhealthbar.fillAmount * Currentgamemanager.Player.Initialhealthvalue, Currentplayerhealth, lerp)).ToString();  //IntLerp(Currentgamemanager.Player.Healthvalue, Currentplayerhealth, 3).ToString();
            }
            if (CheckAIplayer)
            {
                AIplayerdamagetext.text = "- " + ((int)Mathf.Lerp(AIplayerdamage, 0, lerp)).ToString();

                currnetAIplayerhealth = Currentgamemanager.AIplayer.Initialhealthvalue * AIplayerfillvalue;
                if (currnetAIplayerhealth < 0)
                {
                    currnetAIplayerhealth = 0;
                }

                AIplayerheathtext.text = ((int)Mathf.Lerp(AIplayerhealthbar.fillAmount * Currentgamemanager.AIplayer.Initialhealthvalue, currnetAIplayerhealth, lerp)).ToString();  //IntLerp(Currentgamemanager.Player.Healthvalue, Currentplayerhealth, 3).ToString();

                // AIplayerheathtext.text = ((int)Mathf.Lerp(Currentgamemanager.AIplayer.Initialhealthvalue, currnetAIplayerhealth, lerp)).ToString(); //IntLerp(Currentgamemanager.AIplayer.Healthvalue, currnetAIplayerhealth, 3).ToString();
            }

        

    }
    float lerp=3,duration=1;
    float speedvalue=0.05f;
    float Playerfillvalue=1;
    float AIplayerfillvalue=1;
    [HideInInspector]
    public float Currentplayerhealth,Playerdamage;
    [HideInInspector]
     public float currnetAIplayerhealth,AIplayerdamage;
    private bool Checkplayer, CheckAIplayer,CheckIncreaseplayerhealth;

    public void CheckPlayerhealthbar(float Fillvalue,int Damagevalue=0)
    {

        if (Damagevalue > 0)
        {
            Playerdamagetext.gameObject.SetActive(true);
            Playerdamage = Damagevalue;

        }
        Playerfillvalue = Fillvalue;
        if(Currentgamemanager.Player)
        Currentplayerhealth = Mathf.RoundToInt(Currentgamemanager.Player.Initialhealthvalue*Fillvalue);

        lerp = 0;
        Checkplayer = true;
        CheckAIplayer = false;

      
        

    }

    public void CheckAIPlayerhealthbar(float Fillvalue,int Damagevalue=0)
    {
        //AIplayerhealthbar.fillAmount = Fillvalue;

        if (Damagevalue > 0)
        {
            AIplayerdamagetext.gameObject.SetActive(true);
            AIplayerdamage = Damagevalue;
        }

        AIplayerfillvalue = Fillvalue;

        if (Currentgamemanager.AIplayer)
            currnetAIplayerhealth = Mathf.RoundToInt(Currentgamemanager.AIplayer.Initialhealthvalue*Fillvalue);

        lerp = 0;
        Checkplayer = false;
        CheckAIplayer = true;
    }

    public void HideplayersHealthsetups()
    {
        //  Playerhealthsetup.SetActive(false);
        //  AIplayerhealthsetup.SetActive(false);
      
       
        SetOutUIelelments();
        Invoke(nameof(Enablekonow), 2);
    }

    void Enablekonow()
    {
        if (Currentgamemanager.Player.Isdead == false)
        {
            KOset.SetActive(true);
            Currentgamesoundmanager.Playwinclapsound();
            Invoke(nameof(Showlevelcomplete), 3);
        }
        else
        {
            Invoke(nameof(Showlevelfail), 3);

        }
    }
    

    void Showlevelfail()
    {

        Bonuslevelnextbutton.SetActive(false);

        Lcset.SetActive(false);
        Lfset.SetActive(false);
        KOset.SetActive(false);

        Lfset.SetActive(true);
        Currentgamesoundmanager.Playlevelfailsound();

        if(Database.Levelsnumber%5==0)
        {
            Bonuslevelnextbutton.SetActive(true);
        }
    }
    void Showlevelcomplete()
    {
        Debug.Log("ShowLevelcomplete");
        Lcset.SetActive(false);
        Lfset.SetActive(false);
        KOset.SetActive(false);

        Paperblast.SetActive(true);
        Currentgamesoundmanager.Playlevelwinsound();

        Lcrandomimg.sprite = Allrandomlcimgs[Random.Range(0, Allrandomlcimgs.Length)];
#if ADSETUP_ENABLED
        if (AdManager.instance)
        {
            AdManager.instance.ShowRatePopWithCallback(Database.Levelsnumber, () =>
            {
                Lcset.SetActive(true);
               

                if (AdManager.instance)
                {
                    AdManager.instance.RunActions(AdManager.PageType.LCPage, Database.Levelsnumber);
                }
            });
        }
#endif
        Lcset.SetActive(true);

#if UNITY_EDITOR

        Lcset.SetActive(true);
        //  Database.Levelsnumber++;
#endif

        Isupgradeshown = false;

    }

    public GameObject Bonuslevelnextbutton;

    public void Bonuslevelnextclicked()
    {
       
        Nextbuttonclicked(true);

    }
    public void Nextbuttonclicked(bool increaselevelnumber=false)
    {
        Debug.Log("Exists in this....1111");
        if (increaselevelnumber)
        {
            Database.Levelsnumber++;
        }

        Gotolevel("Theme" + Database.Getthemenumber);

        Lcset.SetActive(false);
        Lfset.SetActive(false);

       // SceneManager.LoadScene("Theme"+ Database.Getthemenumber);

    }

    Scene currentscene;
    public void Retrybuttonclicked()
    {
        Lcset.SetActive(false);
        Lfset.SetActive(false);

         Gotolevel(currentscene.name);
       
       // SceneManager.LoadScene(currentscene.name);
    }

    void Gotolevel(string themename)
    {
        if (Loadingset.GetComponent<LoadingPage>())
        {
            LoadingPage.Levelname = themename;
            Loadingset.SetActive(true);

            Loadingset.GetComponent<LoadingPage>().Gotolevel();

        }
    }

    public void Resetplayer()
    {
        if(Gamemanager.Instance)
        {
            Gamemanager.Stopchecking = false;
            Gamemanager.Inputrestricted = false;

            Gamemanager.Instance.Player.Resetplayer();
            Gamemanager.Instance.AIplayer.Resetplayer();

            Gamemanager.Instance.Player.transform.position = Gamemanager.Instance.Player.Initialposition;
            Gamemanager.Instance.AIplayer.transform.position = Gamemanager.Instance.AIplayer.Initialposition;

            Gamemanager.Instance.Player.transform.eulerAngles = Gamemanager.Instance.Player.Initialrotation;
            Gamemanager.Instance.AIplayer.transform.eulerAngles = Gamemanager.Instance.AIplayer.Initialrotation;
             Gamemanager.Instance.Camerapoint.transform.localEulerAngles = Gamemanager.Instance.Initalrotation;

            Gamemanager.Instance.Maincameraobj.transform.localPosition = Gamemanager.Instance.Initialposition;
            Gamemanager.Instance.Maincameraobj.transform.localEulerAngles = Gamemanager.Instance.InitialMaincamerarotation;

            Gamemanager.Instance.Cameraaimator.GetComponent<Camera>().fieldOfView = 60;




            Gamemanager.Instance.Playerturn = Gamemanager.Instance.AIplayerturn = false;
        
            Gamemanager.Instance.Cameraaimator.enabled = false;

            Lcset.SetActive(false);
            Lfset.SetActive(false);

            Playerhealthsetup.SetActive(true);
            AIplayerhealthsetup.SetActive(true);

            Gamemanager.Instance.Playerturn = true;

            Gamemanager.Instance.Player.enabled = true;
            Gamemanager.Instance.AIplayer.enabled = true;

            SetInUIelelments(0.25f);

            Currentgamesoundmanager.PlayIngamebgsound(false);

            if(Isbonuslevel)
            {
                Checknoofavialblekicks = 1;
                Enablenoofavailablekicksstrip();
            }
#if ADSETUP_ENABLED
            if (AdManager.instance)
            {
                AdManager.instance.RunActions(AdManager.PageType.InGame, Database.Levelsnumber);
            }
#endif
        }


    }

    #region BUTTON ENABLE METHODS

    [Header("BUTTONS RELATED")]
    [Space(20)]

        public GameObject Upgradebutton;
        public GameObject Powerattackbutton;
        public GameObject Increasepowerbutton;
        public GameObject Increasehealthbutton;
        public GameObject Powerdefensebutton;

    public bool Watchenablepower;
    public bool Watchenabledefence;
    public bool Watchincreasehealth;
    public bool Watchincreasepower;
 

        public void Enablepowebutton()
        {   if (Gamemanager.Stopchecking)
                return;
            if(Powerattackbutton.activeSelf==false)
            {
                Powerattackbutton.SetActive(true);
           
            }
        }


        public void EnablepoweDefensebutton()
        {
            if (Gamemanager.Stopchecking || Isbonuslevel)
                return;

            if (Powerdefensebutton.activeSelf == false)
            {
                Powerdefensebutton.SetActive(true);
           
            }
        }
        public void Enablepowerattack()
        {
             Watchenablepower = false;
#if ADSETUP_ENABLED
        if (AdManager.instance)
                AdManager.instance.ShowRewardVideoWithCallback((result) =>
                {
                    if (result)
                    {
                        Watchenablepower = true;
                       
                    }

                });

#endif


            #if UNITY_EDITOR

                    Gamemanager.Instance.Player.EnablePowerattack();
            #endif

        Disableincreasebuttons();

        }


        public void Enablepowerdefense()
        {
             Watchenabledefence = false;
#if ADSETUP_ENABLED
        if (AdManager.instance)
                AdManager.instance.ShowRewardVideoWithCallback((result) =>
                {
                    if (result)
                    {
                        Watchenabledefence = true;

                    }

                });
#endif



            #if UNITY_EDITOR

                    Gamemanager.Instance.Player.EnablePowerdefense();

            #endif

        Disableincreasebuttons();
        }

    #endregion

    #region BUTTON FUNCTIONALITIES 

            public void Increasehealthbuttonfunc()
            {
                //int increasevalue = Gamemanager.Instance.Player.Healthvalue / 10;

                if (Database.Checkavailabiltyofcoins(currentplayerhealthprice))
                {

                    Database.Totalcoins -= currentplayerhealthprice;
                    Healthincreasesuccesscallback();
                }
                else
                {

                     Debug.Log("Watch ad to unlock");
                         Watchincreasehealth = false;
#if ADSETUP_ENABLED
            if (AdManager.instance)
                            AdManager.instance.ShowRewardVideoWithCallback((result) =>
                            {
                                if (result)
                                {

                                    Watchincreasehealth = true;

                                }

                            });

#endif
                        #if UNITY_EDITOR

                                    Healthincreasesuccesscallback();

                        #endif

        }


        Checkplayerhealthpriceandleveltext();
                CheckPlayerpowerandleveltext();

            }

            void Healthincreasesuccesscallback()
            {
          
             
                Database.SetPlayerhealthvalue(Currentgamemanager.Player.Healthvalue+Database.Increasehealthvalue);
                Database.Playerhealthlevelvalue++;
    
                CheckIncreaseplayerhealth = true;
                Playerhealthbarincreasetext.gameObject.SetActive(true);


                
                Currentgamesoundmanager.Invoke("Playlevelpowersound", 0.5f);
        
                Checkplayerhealthpriceandleveltext();
                CheckPlayerpowerandleveltext();

                if(Tutorial.Instance)
                {
                    Tutorial.Instance.Healthupgradepurchased();
                }


             }


    public void Increasepowerbuttonfunc()
            {
               

                if (Database.Checkavailabiltyofcoins(Currentplaerpowerprice))
                {
                    Database.Totalcoins -= Currentplaerpowerprice;
                    Powerincreasesuccesscallback();

                }
                else
                {
                    Debug.Log("Watch ad to unlock");

                     Watchincreasepower = false;
#if ADSETUP_ENABLED
            if (AdManager.instance)
                        AdManager.instance.ShowRewardVideoWithCallback((result) =>
                        {
                            if (result)
                            {

                                Watchincreasepower = true;


                            }

                        });
#endif

                    #if UNITY_EDITOR

                            Powerincreasesuccesscallback();

                    #endif

                }




                 Checkplayerhealthpriceandleveltext();
                CheckPlayerpowerandleveltext();
            }

            void Powerincreasesuccesscallback()
            {
                Database.SetPlayerpowervalue(Database.GetPlayerpowervalue + Database.Incrasepowervalue);
                Database.Playerpowervalue++;
                Currentgamemanager.Playermeterscriptref.Getvalues();
                Currentgamesoundmanager.Invoke("Playlevelpowersound", 0.5f);

               // Debug.Log("Power purchased " + Tutorial.Instance);

                if (Tutorial.Instance)
                {
                    Tutorial.Instance.Powerupgradepurchased();
                }

            }
    #endregion
    int currentplayerhealthprice;
    int Currentplaerpowerprice;
    public void Checkplayerhealthpriceandleveltext()
    {
        Playerhealthleveltext.text = Database.Playerhealthlevelvalue.ToString();
         currentplayerhealthprice = Database.Playerhealthlevelvalue * 50;
        Playerhealthpricesetup.SetActive(false);
        Playerhealthwatchvideosetup.SetActive(false);
        if(Database.Checkavailabiltyofcoins(currentplayerhealthprice))
        {
            Debug.Log("Coins available");
            PlayerHelathprictext.text = currentplayerhealthprice.ToString();
            Playerhealthpricesetup.SetActive(true);

        }
        else
        {
            Debug.Log("Coins unavailable");
            Playerhealthwatchvideosetup.SetActive(true);

        }

        CheckAvailablecoins();

    }

    public void CheckPlayerpowerandleveltext()
    {
        PlayerPowerleveltext.text = Database.Playerpowervalue.ToString();
        Currentplaerpowerprice = Database.Playerpowervalue * 50;
        Playerpowerwatchvideosetup.SetActive(false);
        Playerpowerpricesetup.SetActive(false);
        if (Database.Checkavailabiltyofcoins(Currentplaerpowerprice))
        {
            Debug.Log("Coins available");
            PlayerPowerpricetext.text = Currentplaerpowerprice.ToString();
            Playerpowerpricesetup.SetActive(true);

        }
        else
        {
            Debug.Log("Coins unavailable");
            Playerpowerwatchvideosetup.SetActive(true);

        }

        CheckAvailablecoins();
    }

    public void Disableincreasebuttons()
    {
        Increasepowerbutton.SetActive(false);
        Increasehealthbutton.SetActive(false);
        if (Powerattackbutton.activeSelf)
        {
            Powerattackbutton.SetActive(false);
        }

        if (Powerdefensebutton.activeSelf)
        {
            Powerdefensebutton.SetActive(false);
        }

        Taptokickobj.SetActive(false);
        Upgradebutton.SetActive(false);


       


    }

    private bool showonlyonce;
    public void Showincreasepowerbuttons()
    {

        Increasepowerbutton.SetActive(false);
        Increasehealthbutton.SetActive(false);

        Taptokickobj.SetActive(false);
        Upgradebutton.SetActive(false);

        if (showonlyonce)
            return;
        showonlyonce = true;

        Increasepowerbutton.SetActive(showonlyonce);
        Increasehealthbutton.SetActive(showonlyonce);

        Taptokickobj.SetActive(true);
        Upgradebutton.SetActive(true);
    }

    

    void CheckAvailablecoins()
    {
        Coinstext.text = Database.Totalcoins.ToString();
    }

    public void Showupgradepage()
    {
        if(Characterscontainer.Instance)
        {
            Characterscontainer.Instance.Showupgrade();

            Currentgamesoundmanager.Playlevelupgradesound();
            Currentgamesoundmanager.Playingameclapsound(true);


        }
    }

    void SetInUIelelments(float delayvalue=1)
    {
        Header.transform.localPosition = Vector3.zero;
        Footer.transform.localPosition = Vector3.zero;
        Leftset.transform.localPosition = Vector3.zero;
        Rightset.transform.localPosition = Vector3.zero;



        iTween.MoveFrom(Header.gameObject, iTween.Hash("y", 500, "time", 1,"delay", delayvalue, "easteype", "easeinoutexpo","islocal",true));
        iTween.MoveFrom(Footer.gameObject, iTween.Hash("y", -500, "time", 1, "delay", delayvalue, "easteype", "easeinoutexpo", "islocal", true));
        iTween.MoveFrom(Leftset.gameObject, iTween.Hash("x", -500, "time", 1, "delay", delayvalue, "easteype", "easeinoutexpo", "islocal", true));
        iTween.MoveFrom(Rightset.gameObject, iTween.Hash("x", 500, "time", 1, "delay", delayvalue, "easteype", "easeinoutexpo", "islocal", true));

    }

    void SetOutUIelelments()
    {
        iTween.MoveTo(Header.gameObject, iTween.Hash("y", 500, "time", 1, "easteype", "easeinoutexpo", "islocal", true));
        iTween.MoveTo(Footer.gameObject, iTween.Hash("y", -500, "time", 1, "easteype", "easeinoutexpo", "islocal", true));
        iTween.MoveTo(Leftset.gameObject, iTween.Hash("x", -500, "time", 1, "easteype", "easeinoutexpo", "islocal", true));
        iTween.MoveTo(Rightset.gameObject, iTween.Hash("x", 500, "time", 1, "easteype", "easeinoutexpo", "islocal", true));
    }

    #region BONUS LEVEL RELATED

    public void Enablenoofavailablekicksstrip()
    {
        Bounsulevelstrip.SetActive(true);
        iTween.MoveTo(Bounsulevelstrip.gameObject, iTween.Hash("y", 0, "time", 0.5f, "easetype", "easeoutback"));
    }

    public bool Isbonuslevel;
    private int _Checknoofavialblekicks=3;
    public int Checknoofavialblekicks
    {
        get
        {
            return _Checknoofavialblekicks;
        }
        set
        {
            _Checknoofavialblekicks = value;

            for (int i=0;i< Availablekickimgs.Length;i++)
            {
                Color col = new Color();
                col.a = 0.5f;
                Availablekickimgs[i].color = col;
            }

            for (int i = 0; i < _Checknoofavialblekicks; i++)
            {
               
                Availablekickimgs[i].color = Color.white;
            }
            if(Checknoofavialblekicks<=0 && !Currentgamemanager.AIplayer.Isdead)
            {
                
                Currentgamemanager.Playerturn = true;
                Currentgamemanager.AIplayerturn = true;
                Currentgamemanager.Dontchangecamera = false;
                Gamemanager.Inputrestricted = true;
                Gamemanager.Stopchecking = true;               
                SetOutUIelelments();
                Invoke("Showlevelfail", 3);
                Bounsulevelstrip.SetActive(false);
                Bonuslevelnextbutton.SetActive(true);
            }
          //  Debug.Log("check available kicks " + Checknoofavialblekicks);

        }
    }


    #endregion

    public GameObject Settingspageobj;
    public void Showsettingspage()
    {
        Settingspageobj.SetActive(true);
    }


    
}
