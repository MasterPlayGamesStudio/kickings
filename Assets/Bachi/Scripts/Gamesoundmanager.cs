using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamesoundmanager : MonoBehaviour
{
    private static Gamesoundmanager _instance;

    public static Gamesoundmanager Instance
    {
        get =>_instance;      
        
    }

    

    #region ALL AUDIO SOURCES

            [Header("AUDIO SOURCES")]
            [Space(15)]
            public AudioSource[] Allaudiosources;
            //0 : background source
            //1 : Audience source
            //2 : other source

    #endregion


    #region ALL AUDIO CLIPS

   
            [Header("AUDIO CLIPS")]
            [Space(15)]
            public AudioClip[] Allclips;

    #endregion

    void Awake() => _instance = this;


    private void Start()
    {

        checkbgsoundstatus();

        Allaudiosources[0].loop = true;
        Allaudiosources[0].clip = Allclips[0];
        Allaudiosources[0].Play();
        Allaudiosources[0].spatialBlend = 0;
        PlayIngamebgsound(false);


        Allaudiosources[1].loop = false;
        Allaudiosources[1].clip = Allclips[1];
    



        Allaudiosources[2].loop = false;
        Allaudiosources[2].spatialBlend = 0;




    }

    
    public void PlayIngamebgsound(bool Status=false)
    {
        Allaudiosources[0].loop = true;
        Allaudiosources[0].clip = Allclips[0];
        Allaudiosources[0].Play();
      
    }

    public void Playingameclapsound(bool Status=false)
    {
        if (Allaudiosources[1].isPlaying || Database.GetMusictatus!="On")
            return;

        Allaudiosources[1].mute = Status;
        Allaudiosources[1].loop = false;
        Allaudiosources[1].clip = Allclips[Random.Range(1,3)];
        Allaudiosources[1].Play();
        Allaudiosources[1].spatialBlend = 0;
    }

    public void Playwinclapsound()
    {
        if (Allaudiosources[1].isPlaying || Database.GetMusictatus != "On")
            return;

        Allaudiosources[1].loop = false;
        Allaudiosources[1].clip = Allclips[3];
        Allaudiosources[1].Play();
        Allaudiosources[1].spatialBlend = 0;
    }

    public void Playlevelfailsound()
    {
       
        Allaudiosources[0].Stop();


        Allaudiosources[2].loop = false;
        Allaudiosources[2].clip = Allclips[7];
        Allaudiosources[2].Play();

    }

    public void Playlevelwinsound()
    {
        Allaudiosources[0].Stop();


        Allaudiosources[2].loop = true;
        Allaudiosources[2].clip = Allclips[6];
        Allaudiosources[2].Play();

    }

    public void Playlevelupgradesound()
    {
       
        Allaudiosources[2].loop = false;
        Allaudiosources[2].clip = Allclips[5];
        Allaudiosources[2].Play();

    }

    public void Playlevelpowersound()
    {

        Allaudiosources[2].loop = false;
        Allaudiosources[2].clip = Allclips[4];
        Allaudiosources[2].Play();

    }


    public void PlayButtonsound()
    {

        if (Database.GetSoundstatus != "On")
        {
            return;
        }

        Allaudiosources[3].loop = false;
        Allaudiosources[3].clip = Allclips[8];
        Allaudiosources[3].Play();

    }

    #region AUDIO BACKGROUND MUTE STATUS

    public void Allbgsoundsstatus(bool Status)
    {
        for (int i = 0; i < Allaudiosources.Length-1; i++)

        {
            Allaudiosources[i].mute = Status;
        }
    }


    public void checkbgsoundstatus()
    {
        if (Database.GetMusictatus == "On")
        {
            Allbgsoundsstatus(false);
        }
        else
        {
            Allbgsoundsstatus(true);

        }

    }
    #endregion
}
