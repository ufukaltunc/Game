using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int iLeveltoLoad;
    public string sLeveltoLoad;

    public bool useInttoLoadLevel = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gameObject = other.gameObject;

        if (gameObject.tag == "Player")
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
