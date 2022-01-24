using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    SCENE KEY
    ---------

    0 - Main Menu
    1 - Game

*/

public class SceneManager : MonoBehaviour
{
    public Canvas[] scenes;

    void Start()
    {
        Canvas mainMenu = scenes[0];
        Canvas game = scenes[1];

        mainMenu.gameObject.SetActive(true);
        game.gameObject.SetActive(false);
    }
}
