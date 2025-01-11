using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Levelfailscript : MonoBehaviour
{

    public GameObject retrybutton;


    private void OnEnable()
    {
        retrybutton.SetActive(false);

        CancelInvoke("Enablebuttonnow");
        Invoke("Enablebuttonnow", 3);

    }

    void Enablebuttonnow()
    {
        retrybutton.SetActive(true);
    }

    bool Isvideowatched;
    private void Update()
    {
        if(Isvideowatched)
        {
            Isvideowatched = false;

            UIcontrol.Obj.Resetplayer();

        }

    }
    public void GetExtrakickbuttonclicked()
    {
        Isvideowatched=false;
#if ADSETUP_ENABLED
        if(AdManager.instance)
                AdManager.instance.ShowRewardVideoWithCallback((result) =>
                {
                    if (result)
                    {

                        Isvideowatched = true;

                    }

                });
#endif
    }

    public void Retrybuttonclicked()
    {
        UIcontrol.Obj.Retrybuttonclicked();

        #if ADSETUP_ENABLED
        if (AdManager.instance)
        {
            AdManager.instance.RunActions(AdManager.PageType.LF, Database.Levelsnumber);
        }
#endif
    }
    // Start is called before the first frame update

}
