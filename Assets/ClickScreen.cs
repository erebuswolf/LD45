using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickScreen : MonoBehaviour
{
  

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("EnterCameraMode") !=0)
        {
            this.GetComponent<Animator>().SetTrigger("Flash");
            this.GetComponent<AudioSource>().Play();

        }
    }
    public void ShowSelfie()
    {
        SceneManager.LoadScene("GameScene");
    }
}
