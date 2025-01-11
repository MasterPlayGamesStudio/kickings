using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Data Base")]
public class Leveldata : ScriptableObject
{



    [System.Serializable]
    public class Levelinfo
    {

        
        public enum AIlevel
        { Easy, Medium, Difficult }
       

        [Range(1,102)]
        public int Characterindexvalue;
        [Space(15)]
        public int AIplayerhealth;   
        [Space(15)]
        public int Levelrewardvalue;
        [Space(15)]
        public AIlevel CurrentAIplayerLevel;

    }

    public Levelinfo[] Alllevesinfos;

}
