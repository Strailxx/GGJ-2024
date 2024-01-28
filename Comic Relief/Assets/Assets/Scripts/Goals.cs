using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public GameObject MainOBJ;

    public GameObject SecondOBJ;

    public GameObject HiddenOBJ;

    public GameObject Winner;
    
    bool maingoal = false;
    bool secondgoal = false;
    bool hiddengoal = false;

    private bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        MainOBJ.SetActive(false);
        SecondOBJ.SetActive(false);
        HiddenOBJ.SetActive(false);
        Winner.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (maingoal)
        {
            MainOBJ.SetActive(true);
        }
        if (secondgoal)
        {
            SecondOBJ.SetActive(true);
        }
        if (hiddengoal)
        {
            HiddenOBJ.SetActive(true);
        }

        if (hiddengoal && secondgoal && hiddengoal)
        {
            Winner.SetActive(true);
        }
    }
}
