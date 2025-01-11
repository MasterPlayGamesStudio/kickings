using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveldatahandler : MonoBehaviour
{
    public Leveldata AILeveldatacontainer;
    public PlayerData Allplayerdatacontainer;


    private static Leveldatahandler _instance;

    public static Leveldatahandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Leveldatahandler();
            }
            return _instance;
        }
    }


  

    private void Awake() => _instance = this;


    #region AIplayerStuff
        public int GetAIplayerhealth
        {
            get => AILeveldatacontainer.Alllevesinfos[Database.Levelsnumber - 1].AIplayerhealth;
        }

        public int GetAIplayerIndexvalue
        {
            get => AILeveldatacontainer.Alllevesinfos[Database.Levelsnumber - 1].Characterindexvalue;
        
        }        

        public int GetRewardvalue
        {
            get => AILeveldatacontainer.Alllevesinfos[Database.Levelsnumber - 1].Levelrewardvalue;

        }


        public Leveldata.Levelinfo.AIlevel GetAIplayerLevel()
        {
            return AILeveldatacontainer.Alllevesinfos[Database.Levelsnumber - 1].CurrentAIplayerLevel;
        }
    #endregion

    #region PLAYER RELATED STUFF


    public int GetPlayerhealth
    {
        get=> Allplayerdatacontainer.Allplayersinfo[Database.Playerselectedindex - 1].PlayerInitialhealthvalue;
    }

    public int GetPlayerpower
    {
        get => Allplayerdatacontainer.Allplayersinfo[Database.Playerselectedindex - 1].PlayerInitialpowervalue;
    }



    #endregion


}
