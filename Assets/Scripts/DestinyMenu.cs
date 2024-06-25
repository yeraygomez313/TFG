using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyMenu : MonoBehaviour
{
    [SerializeField] private GameObject destinyMenu;

    public void Sit()
    {
        print("me siento");

    }

    public void NotSit()
    {
        print("no me siento");
        Level3Manager.badEnding = true;
        destinyMenu.SetActive(false);
    }
}
