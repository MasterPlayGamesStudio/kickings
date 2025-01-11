using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levelcompletescript : MonoBehaviour
{

    public Text Rewardtext;
    public Text Stagetext;
    public Text Totalcoinstext;

    public bool Get5xButtonclicked;
    public GameObject Nextbutton;
    // Start is called before the first frame update

    public Characterupgradepopup _Checkcharacterunlockpopup;

    public GameObject Coinscollectedset;


    private void OnEnable()
    {

        Database.Levelsnumber++;

        Get5xButtonclicked = false;

        Rewardtext.text = Leveldatahandler.Instance.GetRewardvalue.ToString();
        Stagetext.text = "Stages " + (Database.Levelsnumber-1) + " / " + Database.Totallevels;
        Database.Totalcoins += Leveldatahandler.Instance.GetRewardvalue;
        Totalcoinstext.text = Database.Totalcoins.ToString();





        // Nextbutton.SetActive(false);
         Invoke("Enablecoinset", 1.25f);

    }

    private void Update()
    {
        if(Getextracoins)
        {
            Getextracoins = false;
            Database.Totalcoins += 1000;
            Totalcoinstext.text = Database.Totalcoins.ToString();
        }
    }
    void Enablecoinset()
    {
        Coinscollectedset.SetActive(true);
    }

    void Enablenextbutton()
    {
        Nextbutton.SetActive(true);
    }


    public void Get5Xrewardbutton()
    {
        Database.Totalcoins += Leveldatahandler.Instance.GetRewardvalue * 5;
        Nextbuttonclicked();

    }

    public void Nextbuttonclicked()
    {

        if (_Checkcharacterunlockpopup.gameObject.activeSelf==false &&(Database.Levelsnumber==7 || Database.Levelsnumber==13))
        {
            _Checkcharacterunlockpopup.gameObject.SetActive(true);
            return;
        }


        if (AdManager.instance != null)
        {
            AdManager.instance.RunActions(AdManager.PageType.LevelComplete, Database.Levelsnumber, () =>
            {
                UIcontrol.Obj.Nextbuttonclicked(false);
            });
        }
        else
        {
            UIcontrol.Obj.Nextbuttonclicked(false);
        }


        /*
        #if ADSETUP_ENABLED
                if (AdManager.instance)
                {
                    AdManager.instance.CheckUpgradeUnlockPopup(Database.Levelsnumber, () =>
                    {
                        Debug.Log("Exists in this...");
                        if (AdManager.instance)
                        {
                            AdManager.instance.RunActions(AdManager.PageType.LC, Database.Levelsnumber,Database.Totalcoins);
                        }
                        Debug.Log("Exists in this...00000");
                        UIcontrol.Obj.Nextbuttonclicked(false);


                    });
                }
        #endif

                else
                {

        #if UNITY_EDITOR

                    UIcontrol.Obj.Nextbuttonclicked(false);
        #endif
                }*/



    }

    public void Retrybuttonclicked()
    {
        Database.Levelsnumber--;
        UIcontrol.Obj.Retrybuttonclicked();

    }


    public bool Getextracoins;
    public void Watchvideotogetextracoins()
    {
        Getextracoins = false;
#if ADSETUP_ENABLED
        if (AdManager.instance)
            AdManager.instance.ShowRewardVideoWithCallback((result) =>
            {
                if (result)
                {

                    Getextracoins = true;


                }

            });
#endif

#if UNITY_EDITOR

        Database.Totalcoins += 1000;

#endif
    }
}
