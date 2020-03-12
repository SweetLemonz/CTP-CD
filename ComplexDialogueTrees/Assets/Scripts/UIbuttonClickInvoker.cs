using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbuttonClickInvoker : MonoBehaviour
 {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}

