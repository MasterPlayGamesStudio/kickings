using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Maincomponents: MonoBehaviour
{
    public readonly string Triggername = "Cheer";
    public  Animator Playeranim;

    public abstract void Playanimation();
   

}


public class Changecharanimations : Maincomponents
{
    // Start is called before the first frame update

    void OnEnable()
    {
        Audiencecontroller.Addtocontroller(this);
    }

    void OnDisable()
    {
        Audiencecontroller.Removefromcontroller(this);

    }




    public override void Playanimation()
    {
        if(Random.Range(1,100)>50)
            Playeranim.SetTrigger(Triggername + Random.Range(1, 3));
    }
}
