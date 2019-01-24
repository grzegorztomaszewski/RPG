using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(int index)
    {
        castingBar.fillAmount = 0;

        castingBar.color = spells[index].MyBarColor;

        spellName.text = spells[index].MyName;

        icon.sprite = spells[index].MyIcon;

        spellRoutine = StartCoroutine(Progress(index));    //metoda do animacji castbar'a

        fadeRoutine = StartCoroutine(FadeBar());

        return spells[index];
    }

    private IEnumerator Progress(int index) //metoda do animacji castbar'a(czas leci w dół)
    {
        float timePassed = Time.deltaTime; //start time

        float rate = 1.0f / spells[index].MyCastTime;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if(spells[index].MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }
        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float timeleft = Time.deltaTime; //start time

        float rate = 1.0f / 0.25f; //szybkość powrotu paska skilli po nie udanym użyciu skilla

        float progress = 0.0f;

        while (progress <= 1.0)
        {
           canvasGroup.alpha = Mathf.Lerp(0, 1, progress); //zmienia przezroczystość paska skilla

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }

            if (spellRoutine != null)
            {
                StopCoroutine(spellRoutine);
                spellRoutine = null;
            }
        }

}
