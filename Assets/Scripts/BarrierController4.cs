using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController4 : MonoBehaviour
{
    int numFix2;

    // Start is called before the first frame update
    void Start()
    {
        numFix2 = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RobFix2()
    {
        if (numFix2 == 11)
        {
            Destroy(gameObject);
        }

        numFix2 = (numFix2 + 1);
    }
}
