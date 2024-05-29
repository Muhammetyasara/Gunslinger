using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    public ParticleSystem[] openEffect;
    public bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen)
        {
            OpenChest();
            isOpen = false;
        }
    }
    public void OpenChest()
    {
        animator.SetBool("isOpen", true);
        if (openEffect != null)
        {
            Invoke(nameof(OpenEffectPlay), .45f);
        }
    }

    public void CloseChest()
    {
        animator.SetBool("isOpen", false);
    }

    private void OpenEffectPlay()
    {
        foreach (var item in openEffect)
        {
            item.Play();
        }
    }
}