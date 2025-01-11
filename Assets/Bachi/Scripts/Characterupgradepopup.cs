using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Characterupgradepopup : MonoBehaviour
{

    #region CHAR UPGRADE RELATED

    public Image Charimg;
    public Sprite[] Allcharimgs;

    public Levelcompletescript _levelcompleteref;
    [SerializeField]
    private int Chartobeunlockedindexvalue = -1;

    private int[] Checkselectedcharindex = new int[] { 2, 3, 4, 6 };

    private bool Isvideowatched;

    #endregion

    private void OnEnable()
    {
        Chartobeunlockedindexvalue = -1;
        for (int i=0;i<Checkselectedcharindex.Length;i++)
        {
            if(Database.Getcharacterstatus(Checkselectedcharindex[i])==false)
            {
                Chartobeunlockedindexvalue = Checkselectedcharindex[i];
                Charimg.sprite = Allcharimgs[i];
                break;
            }
        }
       
    }

    private void Update()
    {
        if(Isvideowatched)
        {
            Isvideowatched = false;
            Unlockcharacternow();
        }
    }
    
    void Unlockcharacternow()
    {
        if (Chartobeunlockedindexvalue != -1)
        {
            Database.Unlockcharacter(Chartobeunlockedindexvalue);
            Database.Playerselectedindex = Chartobeunlockedindexvalue;
        }
        _levelcompleteref.Nextbuttonclicked();
        gameObject.SetActive(false);
    }


    public void Closebuttonclicked()
    {
        _levelcompleteref.Nextbuttonclicked();
        gameObject.SetActive(false);
    }

    public void Claimbuttonclicked()
    {
        if (Chartobeunlockedindexvalue != -1)
        {
            Database.Unlockcharacter(Chartobeunlockedindexvalue);
            Database.Playerselectedindex = Chartobeunlockedindexvalue;
        }

        _levelcompleteref.Nextbuttonclicked();
        gameObject.SetActive(false);
    }

    public void Watchvideobuttonclicked()
    {
        Isvideowatched = false;
#if Adsetup_On
        if (AdManager.instance)
            AdManager.instance.ShowRewardVideoWithCallback((result) =>
            {
                if (result)
                {

                    Isvideowatched = true;

                }

            });
#endif

    }


}
