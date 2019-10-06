using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShowImage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(Texture2D Image)
    {
        spriteRenderer.sprite = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2());
    }
}
