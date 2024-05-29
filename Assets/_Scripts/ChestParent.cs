using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestParent : MonoBehaviour
{
    public Chest[] chests;
    Chest activeChest;

    public ParticleSystem chestAppearVFX;

    public MoneyRushChest moneyRushChest;

    public int movementSpeed;
    public bool isOpening;
    public float pingPongLength;
    private void Update()
    {
        if (!isOpening && GamePlayManager.instance.isPlayerFinished)
        {
            ChestParentMovement();
        }
    }

    private void ChestParentMovement()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * movementSpeed, pingPongLength), 0, 0);
    }

    public Chest GetActiveChest()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].isActiveAndEnabled)
            {
                activeChest = chests[i];
                break;
            }
        }
        return activeChest;
    }

    public void ChestReveal(int value)
    {
        foreach (var item in chests)
        {
            item.gameObject.SetActive(false);
        }
        chests[value].gameObject.SetActive(true);
        chestAppearVFX.gameObject.SetActive(true);
    }
}