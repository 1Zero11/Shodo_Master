using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMainScript : MonoBehaviour {

    public GameObject mainScreen;
    public GameObject menuScreen;
    public GameObject brushScreen;
    public GameObject objectiveScreen;

    public ShopScrollList ShopScrollList;
    public BrushScrollList BrushScrollList;
    public int showedScreen = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScreen(int direction)
    {
      
        if(direction!=2&&direction!=3)
            StartCoroutine(SmoothChange(direction));
       
    }

    public void ButtonPressed(int number)
    {
        if (number == 0)
            StartCoroutine(MouseMove());
        else
            ChangeScreen(number);

    }

    private IEnumerator MouseMove()
    {
        
        Vector2 pos = Input.mousePosition;
        Vector2 newPos = new Vector2();
        while (Input.GetMouseButton(0))
        {
            
            newPos = Input.mousePosition;
            yield return null;
        }
        MainFightingScript.MFS.gameObject.GetComponent<Drawer>().TurnLines(false);

        if (Mathf.Abs(newPos.y - pos.y) > Mathf.Abs(newPos.x - pos.x))
            ChangeScreen(2);
        else if (newPos.x > pos.x)
            ChangeScreen(3);
        else
            ChangeScreen(1);

    }



    private IEnumerator SmoothChange(int direction)
    {
        float speed = 2f;
        Vector3 mainPos = mainScreen.GetComponent<RectTransform>().localPosition;
        Quaternion mainRot = mainScreen.GetComponent<RectTransform>().localRotation;
        if (showedScreen == 0)
        {
            if (direction == 1)
            {
                Vector3 otherPos = objectiveScreen.GetComponent<RectTransform>().localPosition;
                Quaternion otherRot = objectiveScreen.GetComponent<RectTransform>().localRotation;
                float time = 0f;
                while (time <= 1f)
                {
                    time += Time.deltaTime * speed;

                    mainPos.x = -540f *time;
                    mainScreen.GetComponent<RectTransform>().localPosition = mainPos;

                    mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.AngleAxis(-90f, Vector3.up), time);
                    objectiveScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.identity, time);

                    yield return null;

                }
                ShopScrollList.RefreshDisplay();

            }
            else if (direction == 2)
            {
                Vector3 otherPos = brushScreen.GetComponent<RectTransform>().localPosition;
                Quaternion otherRot = brushScreen.GetComponent<RectTransform>().localRotation;
                float time = 0f;
                while (time<=1f)
                {
                    time += Time.deltaTime * speed;

                    

                    mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.AngleAxis(-90f, Vector3.right), time);
                    brushScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.identity, time);

                    yield return null;

                }
                BrushScrollList.RefreshDisplay();
            }
            else if (direction == 3)
            {
                Vector3 otherPos = menuScreen.GetComponent<RectTransform>().localPosition;
                Quaternion otherRot = menuScreen.GetComponent<RectTransform>().localRotation;
                float time = 0f;
                while (time <= 1f)
                {
                    time += Time.deltaTime * speed;

                    mainPos.x = 540f * time;
                    mainScreen.GetComponent<RectTransform>().localPosition = mainPos;

                    mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.AngleAxis(90f, Vector3.up), time);
                    menuScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.identity, time);

                    yield return null;

                }
            }
            showedScreen = direction;
        }else if(showedScreen == 1)
        {
            Vector3 otherPos = objectiveScreen.GetComponent<RectTransform>().localPosition;
            Quaternion otherRot = objectiveScreen.GetComponent<RectTransform>().localRotation;
            float time = 0f;
            while (time <= 1f)
            {
                

                mainPos.x = -540f + 540f * time;
                mainScreen.GetComponent<RectTransform>().localPosition = mainPos;

                mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, time);
                objectiveScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(90f, Vector3.up), time);

                time += Time.deltaTime * speed;

                yield return null;

            }
            mainScreen.GetComponent<RectTransform>().localPosition = new Vector3(0,mainPos.y,mainPos.z);
            mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, 1);
            objectiveScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(90f, Vector3.up), 1);
            showedScreen = 0;
            MainFightingScript.MFS.gameObject.GetComponent<Drawer>().TurnLines(true);

        }
        else if (showedScreen == 2)
        {
            Vector3 otherPos = brushScreen.GetComponent<RectTransform>().localPosition;
            Quaternion otherRot = brushScreen.GetComponent<RectTransform>().localRotation;
            float time = 0f;
            while (time <= 1f)
            {
                


                mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, time);
                brushScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(90f, Vector3.right), time);

                time += Time.deltaTime * speed;

                yield return null;

            }
            mainScreen.GetComponent<RectTransform>().localPosition = new Vector3(0, mainPos.y, mainPos.z);
            mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, 1);
            brushScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(90f, Vector3.right), 1);
            showedScreen = 0;
            MainFightingScript.MFS.gameObject.GetComponent<Drawer>().TurnLines(true);
        }
        else if (showedScreen == 3)
        {
            Vector3 otherPos = menuScreen.GetComponent<RectTransform>().localPosition;
            Quaternion otherRot = menuScreen.GetComponent<RectTransform>().localRotation;
            float time = 0f;
            while (time <= 1f)
            {
                

                mainPos.x = 540f - 540f * time;
                mainScreen.GetComponent<RectTransform>().localPosition = mainPos;

                mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, time);
                menuScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(-90f, Vector3.up), time);

                time += Time.deltaTime * speed;

                yield return null;

            }
            mainScreen.GetComponent<RectTransform>().localPosition = new Vector3(0, mainPos.y, mainPos.z);
            mainScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(mainRot, Quaternion.identity, 1);
            menuScreen.GetComponent<RectTransform>().localRotation = Quaternion.Lerp(otherRot, Quaternion.AngleAxis(-90f, Vector3.up), 1);
            showedScreen = 0;
            MainFightingScript.MFS.gameObject.GetComponent<Drawer>().TurnLines(true);
        }
    }
}
