using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool InSelfieMode;
    bool CameraBusy;

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    CameraMovement cameraMovement;


    [SerializeField]
    GameObject ArmObject;

    [SerializeField]
    GameObject ShoulderObject;

    [SerializeField]
    AnimationCurve ArmCurve;


    [SerializeField]
    AnimationCurve TurnCurve;

    [SerializeField]
    GameObject BodyRoot;


    [SerializeField]
    CameraCapture cameraCapture;

    [SerializeField]
    SpriteShowImage spriteShowImage;

    [SerializeField]
    Camera myCamera;


    float camAngle = 0;
    float armAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterSelfieMode()
    {
        if(InSelfieMode || CameraBusy)
        {
            return;
        }
        //Start enter selfie mode animation
        CameraBusy = true;
        StartCoroutine(TurnToCameraRoutine());
        cameraMovement.MoveToSelfie();
    }

    public void ExitSelfieMode()
    {
        if (!InSelfieMode || CameraBusy)
        {
            return;
        }

        //Start exit selfie mode animation
        CameraBusy = true;
        StartCoroutine(ZeroArmRoutine());
        StartCoroutine(TurnFromCameraRoutine());
        cameraMovement.MoveToWalking();
    }

    public void FindObjectsInShot()
    {
        var objects = FindObjectsOfType<PhotoTarget>();
        foreach(PhotoTarget pt in objects){
            if (pt.WasInShot(myCamera)) {
                pt.OnShotReaction();
            }
        }
    }

    public void TakeSelfie()
    {
        if (!InSelfieMode || CameraBusy)
        {
            return;
        }
        // Start selfie animation.
        cameraMovement.TakeSelfie();
        StartCoroutine(SelfieRoutine());
    
        CameraBusy = true;
    }

    IEnumerator SelfieRoutine()
    {
        yield return new WaitForSeconds(.5f);
        var image = cameraCapture.CaptureImage();
        yield return new WaitForEndOfFrame();
        spriteShowImage.SetImage(image);

        yield return new WaitForEndOfFrame();
        FindObjectsInShot();

        yield return new WaitForEndOfFrame();
    }

    IEnumerator ZeroArmRoutine()
    {
        float totalTime = .2f;
        float startTime = Time.time;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {
            
            t = ((Time.time - startTime) / totalTime);

            camAngle *= ArmCurve.Evaluate(t);

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    IEnumerator TurnToCameraRoutine()
    {
        float totalTime = 1f;
        float startTime = Time.time;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {

            t = ((Time.time - startTime) / totalTime);
            BodyRoot.transform.localRotation = Quaternion.Euler(0, TurnCurve.Evaluate(t) * -180, 0);

            armAngle = Mathf.Lerp(0, -90, TurnCurve.Evaluate(t));
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }


    IEnumerator TurnFromCameraRoutine()
    {
        float totalTime = 1f;
        float startTime = Time.time;

        float armStart = armAngle;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {

            t = ((Time.time - startTime) / totalTime);
            BodyRoot.transform.localRotation = Quaternion.Euler(0, TurnCurve.Evaluate(1-t) * -180, 0);

            armAngle = Mathf.Lerp(armStart , 0, TurnCurve.Evaluate(t));
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }


    public void HandleMovement(Vector3 Movement, Vector2 look)
    {
        if(CameraBusy)
        {

            camAngle = Mathf.Clamp(camAngle, -60, 60);
            ArmObject.transform.localRotation = Quaternion.Euler(new Vector3(camAngle, 0, 0));
            ShoulderObject.transform.localRotation = Quaternion.Euler(new Vector3(armAngle, 0, 0));
            return;
        }
        if (InSelfieMode)
        {
            // From camera look controls.

            this.transform.Rotate(new Vector3(0, look.x, 0), Space.World);

            armAngle = -camAngle - 90;
            camAngle += look.y;
            camAngle = Mathf.Clamp(camAngle, -60, 60);
            ArmObject.transform.localRotation = Quaternion.Euler(new Vector3(camAngle, 0, 0));
            ShoulderObject.transform.localRotation = Quaternion.Euler(new Vector3(-camAngle - 90, 0, 0));
            //this.transform.Rotate(Quaternion.FromToRotation(Vector3.forward, this.transform.forward) * new Vector3(look.y, 0, 0), Space.World);

            return;
        }

        // None Selfie Mode Logic.

        Vector3 vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        vel = Quaternion.FromToRotation(Vector3.forward, this.transform.forward) * vel;

        this.transform.Rotate(new Vector3(0, look.x, 0));

        controller.Move(Movement);
    }

    public void AnimationFinished(bool EndedInSelfieMode)
    {
        CameraBusy = false;
        InSelfieMode = EndedInSelfieMode;
    }
}
