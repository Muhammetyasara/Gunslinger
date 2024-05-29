using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTexts : MonoBehaviour
{
    public SpriteRenderer sprite;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GamePlayManager.instance.GoldRainWall(transform.position));
        GetComponent<SpriteRenderer>().sprite = null;
        sprite.sprite = null;
        GetComponent<Collider>().enabled = false;
    }
}
