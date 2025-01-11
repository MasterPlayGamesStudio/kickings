using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player Data File")]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class PlayerInfo
    {
      

        public int PlayerInitialhealthvalue;
        public int PlayerInitialpowervalue;
 

    }

    public PlayerInfo[] Allplayersinfo;

}
