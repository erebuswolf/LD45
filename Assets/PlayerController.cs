﻿using System.Collections;
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
    AnimationCurve ArmCurve;

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
        cameraMovement.MoveToWalking();
    }

    public void TakeSelfie()
    {
        if (!InSelfieMode || CameraBusy)
        {
            return;
        }
        // Start selfie animation.
        cameraMovement.TakeSelfie();
        CameraBusy = true;
    }

    IEnumerator ZeroArmRoutine()
    {
        float totalTime = .2f;
        float startTime = Time.time;

        float t = ((Time.time - startTime) / totalTime);
        while (t < 1)
        {
            
            t = ((Time.time - startTime) / totalTime);

            armAngle *= ArmCurve.Evaluate(t);

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void HandleMovement(Vector3 Movement, Vector2 look)
    {
        if(CameraBusy)
        {

            armAngle = Mathf.Clamp(armAngle, -60, 60);
            ArmObject.transform.localRotation = Quaternion.Euler(new Vector3(armAngle, 0, 0));
            return;
        }
        if (InSelfieMode)
        {
            // From camera look controls.

            this.transform.Rotate(new Vector3(0, look.x, 0), Space.World);


            armAngle += look.y;
            armAngle = Mathf.Clamp(armAngle, -60, 60);
            ArmObject.transform.localRotation = Quaternion.Euler(new Vector3(armAngle, 0, 0));
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
