using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundXY : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform CameraTransform;
    private Vector3 LastCameraPosition;
    private float textureUnitSizeX;
    private void Start()
    {
        CameraTransform = Camera.main.transform;
        LastCameraPosition = CameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    private void FixedUpdate()
    {
        Vector3 deltaMovement = CameraTransform.position - LastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        LastCameraPosition = CameraTransform.position;

        if(Mathf.Abs(CameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (CameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(CameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
