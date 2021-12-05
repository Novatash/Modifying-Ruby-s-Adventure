using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    public RubyController Pop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void go1()
    {
        Pop.go11();
        SceneManager.LoadScene("Main 2");
    }

    public void go2()
    {
        Pop.go22();
        SceneManager.LoadScene("Main 2");
    }
}
