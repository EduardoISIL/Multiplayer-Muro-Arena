using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicControl : MonoBehaviour
{
    [SerializeField] private Animator[] animPlayer = null;
    public bool inMenu = true;
    private float temp = 0;

    private string[] nameAnim = null;
    private float[] animTime = null;
    private void Start()
    {
        nameAnim = new string[] { "menuAnim1", "menuAnim2", "menuAnim3", "menuAnim4" };

        for (int i = 0; i < animPlayer.Length; i++)
        {
            animPlayer[i].speed = 0;
        }
    }

    private void Update()
    {
        temp += Time.deltaTime;

        if (inMenu == false)
        {
            for (int j = 0; j < animPlayer.Length; j++)
            {
                animPlayer[j].speed = 1;
            }
        }

        if (temp > 4 && inMenu == false)
        {
            temp = 0;
            StartCoroutine(AnimTransition());
        }

    }
    private IEnumerator AnimTransition()
    {
        for (int k = 0; k < animPlayer.Length; k++)
        {
            animPlayer[k].Play(nameAnim[Random.Range(0, 3)]);
            animTime = new float[] {animPlayer[k].GetCurrentAnimatorStateInfo(0).length };
        }

        for (int l = 0; l < animTime.Length; l++)
        {
            yield return new WaitForSeconds(animTime[l]);
        }
    }
}
