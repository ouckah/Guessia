using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public string letter;
    public TextMeshProUGUI letterText;
    private GameManager gameManager;
    private GameObject nodeGrid;
    public Image image;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        nodeGrid = GameObject.Find("Node Grid");

        image = GetComponent<Image>();
        letterText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        letterText.text = letter;
    }

    void Update()
    {
        CheckLetters();
    }

    void CheckLetters()
    {
        for (int i = 0; i < nodeGrid.transform.childCount; i++)
        {
            for (int j = 0; j < nodeGrid.transform.GetChild(i).transform.childCount; j++)
            {
                LetterBox selectedSlot = nodeGrid.transform.GetChild(i).transform.GetChild(j).GetComponent<LetterBox>();
                string selectedSlotLetter = selectedSlot.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;

                if (selectedSlotLetter == letter && selectedSlot.tryNum < gameManager.guessNum)
                {
                    if (selectedSlot.slotImage.color == selectedSlot.correctGreen)
                    {
                        image.color = selectedSlot.correctGreen;
                        letterText.color = selectedSlot.white;
                    }
                    else if (selectedSlot.slotImage.color == selectedSlot.almostYellow && selectedSlot.slotImage.color != selectedSlot.correctGreen)
                    {
                        image.color = selectedSlot.almostYellow;
                        letterText.color = selectedSlot.white;
                    }
                    else if (selectedSlot.slotImage.color == selectedSlot.defaultGray && image.color != selectedSlot.correctGreen && image.color != selectedSlot.almostYellow)
                    {
                        image.color = selectedSlot.fadedGray;
                        letterText.color = selectedSlot.fadedWhite;
                    }
                }
            }
        }
    }
}
