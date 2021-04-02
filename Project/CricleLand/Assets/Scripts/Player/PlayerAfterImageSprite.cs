using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private Transform playerTransform;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    private Color color;

    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMultipler = 0.85f;

    public GameObject player;


    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        try
        {
            playerTransform = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.transform;
            playerSR = playerTransform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
            alpha = alphaSet;
            SR.sprite = playerSR.sprite;
            transform.position = playerTransform.position;
            transform.rotation = playerTransform.rotation;
            timeActivated = Time.time;
        }
        catch (UnassignedReferenceException) { }
        catch (NullReferenceException) { }
    }

    private void Update()
    {
        alpha *= alphaMultipler;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if(Time.time > timeActivated + activeTime)
        {
            PlayerAfterImagePool.Instace.addToPool(gameObject);
        }
    }
}
