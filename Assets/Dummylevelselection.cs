using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dummylevelselection : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Loadingset;
    void Start()
    {
        Loadingset.SetActive(false);
    }

    public void Selectlevel(int number)
    {
      
        Loadingset.SetActive(true);
        SceneManager.LoadScene("Theme"+Database.Getthemenumber);
    }
  
}
