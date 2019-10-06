using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTarget : MonoBehaviour
{
    [SerializeField]
    Collider myCollider;

    [SerializeField]
    float minDistance;

    [SerializeField]
    bool AlreadyPhotographed;

    [SerializeField]
    int followersGained;

    [SerializeField]
    List<string> Comments;

    [SerializeField]
    List<PhotoTarget> TargetsToActivate;

    [SerializeField]
    List<AudioClip> CommentReads;

    CommentController commentController;

    public bool WasInShot(Camera camera)
    {

        RaycastHit hitinfo = new RaycastHit();
        Debug.DrawRay(camera.transform.position, this.transform.position - camera.transform.position);
        if (Physics.Raycast(camera.transform.position, this.transform.position - camera.transform.position, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            if (hitinfo.collider.gameObject == this.gameObject)
            {
            } else
            {
                Debug.LogWarning("shot blocked!!!");
                return false;
            }
        }
        if (minDistance > 0 && (this.transform.position - camera.transform.position).sqrMagnitude > minDistance * minDistance)
        {
            Debug.LogWarningFormat("outside min distance!!! {0}  {1}", minDistance, (this.transform.position - camera.transform.position).magnitude);
            return false;
        }

        var planes = GeometryUtility.CalculateFrustumPlanes(camera);

        if (!GeometryUtility.TestPlanesAABB(planes, myCollider.bounds)) {
            return false;
        }
        return true;
    }


    public int OnShotReaction(int followers)
    {
        if (AlreadyPhotographed)
        {
            commentController.PlayDuplicateImageReaction();
            return 0;
        }
        Debug.LogWarning("I was in the shot!!!");
        commentController.ShowComments(Comments, CommentReads, followers, followersGained);

        AlreadyPhotographed = true;
        return followersGained;
    }

    // Start is called before the first frame update
    void Start()
    {
        commentController = FindObjectOfType<CommentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
