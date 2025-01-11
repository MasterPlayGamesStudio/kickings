using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjfromresources : MonoBehaviour
{
    // Start is called before the first frame update

        public enum Objectname
        {
            Upgradeset, BgsoundManager, Playersoundmanager

         }
        public Objectname Currentobjectname;

        private void Awake()
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load(Currentobjectname.ToString()));
            obj.transform.SetParent(this.transform);
        }
}
