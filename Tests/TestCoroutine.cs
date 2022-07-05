using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Halluvision.Coroutine;

public class TestCoroutine : MonoBehaviour
{
    CoroutineHandle handle1;
    CoroutineHandle handle2;
    bool stop = false;

    void Start()
    {
        handle1 = new CoroutineHandle(Debugger("First"), true);
        handle2 = new CoroutineHandle(Debugger("Second"), false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            handle2.Start();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            stop = true;
        }
    }

    IEnumerator Debugger(string name)
    {
        while (!stop)
        {
            Debug.Log(name);
            yield return new WaitForSeconds(1f);
        }
    }
}
