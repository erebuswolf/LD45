using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BouncerTalking : MonoBehaviour
{

    [SerializeField]
    List<AudioClip> rejectedClips;

    [SerializeField]
    AudioClip WinClip;

    [SerializeField]
    AudioSource soundPlayer;


    [SerializeField]
    int victoryMin = 1000;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (!soundPlayer.isPlaying)
            {
                if (playerController.GetFollowers() > victoryMin)
                {
                    soundPlayer.clip = WinClip;
                    soundPlayer.Play();
                    StartCoroutine(WinRoutine());
                }else {
                    soundPlayer.clip = rejectedClips[Random.Range(0, rejectedClips.Count)];
                    soundPlayer.Play();
                }
            }
        }
    }

    IEnumerator WinRoutine()
    {
        while (soundPlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene("WinScene");
    }

}
