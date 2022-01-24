using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterBox : MonoBehaviour
{
    private GameManager gameManager;
    private InputHandler inputHandler;
    private string word;

    public int tryNum;
    public int slotNum;
    public Image slotImage;

    public Color32 defaultGray = new Color32(58, 58, 60, 255);
    public Color32 fadedGray = new Color32(58, 58, 60, 100);
    public Color32 white = new Color32(255, 255, 255, 255);
    public Color32 fadedWhite = new Color32(255, 255, 255, 100);
    public Color32 almostYellow = new Color32(181, 159, 59, 255);
    public Color32 correctGreen = new Color32(83, 141, 78, 255);

    public TextMeshProUGUI letter;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        inputHandler = GameObject.Find("Input Handler").GetComponent<InputHandler>();
        slotImage = gameObject.transform.GetChild(0).GetComponent<Image>();

        letter.text = " ";
    }

    void Update()
    {
        word = gameManager.hiddenWord;

        if (gameManager.isPlaying)
        {
            if (gameManager.guessNum > tryNum)
            {
                CheckGuess(letter.text);
            }
        }
    }

    void CheckGuess(string str)
    {
        string guessedWord_ = gameManager.guesses[tryNum];
        string hiddenWord_ = gameManager.hiddenWord;

        List<int> hiddenLetterIndexes = new List<int>();
        List<int> guessedLetterIndexes = new List<int>();

        // If the index and letter in the slot matches to the index and letter in the hidden word
        if (str == hiddenWord_.Substring(slotNum, 1))
        {
            slotImage.color = correctGreen;
            Debug.Log("Correct!");
            return;
        }

        // If the letter but not index of the hidden word matches the letter of slot
        for (int i = 0; i < hiddenWord_.Length; i++)
        {
            if (hiddenWord_.Substring(i, 1) == str && i != slotNum && guessedWord_.Substring(i, 1) != hiddenWord_.Substring(i, 1))
            {
                hiddenLetterIndexes.Add(i);
            }
        }

        for (int i = 0; i < guessedWord_.Length; i++)
        {
            if (guessedWord_.Substring(i, 1) == str)
            {
                guessedLetterIndexes.Add(i);
            }
        }

        Debug.Log("SLOT: " + slotNum.ToString() + "\r HIDDEN WORD INDEXES: " + hiddenLetterIndexes.Count.ToString() + "\r GUESSED WORD INDEXES: " + guessedLetterIndexes.Count.ToString());

        if (guessedLetterIndexes.Count > 0 && hiddenLetterIndexes.Count > 0)
        {
            if (hiddenLetterIndexes.Count >= guessedLetterIndexes.Count)
            {
                slotImage.color = almostYellow;
            }

            else
            {
                foreach (int i in guessedLetterIndexes)
                {
                    if (i < slotNum)
                    {
                        slotImage.color = defaultGray;
                        return;
                    }

                    else
                    {
                        slotImage.color = almostYellow;
                    }
                }
            }
        }
    }
}
