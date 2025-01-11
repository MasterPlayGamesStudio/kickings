using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Audiencecontroller : MonoBehaviour
{
    // Start is called before the first frame update

    public static List<Changecharanimations> Totalaudience;
    public Transform[] Charactersinstantionpositions;


    private Gamesoundmanager Bgsoundmanager;
    private void Awake()
    {

        Totalaudience = new List<Changecharanimations>();
        Totalaudience.Clear();
    }

    private void Start()
    {
        for(int i=0;i<Charactersinstantionpositions.Length;i++)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Character" + Random.Range(1, 6)), Charactersinstantionpositions[i].transform.position, Quaternion.identity);
            obj.transform.LookAt(this.transform);
        }

        if(Gamesoundmanager.Instance)
        {
            Bgsoundmanager = Gamesoundmanager.Instance;
        }
    }
    float timervalue=0;
    float checktimervalue=2;
    private void LateUpdate()
    {
        if (Gamemanager.Stopchecking || Gamemanager.Inputrestricted)
            return;

        timervalue += Time.deltaTime;
        if(timervalue>checktimervalue)
        {
            Playanimations();
            timervalue = 0;
            checktimervalue = Random.Range(5, 8);
        }
    }

    public void  Playanimations()
    {
        for(int i=0;i<Totalaudience.Count;i++)
        {
            Totalaudience[i].Playanimation();

        }

        Bgsoundmanager.Playingameclapsound();
    }

    public static void Addtocontroller(Changecharanimations audienceref)=>Totalaudience.Add(audienceref);
    

    public static void Removefromcontroller(Changecharanimations audienceref)=> Totalaudience.Remove(audienceref);

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        for(int i=0;i<Charactersinstantionpositions.Length;i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Charactersinstantionpositions[i].position,1);
        }
#endif
    }
}
