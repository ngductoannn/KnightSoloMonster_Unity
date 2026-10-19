using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text myText1;
    public Text myText2;
    float time = 0;
    
    // Start is calmyTled before the first frame update
    void Start()
    {
        myText1.text = "I'm Toan";
        myText1.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myText1.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myText1.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myText1.fontSize +=1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myText1.fontSize -=1;
        }

        myText2.text = "Time: " + Time.time.ToString("F1");
        time+=Time.deltaTime;
        if(time >= 5)
        {
            myText2.color = Color.red;
        }

    }

    public void ChangeColor()
    {
        if(myText1.color == Color.blue)
        {
            myText1.color = Color.red;
        }
        else if(myText1.color == Color.red)
        {
            myText1.color = Color.blue;
        }
    }
}
