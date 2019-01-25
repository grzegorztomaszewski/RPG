using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content; //tworzenie obiektu wcześniej stworoznego w Unity

    [SerializeField]
    private Text statValue; //tworzenie obiektu text

    [SerializeField]
    private float lerpSpeed; //prędkość animacji paska HP/MANA

    private float currentFill; //zmienna do paska HP

    public float MyMaxValue { get; set; }

    private float currentValue;
    
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if(value > MyMaxValue) //ustawianie bieżącej wartości HP na Maxymalną
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0) //zabezpieczenie przed ujemnym HP
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue; //ustawienie poziomu paska HP(image)
            if (statValue != null)
            {
            statValue.text = currentValue + "/" + MyMaxValue; //wyświetlanie wartości HP/MANA jako tekst
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>(); //referencja do komponentu w Unity
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFill != content.fillAmount)  //płynne zmniejszanie HP/MANNA
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed); // 1) Wartość początkowa 2) Wartość końcowa 3) Płynna animacja paska HP/MANNA
        }
    }

    public void Initialize(float currentValue, float maxValue) //publiczny obiekt, który ustawia 2 wartości pobierane z playera
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
