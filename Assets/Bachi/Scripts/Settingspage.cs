using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingspage : MonoBehaviour
{
    // Start is called before the first frame update
    private static Settingspage _instance;
    public static Settingspage Instance
    {
        get;private set;
    }


    [Space(10)]
    public GameObject Soundsonbutton,Soundsoffbutton,Musiconbutton,Musicoffbutton,Vibrateonbutton,Vibrateoffbutton;


    void Awake()
    {
        _instance = this;
        //gameObject.SetActive(false);
    }

    void OnEnable()
    {
        CheckMusicstauts();
        CheckSoundstatus();
        CheckVibratestatus();
        if(Gamemanager.Instance)
        {
            Gamemanager.Instance.Isgamerunning = false;
        }
    }


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Closebuttonfunc()
    {
        gameObject.SetActive(false);

        if (Gamemanager.Instance)
        {
            Gamemanager.Instance.Isgamerunning = true;
        }
    }

    #region MUSIC RELATED
        public void CheckMusicstauts()
        {
            Musicoffbutton.SetActive(false);
            Musiconbutton.SetActive(false);

            if (Database.GetMusictatus=="On")
            {
               Musiconbutton.SetActive(true);
            }
            else
            {
             Musicoffbutton.SetActive(true);

            }

            if(Gamesoundmanager.Instance)
            {
                Gamesoundmanager.Instance.checkbgsoundstatus();
            }
        }

        public void Enablemusic(string status="On")
        {
            Database.GetMusictatus = status;
            CheckMusicstauts();
        }

    #endregion

    #region SOUNDS RELATED

        public void CheckSoundstatus()
        {
            Soundsoffbutton.SetActive(false);
            Soundsonbutton.SetActive(false);

            if (Database.GetSoundstatus == "On")
            {
            Soundsonbutton.SetActive(true);
            }
            else
            {
            Soundsoffbutton.SetActive(true);

            }

            if (Playersoundmanager.Instance)
            {
                Playersoundmanager.Instance.checkbgsoundstatus();
            }
        }

        public void EnableSound(string status = "On")
        {
            Database.GetSoundstatus = status;
            CheckSoundstatus();
        }
    #endregion


    #region VIBRATION RELATED

        public void CheckVibratestatus()
        {
            Vibrateoffbutton.SetActive(false);
            Vibrateonbutton.SetActive(false);

            if (Database.GetSoundstatus == "On")
            {
            Vibrateonbutton.SetActive(true);
            }
            else
            {
            Vibrateoffbutton.SetActive(true);

            }
        }

        public void EnablVibration(string status = "On")
        {
            Database.GetVibratestauts = status;
            CheckVibratestatus();
        }
    #endregion



    #region RATING RELATED

    public void Rateclicked()
    {
#if ADSETUP_ENABLED
        if (AdManager.instance)
        {
            Application.OpenURL("market://details?id=" + Application.identifier);
        }
#endif
    }

    public void Privatepolicyclicked()
    {
#if ADSETUP_ENABLED
        if (AdManager.instance)
        {
            AdManager.instance.Privacypolicybuttonfunction();
        }
#endif
    }

    #endregion
}
