using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersoundmanager : MonoBehaviour
{
    private static Playersoundmanager _instance;

    public static Playersoundmanager Instance
    {
        get => _instance;
    }



    #region PLAYERS AUDIO SOURCES
        
        [Header("AUDIO SOURCES")]
        [Space(15)]
        public AudioSource[] Allaudiosources;

    #endregion

    #region PLAYERS AUDIO CLIPS
  
        [Header("AUDIO CLIPS")]
        [Space(15)]
        public AudioClip[] Kickactionclips;
        public AudioClip[] KickReactionclips;
        public AudioClip[] Playermoveclips;
        public AudioClip[] Playerfall;
        public AudioClip[] Playerdead;




    public AudioClip[] Malekickreactionvoiceclips;
        public AudioClip[] Femalekickreactionvoiceclips;

    #endregion

    private void Awake() => _instance = this;

    private void Start()
    {
        checkbgsoundstatus();

        for (int i=0;i<Allaudiosources.Length;i++)
        {
            Allaudiosources[i].loop = false;
        }
    }

    #region PLAYER KICK ACTIOS SOUNDS

        public void CheckKickaction(bool Isnormalkick = true)
            {
                if(Isnormalkick)
                {
                    Getanyaudiosource.clip = Kickactionclips[Random.Range(0, 2)];
                    Getanyaudiosource.Play();
                }

                else
                {
                    Getanyaudiosource.clip = Kickactionclips[Random.Range(2, 5)];
                    Getanyaudiosource.Play();
                }
    }

    #endregion
    #region PLAYER MOVE SOUNDS

        public void Playmovesound()
        {
            Getanyaudiosource.clip = Playermoveclips[Random.Range(0, Playermoveclips.Length)];
            Getanyaudiosource.Play();
        }

    #endregion

    #region PLAYER FALL SOUNDS

        public void Playfallsound()
        {
            Getanyaudiosource.clip = Playerfall[Random.Range(0, Playerfall.Length)];
            Getanyaudiosource.Play();
        }

    #endregion

    #region PLAYER Dead SOUNDS

    public void Playerdeadsound()
    {
        Getanyaudiosource.clip = Playerdead[Random.Range(0, Playerdead.Length)];
        Getanyaudiosource.Play();
    }

    #endregion

    #region PLAYER HIT SOUNDS

    public void PlayHitsound(int hitstatus=0)
    {
        if(hitstatus==0)
        {
            Getanyaudiosource.clip = KickReactionclips[Random.Range(5, 7)];

        }
        else if(hitstatus==1)
        {
            Getanyaudiosource.clip = KickReactionclips[Random.Range(3, 5)];

        }
        else
        {
            Getanyaudiosource.clip = KickReactionclips[Random.Range(0, 3)];

        }


        Getanyaudiosource.Play();
    }

    #endregion


    #region PLAYER HURT SOUNDS

    public IEnumerator PlayerHurtsound(Basescript currentplayer)
    {
        yield return new WaitForSeconds(0.25f);

        if(currentplayer.Currentplayertype==Basescript.Playertype.MalePlayer)
        {
            if(currentplayer.Currentplayerphysique==Basescript.Playerphysique.Slim)
            {
                Getanyaudiosource.clip = Malekickreactionvoiceclips[Random.Range(0, 2)];

            }
            else if (currentplayer.Currentplayerphysique == Basescript.Playerphysique.Normal)
            {
                Getanyaudiosource.clip = Malekickreactionvoiceclips[Random.Range(2, 4)];

            }
            else
            {
                Getanyaudiosource.clip = Malekickreactionvoiceclips[Random.Range(4, 6)];
                 
            }
        }
        else
        {

            if (currentplayer.Currentplayerphysique == Basescript.Playerphysique.Slim)
            {
                Getanyaudiosource.clip = Femalekickreactionvoiceclips[Random.Range(0, 2)];

            }
            else if (currentplayer.Currentplayerphysique == Basescript.Playerphysique.Normal)
            {
                Getanyaudiosource.clip = Femalekickreactionvoiceclips[Random.Range(2, 4)];

            }
            else
            {
                Getanyaudiosource.clip = Femalekickreactionvoiceclips[Random.Range(4, 6)];

            }
        }


        Getanyaudiosource.Play();
    }

    #endregion

    #region AUDIO SOURCE MUTE STATUS

    public void Allbgsoundsstatus(bool Status)
    {
        for (int i = 0; i < Allaudiosources.Length; i++)

        {
            Allaudiosources[i].mute = Status;
        }
    }


    public void checkbgsoundstatus()
    {
        if (Database.GetSoundstatus == "On")
        {
            Allbgsoundsstatus(false);
        }
        else
        {
            Allbgsoundsstatus(true);

        }

    }
    #endregion


    #region GET AUDIO SOURCE TO PLAY CLIPS

    AudioSource Getanyaudiosource
        {
            get
            {
                AudioSource temp = Allaudiosources[0];
                for (int i = 0; i < Allaudiosources.Length; i++)
                {
                    if (Allaudiosources[i].isPlaying == false)
                    {

                        temp = Allaudiosources[i];
                        break;
                    }
                }
                return temp;
            }
        }
    #endregion

}
