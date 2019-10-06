using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColors : MonoBehaviour
{
    [SerializeField]
    Material baseMat;

    [SerializeField]
    List<MeshRenderer> TopObjectsToColor;

    [SerializeField]
    List<MeshRenderer> BottomObjectsToColor;

    // Start is called before the first frame update
    void Start()
    {
        var mat = new Material(baseMat);
        mat.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        foreach (MeshRenderer m in TopObjectsToColor)
        {
            m.material = mat;
        }
        mat = new Material(baseMat);
        mat.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        foreach (MeshRenderer m in BottomObjectsToColor)
        {
            m.material = mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
