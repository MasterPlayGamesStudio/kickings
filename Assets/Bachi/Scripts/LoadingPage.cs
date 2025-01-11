using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoadingPage : MonoBehaviour
{

    // Use this for initialization

    public AsyncOperation _async;
    public static string Levelname = "";
    public Image _ProgressBarSlider;
    public Text _Loadingnumbertext;
  
    private bool Onlyonce;

   
    // Use this for initialization
    void OnEnable()
    {
        Onlyonce = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
      
    }

    public void Gotolevel()
    {
        if (Onlyonce)
            return;


      

        Onlyonce = true;
        _ProgressBarSlider.fillAmount = 0;
        StartCoroutine(ieRequestToLoadNextScene());
    }
    // Update is called once per frame
    void Update()
    {

        if (_async != null)
        {
            _ProgressBarSlider.fillAmount = _async.progress + 0.1f;
            _Loadingnumbertext.text = "LOADING " + (int)(_ProgressBarSlider.fillAmount * 100)+" %";
         
        }

    }

    public IEnumerator ieRequestToLoadNextScene()
    {

        if (Levelname == "")
            yield break;

        _async = SceneManager.LoadSceneAsync(Levelname);
        yield return _async;
    }

    public void Open ()
	{
		gameObject.SetActive (true);
	}


	public void Close ()
	{
        if (this != null)
        {
            gameObject.SetActive(false);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene changed...");
        Close();
    }

}
