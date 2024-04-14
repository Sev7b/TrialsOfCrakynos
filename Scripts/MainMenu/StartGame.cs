using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem; // Make sure to include this for the new Input System
using UnityEngine.SceneManagement; // Include this namespace to work with scene management

public class StartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if any key or button is pressed
        if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current?.allControls.Any(c => c.IsPressed()) == true)
        {
            // Switch to the "Debug" scene
            SceneManager.LoadScene("Debug");
        }
    }
}
