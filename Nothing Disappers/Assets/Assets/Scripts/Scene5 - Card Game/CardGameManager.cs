using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameManager : MonoBehaviour
{
    
    public float playerDefence;
    public float playerAttack;
    private float opponentDefence;
    private float opponentAttack;
    private int duel = 1;
    public GameObject[] playerCardButtons;
    public GameObject batlleObject;
    public GameObject[] opponentCards;
    public GameObject[] opponentCardImages;
    public TextMeshProUGUI[] playerTexts;
    public TextMeshProUGUI[] opponentTexts;
    public TextMeshProUGUI resultText;
    public SceneManager scene;
    private float loseDelay= 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (loseDelay != 0)
		{
            loseDelay = loseDelay - Time.deltaTime;
			if (loseDelay <= 0)
			{
                scene.GoLoseMenu();
			}
		}
    }

    public void PlayCard1()
	{
        batlleObject.SetActive(true);
        playerCardButtons[0].SetActive(false);
        playerAttack = 1700;
        playerDefence = 1500;
        playerTexts[0].text = "DRAGON";
        playerTexts[1].text = "DEF:1500\nATK:1700";
        PrepareOpponentCard();
        Fight();
    }
    public void PlayCard2()
    {
        batlleObject.SetActive(true);
        playerCardButtons[1].SetActive(false);
        playerAttack = 900;
        playerDefence = 1100;
        playerTexts[0].text = "ICE GOLEM";
        playerTexts[1].text = "DEF:900\nATK:1100";
        PrepareOpponentCard();
        Fight();
    }
    public void PlayCard3()
    {
        batlleObject.SetActive(true);
        playerCardButtons[2].SetActive(false);
        playerAttack = 800;
        playerDefence = 1200;
        playerTexts[0].text = "TURTOISE";
        playerTexts[1].text = "DEF:1200\nATK:800";
        PrepareOpponentCard();
        Fight();
    }

    private void PrepareOpponentCard()
	{
        if(duel>1)
            opponentCardImages[duel - 2].SetActive(false);
        opponentCardImages[duel - 1].SetActive(true);
        opponentCards[duel - 1].SetActive(false);
        if (duel == 1)
		{
            opponentDefence = 1000;
            opponentAttack = 1200;
            opponentTexts[0].text = "WYERN";
            opponentTexts[1].text = "DEF:1000\nATK:1200";
		}
        else if(duel == 2)
        {
            opponentDefence = 100;
            opponentAttack = 0;
            opponentTexts[0].text = "OSAKA";
            opponentTexts[1].text = "DEF:100\nATK:0";
        }
        else if(duel == 3)
        {
            opponentDefence = 750;
            opponentAttack = 1000;
            opponentTexts[0].text = "TIGER";
            opponentTexts[1].text = "DEF:750\nATK:1000";
        }
    }
    private void Fight()
	{
		if (playerAttack >= opponentDefence)
		{
            resultText.text = "WIN!";
            duel++;
            CheckEndGame(true);
        }
		else
		{
            resultText.text = "LOSE!";
            CheckEndGame(false);
		}
        
    }

    private void CheckEndGame(bool isWon)
	{
		if (!isWon)
		{
			for (int i = 0; i < playerCardButtons.Length; i++)
			{
                playerCardButtons[i].SetActive(false);
			}
            Debug.Log("Lose!");
            loseDelay = 1.5f;
		}
        else if(duel > 3)
		{
            Debug.Log("game won.");
            scene.ActiveTransition();
		}
	}
}
