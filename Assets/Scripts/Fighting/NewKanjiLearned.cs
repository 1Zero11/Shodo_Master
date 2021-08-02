using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewKanjiLearned : MonoBehaviour {

    public Text NKL;
    public Text meaning;
    public Text kanji;
    private Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();

        NKL.color = new Color(0f, 0f, 0f, 0f);
        meaning.color = new Color(0f, 0f, 0f, 0f);
        kanji.color = new Color(0f, 0f, 0f, 0f);
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    public void Learned(string meaningN, string kanjiN)
    {
        meaning.text = meaningN;
        kanji.text = kanjiN;
        StartCoroutine(AppearAndDissappear());
    }


    private IEnumerator AppearAndDissappear()
    {
        float alpha = 0f;
        while (true)
        {
            NKL.color = new Color(0f, 0f, 0f, alpha);
            meaning.color = new Color(0f, 0f, 0f, alpha);
            kanji.color = new Color(0f, 0f, 0f, alpha);
            img.color = new Color(1f, 1f, 1f, alpha);
            alpha+= 0.01f;
            if (alpha >= 1f)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        while (true)
        {
            NKL.color = new Color(0f, 0f, 0f, alpha);
            meaning.color = new Color(0f, 0f, 0f, alpha);
            kanji.color = new Color(0f, 0f, 0f, alpha);
            img.color = new Color(1f, 1f, 1f, alpha);
            alpha-=0.01f;
            if (alpha <= 0f)
            {
                break;
            }
            yield return null;
        }
    }


}
