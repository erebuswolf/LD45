using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentController : MonoBehaviour
{
    [SerializeField]
    List<Text> textFields;

    [SerializeField]
    Text followersText;

    [SerializeField]
    AudioSource AudioPlayer;


    [SerializeField]
    Animator photoFadeAnimator;


    [SerializeField]
    List<string> duplicateSubject;


    [SerializeField]
    List<string> noSubject;

    [SerializeField]
    List<AudioClip> duplicateSubjectAudio;

    [SerializeField]
    List<AudioClip> noSubjectAudio;

    [SerializeField]
    PlayerController playerController;


    [SerializeField]
    AudioClip FollowerIncrease;
    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowComments(List<string> comments, List<AudioClip> audio, int currentFollowers, int NewFollowers)
    {
        StartCoroutine(CommentRoutine(comments, audio, currentFollowers, NewFollowers));
    }


    public void PlayEmptyImageReaction()
    {
        int randSelect = Random.Range(0, noSubject.Count);

        StartCoroutine(BadCommentRoutine(noSubject[randSelect], noSubjectAudio[randSelect]));
    }

    public void PlayDuplicateImageReaction()
    {
        int randSelect = Random.Range(0, duplicateSubject.Count);

        StartCoroutine(BadCommentRoutine(duplicateSubject[randSelect], duplicateSubjectAudio[randSelect] ));
    }

    IEnumerator BadCommentRoutine(string comments, AudioClip audio)
    {
        yield return new WaitForSeconds(1);
        textFields[0].text = comments;
        textFields[0].gameObject.SetActive(true);
        AudioPlayer.clip = audio;
        AudioPlayer.Play();
        yield return new WaitForSeconds(audio.length + .5f);
        
        foreach (Text t in textFields)
        {
            t.gameObject.SetActive(false);
        }

        photoFadeAnimator.SetTrigger("FadeOut");

        playerController.AnimationFinished(true);
        playerController.ExitSelfieMode();
        yield break;
    }
    IEnumerator CommentRoutine(List<string> comments, List<AudioClip> audio, int currentFollowers, int NewFollowers)
    {
        yield return new WaitForSeconds(1);
        for(int i = 0; i < comments.Count; i++)
        {
            textFields[i].text = comments[i];
            textFields[i].gameObject.SetActive(true);
            AudioPlayer.clip = audio[i];
            AudioPlayer.Play();
            yield return new WaitForSeconds(audio[i].length +.5f);
        }

        int followerstep = (NewFollowers - currentFollowers) / 60;
        if (followerstep == 0)
        {
            followerstep = 1;
        }
        AudioPlayer.clip = FollowerIncrease;
        AudioPlayer.Play();
        while ( currentFollowers < NewFollowers)
        {
            currentFollowers+= followerstep;
            followersText.text = currentFollowers + "";
            yield return new WaitForEndOfFrame();
        }
        currentFollowers = NewFollowers;

        followersText.text = currentFollowers + "";

        foreach (Text t in textFields)
        {
            t.gameObject.SetActive(false);
        }

        photoFadeAnimator.SetTrigger("FadeOut");

        playerController.AnimationFinished(true);
        playerController.ExitSelfieMode();
        yield break;
    }

}
