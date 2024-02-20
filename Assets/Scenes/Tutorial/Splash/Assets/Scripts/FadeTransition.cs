using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public Animator animator;
    public Image img;
    public LevelLoader loader;

    public void FadeToLoader () {
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete () {
        img.gameObject.SetActive(false);
        loader.LoadLevel(1);
    }
}
