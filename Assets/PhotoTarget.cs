using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTarget : MonoBehaviour
{
    [SerializeField]
    Collider myCollider;

    [SerializeField]
    float minDistance;

    bool visible = false;

    public bool WasInShot(Camera camera)
    {
        if (!visible)
        {
            return false;
        }

        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        RaycastHit hitinfo = new RaycastHit();
        Debug.DrawRay(camera.transform.position, this.transform.position - camera.transform.position);
        if (Physics.Raycast(camera.transform.position, this.transform.position - camera.transform.position,  out hitinfo, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            if (hitinfo.collider.gameObject == this.gameObject)
            {
            } else
            {
                Debug.LogWarning("shot blocked!!!");
                return false;
            }
        }
        if (minDistance > 0 && (this.transform.position - camera.transform.position).sqrMagnitude > minDistance*minDistance)
        {
            return false;
        }
        return true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
    private void OnBecameVisible()
    {

        visible = true;
    }

    public void OnShotReaction()
    {
        Debug.LogWarning("I was in the shot!!!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
