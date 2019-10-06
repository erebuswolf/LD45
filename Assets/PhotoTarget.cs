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
        if (Physics.Raycast(camera.transform.position, this.transform.position - camera.transform.position, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            if (hitinfo.collider.gameObject == this.gameObject)
            {
            } else
            {
                return false;
            }
        }
        if (minDistance > 0 && (this.transform.position - camera.transform.position).sqrMagnitude > minDistance * minDistance)
        {
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
        commentController.ShowComments(Comments, CommentReads, followers, followersGained + followers);

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
