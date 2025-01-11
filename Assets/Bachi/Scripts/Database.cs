using UnityEngine;
using System;


public class Database
{
    #region DATA KEY RELATED

            static readonly string Levelskey = "Totallevels";

            static readonly string Characterskey = "Characterskey";

            static readonly string Totalcoinskey = "Totalcoinskey";

            static readonly string Playerselectedkey = "Playerselectedindex";

            static readonly string Playerpowerlevel = "Playerpowerlevel";

            static readonly string Playerhealthlevel = "Playerhealthlevel";

            static readonly string Playerhealth = "Playerhealth";

            static readonly string Playerpower = "Playerpower";

            static readonly string Musicstatus = "Musicstatus";

            static readonly string Soundstatus = "Soundstatus";

            static readonly string Vibratestatus = "Vibratestatus";

            static readonly string Charactersvideokey = "Charactersvideokey";

            static readonly string Healthtutorialshown = "Healthtutorialshown";

            static readonly string Powertutorialshown = "Powertutorialshown ";



    #endregion


    #region INITIALIZE RELATE STUFF

    internal static int Increasehealthvalue = 25;
        internal static int Incrasepowervalue = 10;

        internal static int Totallevels = 500;

    #endregion


    #region Player Related Stuff

            public static int Playerselectedindex
            {
                get => PlayerPrefs.GetInt(Playerselectedkey, 1);
                set => PlayerPrefs.SetInt(Playerselectedkey, value);
            }



            public static int Playerhealthlevelvalue
            {
                get => PlayerPrefs.GetInt(Playerhealthlevel, 1);
                set => PlayerPrefs.SetInt(Playerhealthlevel, value);
            }

            public static int Playerpowervalue
            {
                get => PlayerPrefs.GetInt(Playerpowerlevel, 1);
                set => PlayerPrefs.SetInt(Playerpowerlevel, value);
            }

            public static void SetPlayerhealthvalue(int addedhealthvalue)=>PlayerPrefs.SetInt(Playerhealth, addedhealthvalue);
            


            public static int  GetPlayerhealthvalue=> PlayerPrefs.GetInt(Playerhealth, 100);
            


            public static void SetPlayerpowervalue(int addedpowervalue)=>PlayerPrefs.SetInt(Playerpower, addedpowervalue);
            


            public static int GetPlayerpowervalue=> PlayerPrefs.GetInt(Playerpower, 35);
        
            

    #endregion


    #region Level Data

            public static int Levelsnumber
            {
                get => PlayerPrefs.GetInt(Levelskey, 1);
                set => PlayerPrefs.SetInt(Levelskey, value);
            }


            public static int Getthemenumber
            {
                get
                {
                    int Themenumber =(Levelsnumber%15==0)? 15 : Levelsnumber%15;
                     float divisable = Themenumber / 3f;
                    return Mathf.CeilToInt(divisable);                   

                }
            }

    #endregion

    #region Coins RELATED

        public static int Totalcoins
        {
            get => PlayerPrefs.GetInt(Totalcoinskey, 0);
            set => PlayerPrefs.SetInt(Totalcoinskey, value);                    
        }

        public static bool Checkavailabiltyofcoins(int priceamount) =>Totalcoins >= priceamount;
        


    #endregion

    #region UPGRADE RELATED

            public static string GetAllcharacters
            {
                get => PlayerPrefs.GetString(Characterskey, "10000000000000000000");
                set=>PlayerPrefs.SetString(Characterskey,value);
            }
    
            public static bool Getcharacterstatus(int indexvalue)=>GetAllcharacters.ToCharArray()[indexvalue-1] == '1';
          

            public static void Unlockcharacter(int indexvalue)
            {
                char[] allchar = GetAllcharacters.ToCharArray(); 
                allchar[indexvalue - 1] = '1';
                GetAllcharacters = new string(allchar);
                //Debug.Log("char " + GetAllcharacters);

            }

            public static int Gethighestcharacterunlocked()
            {
                int characterindexvalue = 0;
                char[] allchar = GetAllcharacters.ToCharArray();
                for (int i = 0; i < allchar.Length; i++)
                {
                    if (allchar[i] == '1')
                        characterindexvalue = i;
                }

                return characterindexvalue;
            }

            public static void Unlockallcharacters()=>PlayerPrefs.SetString(Characterskey, "11111111111111111111");
          


    #endregion

    #region UPGRADE VIDEO  RELATED

            public static string GetAllcharactersvideokey
            {
            get => PlayerPrefs.GetString(Charactersvideokey, "00000000000000000000");
            set => PlayerPrefs.SetString(Charactersvideokey, value);
            }

            public static void Addwatchvideovalue(int indexvalue)
            {
            char[] allchar = GetAllcharactersvideokey.ToCharArray();
            char temp1 = allchar[indexvalue - 1];
            int temp = Mathf.Abs( (int)char.GetNumericValue(temp1))-1;
            if (temp < 0)
                temp = 0;
            allchar[indexvalue - 1] = char.Parse(temp.ToString());
             GetAllcharactersvideokey = new string(allchar);
            //Debug.Log("char " + GetAllcharacters);

            }

            public static int Getcharactervideostatus(int indexvalue)
            {         
            return Mathf.Abs((int)char.GetNumericValue( GetAllcharactersvideokey.ToCharArray()[indexvalue - 1]));
            }

            public static void Assignwatchvideovalue(int indexvalue,int Noofvideos)
            {
            char[] allchar = GetAllcharactersvideokey.ToCharArray();
            allchar[indexvalue - 1] = char.Parse( Noofvideos.ToString());
            GetAllcharactersvideokey = new string(allchar);
            //Debug.Log("Videos to watch " + Noofvideos);

            }

   

    #endregion


    #region SOUND RELATED STUFF

    public static string GetSoundstatus
    {
        get => PlayerPrefs.GetString(Soundstatus, "On");
        set => PlayerPrefs.SetString(Soundstatus, value);
    }

    public static string GetMusictatus
    {
        get => PlayerPrefs.GetString(Musicstatus, "On");
        set => PlayerPrefs.SetString(Musicstatus, value);
    }

    public static string GetVibratestauts
    {
        get => PlayerPrefs.GetString(Vibratestatus, "On");
        set => PlayerPrefs.SetString(Vibratestatus, value);
    }

    #endregion

    #region TUTORIAL RELATED

    public static string Gethealthtutorialstatus
    {
        get => PlayerPrefs.GetString(Healthtutorialshown, "false");
        set => PlayerPrefs.SetString(Healthtutorialshown, value);
    }

    public static string Getpowertutorialstatus
    {
        get => PlayerPrefs.GetString(Powertutorialshown, "false");
        set => PlayerPrefs.SetString(Powertutorialshown, value);
    }


    #endregion
    public delegate void Coinsupdated();
    public static event Coinsupdated OnUpdatecoins;

   
}
