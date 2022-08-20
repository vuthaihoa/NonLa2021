using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundXY : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infinitHorizontal;
    [SerializeField] private bool infinitVertical;
    private Transform CameraTransform;
    private Vector3 LastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    private void Start()
    {
        CameraTransform = Camera.main.transform;
        LastCameraPosition = CameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        //textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        //textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;

    }
    private void FixedUpdate()
    {
        Vector3 deltaMovement = CameraTransform.position - LastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        LastCameraPosition = CameraTransform.position;
        if(infinitHorizontal)
        {
            if (Mathf.Abs(CameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (CameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(CameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }
        if(infinitVertical)
        {
            if (Mathf.Abs(CameraTransform.position.y - transform.position.y) >= textureUnitSizeX)
            {
                float offsetPositionY = (CameraTransform.position.y - transform.position.y) % textureUnitSizeX;
                transform.position = new Vector3(CameraTransform.position.x + offsetPositionY, transform.position.y);
            }
        }
    }
}
