using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private AudioClip badEndingSong;
    [SerializeField] private AudioSource music;

    private bool playOnce = true;

    public static bool badEnding = false;


    // Update is called once per frame
    void Update()
    {
        if (badEnding)
        {
            music.clip = badEndingSong;
            music.Play();
            badEnding = false;
            foreach (GameObject enemie in enemies)
                enemie.SetActive(true);
        }
    }
}
