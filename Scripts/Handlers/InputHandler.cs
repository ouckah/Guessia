using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class InputHandler : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject nodeGrid;
    private GameObject nodeRow;
    public int slotIndex;
    private string wordList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        nodeGrid = GameObject.Find("Node Grid");

        gameManager.isPlaying = true;

        wordList = "";
        foreach (string word in gameManager.words)
        {
            wordList += word;
        }

        slotIndex = 0;
    }

    void Update()
    {
        if (gameManager.isPlaying)
        {
            KeyboardInput();
        }
    }

    void KeyboardInput()
    {
        if (gameManager.guessNum < gameManager.ALLOWED_GUESSES)
        {
            nodeRow = nodeGrid.transform.GetChild(gameManager.guessNum).gameObject;
        }

        foreach (char letter in Input.inputString)
        {

            // BACKSPACE

            if (letter == '\b')
            {
                if (slotIndex > 0)
                {
                    if (gameManager.guessNum == nodeRow.transform.GetChild(slotIndex - 1).GetComponent<LetterBox>().tryNum)
                    {
                        nodeRow.transform.GetChild(slotIndex - 1).GetComponent<LetterBox>().letter.text = " ";
                        slotIndex--;
                    }
                }
            }

            // ENTER

            else if (letter == '\r')
            {
                if (slotIndex == nodeRow.transform.childCount)
                {
                    string input = SubmitAnswer();
                    if (wordList.Contains(input.ToLower()))
                    {
                        Debug.Log(input);
                        gameManager.guesses.Add(input);

                        slotIndex = 0;
                        gameManager.guessNum++;
                    }
                    else
                    {
                        Debug.Log("\"" + input + "\" not found!");
                    }
                }
            }

            // ALPHA KEYS

            else if (letter >= 'a' && letter <= 'z' || letter >= 'A' && letter <= 'Z')
            {
                if (slotIndex < nodeRow.transform.childCount)
                {
                    if (gameManager.guessNum == nodeRow.transform.GetChild(slotIndex).GetComponent<LetterBox>().tryNum)
                    {
                        nodeRow.transform.GetChild(slotIndex).GetComponent<LetterBox>().letter.text = (letter + "").ToUpper();
                        slotIndex++;
                    }
                }
            }
        }
    }

    string SubmitAnswer()
    {
        string answer = "";
        for (int i = 0; i < nodeRow.transform.childCount; i++)
        {
            answer += nodeRow.transform.GetChild(i).GetComponent<LetterBox>().letter.text;
        }
        return answer;
    }
}
