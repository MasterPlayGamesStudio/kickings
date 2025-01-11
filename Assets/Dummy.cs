using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Maincam;
    public List<GameObject> Allobj;
    int countvalue =0;

    void Start()
    {
        
    }

    Vector3 startpos, Dragpos;
    public GameObject Dummyobj;
    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    { 
        

        if(Input.GetMouseButtonDown(0))
        {
            ray = Maincam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,100))
            {
                Vector3 pos = hit.point;
                



                startpos = pos;

                GameObject dummy = (GameObject)Instantiate(Dummyobj, pos, Quaternion.identity);
                Allobj.Add(dummy);
            }
           

        }
        if(Input.GetMouseButton(0))
        {

            ray = Maincam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {

                Vector3 pos = hit.point;         
            

                Dragpos = pos;
                diffnew = Allobj[Allobj.Count - 1].transform.position - Dragpos;
                diffnew.Normalize();
                if (lastpos != Dragpos)
                {

                    GameObject dummy = (GameObject)Instantiate(Dummyobj, pos, Quaternion.identity);
                    Allobj.Add(dummy);
                    countvalue = Allobj.Count - 1;

                    Vector3 temp = Allobj[countvalue - 1].transform.position - Dragpos;

                    temp.Normalize();
                    dummy.transform.rotation = Quaternion.LookRotation(Vector3.up, temp * 0.1f);
                    lastpos = pos;
                }

            }
            //euleragles.z = Xvalue * 90;
            //transform.eulerAngles = euleragles;

           // transform.LookAt(Allobj[countvalue].transform.position+ Allobj[countvalue].transform.up);
        }
        
    }

    Vector3 lastpos;

    Vector3 diffnew;

    public float diff;
}
