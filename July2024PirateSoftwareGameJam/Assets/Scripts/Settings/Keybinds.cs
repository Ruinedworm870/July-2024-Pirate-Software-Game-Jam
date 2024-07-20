using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keybinds
{
    private static Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();
    private static Dictionary<string, string> keyNames = new Dictionary<string, string>();

    static Keybinds()
    {
        //Initializing default values

        keybinds["W"] = KeyCode.W;
        keybinds["S"] = KeyCode.S;
        keybinds["A"] = KeyCode.A;
        keybinds["D"] = KeyCode.D;


        keyNames["W"] = "Forward";
        keyNames["S"] = "Backward";
        keyNames["A"] = "Left";
        keyNames["D"] = "Right";
    }

    public static void ChangeKeybind(string key, KeyCode pressed)
    {
        keybinds[key] = pressed;
    }

    public static Dictionary<string, KeyCode> GetKeybinds()
    {
        return keybinds;
    }

    public static string GetKeyActionName(string key)
    {
        return keyNames[key];
    }

    public static KeyCode GetKeyCode(string key)
    {
        return keybinds[key];
    }
}
