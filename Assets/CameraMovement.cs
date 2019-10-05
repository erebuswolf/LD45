using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Vector3 InitialOffset;

    Quaternion InitialRotation;

    Quaternion FinalRotation;

    [SerializeField]
    GameObject selfiePosition;

    [SerializeField]
    GameObject selfieTarget;

    [SerializeField]
    AnimationCurve MovementCurve;

    [SerializeField]
    AnimationCurve RotationCurve;

    [SerializeField]
    PlayerController playerController;

    void Start()
    {
        InitialOffset = this.transform.localPosition;
        InitialRotation = this.transform.localRotation;
        selfiePosition.transform.LookAt(selfieTarget.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToSelfie()
    {
        StartCoroutine(MoveToSelfieRoutine());
    }

    public void MoveToWalking()
    {
        StartCoroutine(MoveToInitRoutine());
    }

    IEnumerator MoveToSelfieRoutine()
    {
        float totalTime = 1f;
        float startTime = Time.time;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {
            t = ((Time.time - startTime) / totalTime);
                
            MoveToSelfiePosition(MovementCurve.Evaluate(t));

            yield return new WaitForEndOfFrame();
        }
        playerController.AnimationFinished(true);
        yield break;
    }

    IEnumerator MoveToInitRoutine()
    {
        float totalTime = 1f;
        float startTime = Time.time;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {
            t = ((Time.time - startTime) / totalTime);

            MoveToSelfiePosition(MovementCurve.Evaluate(1-t));

            yield return new WaitForEndOfFrame();
        }
        playerController.AnimationFinished(false);
        yield break;
    }

    void MoveToSelfiePosition(float t)
    {
        this.transform.localPosition = Vector3.Lerp(InitialOffset, selfiePosition.transform.localPosition, t);

        this.transform.localRotation = Quaternion.Lerp(InitialRotation, selfiePosition.transform.localRotation, t);
            
    }

}
