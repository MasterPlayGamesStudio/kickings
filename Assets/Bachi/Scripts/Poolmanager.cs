using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolmanager : MonoBehaviour {

	// Use this for initialization



	[System.Serializable]
	public class TypeofBullets
	{
	
		public GameObject Objecttoinstantiate;
		public int Maximumvalue = 10;
        [HideInInspector]
		public List<GameObject> Objectpool;
	}

	public TypeofBullets[] Allpoolobjects;


	public static Poolmanager Obj;


	void Awake () 
	{
		Obj = this;
	}

	// 0 Riflebullets 	20
	//1 shotgn        	20
	//2 smg				20
	//3 sniper			20
	//4 dualbullet		20
	//5 dulabullet2		20
	//6 Laserbullet		20
	//7 sixbullet		20
	//8 Machinebullet 	20
	//9 granade1        10
	//10 granade2  		10
	//11 Missile 		10


	//12 muzzleeffect	20		
	//13 hiteffect		10
	//14 bothiteffect	10
	//15 Explosion1		10
	//16 Explosion2		10
	//17 MissileMissefect 10
	//18 Grenade_mobile
	//19 Rocket_mobile


	public GameObject GetPoolobject(int indexvalue=0)
	{
		if (Allpoolobjects[indexvalue].Objectpool.Count < Allpoolobjects[indexvalue].Maximumvalue) 
		{
			GameObject obj = (GameObject)Instantiate (Allpoolobjects[indexvalue].Objecttoinstantiate);
			Allpoolobjects[indexvalue].Objectpool.Add (obj);
            obj.transform.parent = null;
			return obj;
		} 
		else 
		{
			for (int i = 0; i < Allpoolobjects[indexvalue].Objectpool.Count; i++) 
			{
				if (Allpoolobjects[indexvalue].Objectpool [i].activeSelf == false) 
				{
					Allpoolobjects[indexvalue].Objectpool [i].SetActive (true);
                    Allpoolobjects[indexvalue].Objectpool[i].transform.parent = null;

                    return Allpoolobjects[indexvalue].Objectpool [i];
					break;
				}
			}
		}
		return null;
	}

}

/*
public IEnumerator OpenMenuScene(float waitTime, bool IsFromXml = false)
{
    Debug.LogError("--------- Open Menu Scene");
    yield return new WaitForSeconds(waitTime);
    Debug.LogError("--------- Open Menu Scene 22 is menu loaded=" + IsMenuadloaded);
    if (IsMenuLoaded)
    {
        Debug.LogError("--------- Open Menu Scene menu loaded break");
        yield break;
    }
    Debug.LogError("-------- CallToOpenMenuScene IsFromXml=" + IsFromXml);
  
    CancelInvoke("OpenMenuScene");
 

    if (Loading.GetComponent<LoadingPage>())
    {
        LoadingPage.Levelname = "Theme" + Database.Getthemenumber;
        Loading.GetComponent<LoadingPage>().Gotolevel();
    }
   
    IsMenuLoaded = true;
}
*/


