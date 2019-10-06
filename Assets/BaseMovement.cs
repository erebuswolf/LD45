using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField]
    float WanderRange;

    Vector3 startLocation;


    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float vel;


    [SerializeField]
    Vector3 RandomGoal;

    float lastGoalTime;

    [SerializeField]
    float ChangeGoalTime;

    [SerializeField]
    float goalEps;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = this.transform.position;
        PickGoal();
    }

    void PickGoal()
    {
        RandomGoal = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * WanderRange + startLocation;
        lastGoalTime = Time.time;
        
        this.transform.rotation= Quaternion.LookRotation(new Vector3((RandomGoal- this.transform.position).x, 0, (RandomGoal - this.transform.position).z));
    }

    // Update is called once per frame
    void Update()
    {
        WanderAroundStart();

        characterController.SimpleMove(new Vector3(0, -1, 0));
    }

    void WanderAroundStart()
    {

        // Pick a random goal location
        if (lastGoalTime + ChangeGoalTime < Time.time || (this.transform.position - RandomGoal).sqrMagnitude < goalEps*goalEps)
        {
            PickGoal();
        }

        // turn towards that location


        // start walking that way.
        var newvel = (RandomGoal - this.transform.position).normalized * vel;
        characterController.Move(newvel);


    }
}
