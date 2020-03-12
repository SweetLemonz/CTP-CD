using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesButt : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
