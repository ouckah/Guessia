using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndHandler : MonoBehaviour
{

    private GameManager gameManager;

    // UI ELEMENTS
    public GameObject dimElement;
    public GameObject popUp;

    private Color32 popUpLerpColor;
    private float popUpLerpTime = 5f;

    // UI COLORS
    private Color32 clear = new Color32(0, 0, 0, 255);
    private Color32 popUpColor = new Color32(27, 25, 25, 255);

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // UI ELEMENTS
        dimElement.SetActive(false);
        popUp.SetActive(false);
    }

    void Update()
    {
        if (gameManager.status == "Win")
        {
            WinHandler();
        }

        else if (gameManager.status == "Lose")
        {
            LoseHandler();
        }

        popUp.GetComponent<Image>().color = Color.Lerp(popUp.GetComponent<Image>().color, popUpLerpColor, popUpLerpTime * Time.deltaTime);
    }

    void WinHandler()
    {
        dimElement.SetActive(true);
        popUp.SetActive(true);

        popUpLerpColor = popUpColor;
    }

    void LoseHandler()
    {

    }

}
