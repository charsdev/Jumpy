using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCredits : MonoBehaviour
{
    public Animator PanelAnimator;
    public GameObject CreditsPanel;

    private void OnEnable()
    {
        FadeEffect.Instance.FadeIn(1f);
    }

    private void OnDisable()
    {
        FadeEffect.Instance.FadeOut(1f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (PanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            FadeEffect.Instance.FadeOut(1f);
            CreditsPanel.SetActive(false);
        }

        if (Input.anyKeyDown && CreditsPanel.activeInHierarchy)
        {
            CreditsPanel.SetActive(false);
        }

    }
}
