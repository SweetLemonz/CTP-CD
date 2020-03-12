using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoButt : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}

