using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicControl : MonoBehaviour
{
    [SerializeField] private Animator animPlayer = null;
    public bool inMenu = true;

    private void Update()
    {
        if (inMenu == false)
        {
            inMenu = true;
            StartCoroutine(animTransition());
        }
    }
    private IEnumerator animTransition()
    {
        animPlayer.Play("menuAnim1");
        float animTime1 = animPlayer.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animTime1 + 0.5f);
        animPlayer.Play("menuAnim2");
        float animTime2 = animPlayer.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animTime2 + 0.5f);
        animPlayer.Play("menuAnim3");
        float animTime3 = animPlayer.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animTime3 + 0.5f);
        animPlayer.Play("menuAnim4");
        float animTime4 = animPlayer.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animTime4 + 0.5f);
    }
}
