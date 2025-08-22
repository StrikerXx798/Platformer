using System;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayEffect()
    {
        this.gameObject.SetActive(true);
    }

    public void StopEffect()
    {
        this.gameObject.SetActive(false);
    }
}