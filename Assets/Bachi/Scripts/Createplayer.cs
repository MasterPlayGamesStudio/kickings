using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createplayer : MonoBehaviour
{
    // Start is called before the first frame update

    public int Healthvalue = 100;
    public enum Createplaertype
    {
        Player,AIplayer
    }
    [Header("Create Player realted stuff")]
    public Createplaertype CurrentAIplayertype;

    public enum AIplayerlevel
    {
        Easy,Medium,Hard
    }

    [Header("AIplayer realted stuff")]
    public AIplayerlevel CurrentAIplayerlevel;


    private Leveldatahandler CurrentLeveldatahandler;
    private void Awake()
    {

        if (CurrentAIplayertype==Createplaertype.Player)
        {
            Createplayernow();

        }
        else
        {
            CreateAIplayernow();
        }
    }


    public void Createplayernow()
    {
#if UNITY_EDITOR
        //Database.Playerselectedindex = 17;
#endif
        if (Gamemanager.Createplayerindexvalue < 0)
        {
            Gamemanager.Createplayerindexvalue = Database.Gethighestcharacterunlocked();
        }
        else
        {
            Gamemanager.Createplayerindexvalue = Database.Playerselectedindex;
        }

        GameObject obj = (GameObject)Instantiate(Resources.Load("Commonplayer" + Database.Playerselectedindex), transform.position, Quaternion.identity);

        if (obj.GetComponent<AIplayercontroller>())
        {
            Destroy(obj.GetComponent<AIplayercontroller>());
        }
        obj.name = "Player";

        if(Gamemanager.Instance)
        {
            Gamemanager.Instance.Playercreator = this;
        }

        if (Leveldatahandler.Instance)
        {
            CurrentLeveldatahandler = Leveldatahandler.Instance;

            if (obj.GetComponent<Playercontroller>())
            {
                //Debug.Log("Before " + Database.GetPlayerhealthvalue(Database.Playerselectedindex));
                //int _healthvalue = (CurrentLeveldatahandler.GetPlayerhealth > Database.GetPlayerhealthvalue(Database.Playerselectedindex)) ?
                //                CurrentLeveldatahandler.GetPlayerhealth : Database.GetPlayerhealthvalue(Database.Playerselectedindex);

                int _healthvalue = Database.GetPlayerhealthvalue;




                //Debug.Log("After " + Database.GetPlayerhealthvalue(Database.Playerselectedindex));

                obj.GetComponent<Playercontroller>().Healthvalue = _healthvalue;


                // Debug.Log("Before " + Database.GetPlayerpowervalue(Database.Playerselectedindex));

                //int _powervalue = (CurrentLeveldatahandler.GetPlayerpower > Database.GetPlayerpowervalue) ?
                //                CurrentLeveldatahandler.GetPlayerpower : Database.GetPlayerpowervalue;


                int _powervalue = Database.GetPlayerpowervalue;


                //Debug.Log("After " + Database.GetPlayerpowervalue(Database.Playerselectedindex));

                obj.GetComponent<Playercontroller>().Powervalue = _powervalue;
                obj.GetComponent<Playercontroller>().Playerindexvalue = Database.Playerselectedindex;

            }
        }
    }


    void CreateAIplayernow()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("Commonplayer" + Leveldatahandler.Instance.GetAIplayerIndexvalue), transform.position, Quaternion.identity);

        if (obj.GetComponent<AIplayercontroller>())
        {


            if (Leveldatahandler.Instance)
            {
                CurrentLeveldatahandler = Leveldatahandler.Instance;

                obj.GetComponent<AIplayercontroller>().Healthvalue = CurrentLeveldatahandler.GetAIplayerhealth;

                if (CurrentLeveldatahandler.GetAIplayerLevel() == Leveldata.Levelinfo.AIlevel.Easy)
                {
                    obj.GetComponent<AIplayercontroller>().currentplayerlevel = AIplayercontroller.AIplayerLevel.Easy;
                }
                if (CurrentLeveldatahandler.GetAIplayerLevel() == Leveldata.Levelinfo.AIlevel.Medium)
                {
                    obj.GetComponent<AIplayercontroller>().currentplayerlevel = AIplayercontroller.AIplayerLevel.Medium;
                }

                if (CurrentLeveldatahandler.GetAIplayerLevel() == Leveldata.Levelinfo.AIlevel.Difficult)
                {
                    obj.GetComponent<AIplayercontroller>().currentplayerlevel = AIplayercontroller.AIplayerLevel.Hard;
                }


            }



        }

        if (obj.GetComponent<AIproperty>())
        {


            if (Leveldatahandler.Instance)
            {
                CurrentLeveldatahandler = Leveldatahandler.Instance;

                obj.GetComponent<AIproperty>().Healthvalue = CurrentLeveldatahandler.GetAIplayerhealth;

                


            }



        }

        if (obj.GetComponent<Playercontroller>())
        {
            Destroy(obj.GetComponent<Playercontroller>());
        }
        obj.name = "AIPlayer";

    }
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

      
        if (CurrentAIplayertype == Createplaertype.Player)
        {
        Gizmos.color = Color.green;

        }
        else
        {
            Gizmos.color = Color.red;

        }
        Gizmos.DrawSphere(transform.position, 1);


#endif
    }
}
