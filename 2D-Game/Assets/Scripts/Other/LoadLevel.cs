using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int iLeveltoLoad;
    public string sLeveltoLoad;
    public Key key;
    public bool useInttoLoadLevel = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKey("o") && key.havekey == true)
        {
            LoadScene();
        }
    }
    void LoadScene()
    {
        if (useInttoLoadLevel)
        {
            SceneManager.LoadScene(iLeveltoLoad);
        }
        else
        {
            SceneManager.LoadScene(sLeveltoLoad);
        }
    }
}
