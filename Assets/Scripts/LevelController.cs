using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public MusicController musicController2;

    public RubyController l_RubyController;

    int numFix = 0;
    int OneDone = 2;

    public Text countText;
    public Text winText;
    public Text winText2;

    public int WickenLiner;

    public BarrierController barrierController;
    public BarrierController2 barrierController2;
    public BarrierController3 barrierController3;
    public BarrierController4 barrierController4;

    // Start is called before the first frame update
    void Start()
    {
        WickenLiner = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (WickenLiner == 3)
            {
                Debug.Log("Reset");
                SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

     }

    public void RobFix()
    {
        numFix = numFix + 1;;

        countText.text = "Robots Fixed: " + numFix.ToString();

        if (numFix == 6)
        {
            if (OneDone != 3)
            {
                OneDone = 3;
                winText.text = "Talk to Jambi to visit stage two!";
            }
        }

        if (numFix == 12)
        {
            musicController2.Win();
            l_RubyController.winner();
            winText2.text = "You win! Press R to return to start!";
        }


        barrierController.RobFix2();
        barrierController2.RobFix2();
        barrierController3.RobFix2();
        barrierController4.RobFix2();
    }

    public void talk()
    {
        if (OneDone == 3)
        {
            SceneManager.LoadScene("Main 1");
        }
    }

    public void loser()
    {
        WickenLiner = 3;
    }
}