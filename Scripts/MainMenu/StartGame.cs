using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current?.allControls.Any(c => c.IsPressed()) == true)
        {
            SceneManager.LoadScene("Debug");
        }
    }
}
