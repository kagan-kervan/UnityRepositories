using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    public GameObject tileObject;
    public GameObject[] dayBackgroundObjects;
    public GameObject[] eveningBackgroundObjects;
    public GameObject[] nightBackgroundObjects;
    public Camera mainCamera;
    public GameObject playerObject;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI levelText;
    public GameObject levelClearenceObject;
    public GameObject specialButton1;
    public GameObject specialButton2;
    public GameObject special1PurchasewdText;
    public GameObject special2PurchaseText;
    public GameObject menuObject;
    public GameObject gameOverObject;
    public GameObject levelMusicObject;
    public TextMeshProUGUI scoreText;
    public AudioSource audioSource;
    public AudioClip buySFX;


    private int skinindex;
    // Start is called before the first frame update
    void Start()
    { 
        TileDrawing(-18f, -4.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraTracker();
        UpdatePlayerTexts();
    }

    public void BackgroundRandomizer()
	{
        int random = Random.Range(0, 3);
		switch (random)
		{
            case 0:
				for (int i = 0; i < dayBackgroundObjects.Length; i++)
				{
                    dayBackgroundObjects[i].SetActive(true);
				}
				for (int i = 0; i < eveningBackgroundObjects.Length; i++)
				{
                    eveningBackgroundObjects[i].SetActive(false);  
				}
				for (int i = 0; i < nightBackgroundObjects.Length; i++)
				{
                    nightBackgroundObjects[i].SetActive(false);
				}
                return ;
            case 1:
                for (int i = 0; i < dayBackgroundObjects.Length; i++)
                {
                    dayBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < eveningBackgroundObjects.Length; i++)
                {
                    eveningBackgroundObjects[i].SetActive(true);
                }
                for (int i = 0; i < nightBackgroundObjects.Length; i++)
                {
                    nightBackgroundObjects[i].SetActive(false);
                }
                return ;
            case 2:
                for (int i = 0; i < dayBackgroundObjects.Length; i++)
                {
                    dayBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < eveningBackgroundObjects.Length; i++)
                {
                    eveningBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < nightBackgroundObjects.Length; i++)
                {
                    nightBackgroundObjects[i].SetActive(true);
                }
                return ;
        }
	}

    public void TileDrawing(float startpos_x,float startpos_y, float lengthoftile)
	{
		for (int i = 0; i < 60; i++)
		{
            Vector3 tileposition = new Vector3(startpos_x+(lengthoftile*i),startpos_y, 1);
            Instantiate(tileObject, tileposition, tileObject.transform.rotation);
		}
	}

    public void SetObjectActive(GameObject obj)
	{
        obj.SetActive(true);
	}
    public void SetObjectNotActive(GameObject obj)
	{
        obj.SetActive(false);
	}

    public void UpdateLevelText(int level)
	{
        levelText.text = "Level " + level + " is Complete.";
	}
    public void UpdatePlayerTexts()
	{
        healthText.text = "Health : " + playerObject.GetComponent<Player>().life;
        goldText.text = "Gold : " + playerObject.GetComponent<Player>().gold;
	}

    public void ActivateMenu()
	{
        levelClearenceObject.SetActive(false);
        menuObject.SetActive(true);
        playerObject.SetActive(false);
        SetObjectActive(healthText.gameObject);
        playerObject.GetComponent<Player>().playerStates = Player.States.INMENU;
        SetObjectNotActive(levelMusicObject);

	}
    public void ActivateGameOverMenu()
	{
        Player pl = playerObject.GetComponent<Player>();
        gameOverObject.SetActive(true);
        healthText.gameObject.SetActive(false);
        int score = (int)(pl.gold * 4 + 200);
        scoreText.text = "Your Score : " + score;
        SetObjectNotActive(levelMusicObject);
    }
    public void DeactivateGameOverMenu()
    {
        SetObjectNotActive(gameOverObject);
    }
    public void Special1Button()
	{
        Player player = playerObject.GetComponent<Player>();

        if (player.gold >= 150)
		{
            player.PurchaseSpecial1();
            specialButton1.SetActive(false);
            special1PurchasewdText.SetActive(true);
            audioSource.PlayOneShot(buySFX);
            UpdatePlayerTexts();
        }
    }
    public void ResetButtons()
	{
        SetObjectActive(specialButton1);
        SetObjectActive(specialButton2);
        SetObjectNotActive(special1PurchasewdText);
        SetObjectNotActive(special2PurchaseText);
    }
    public void Special2Button()
    {
        Player player = playerObject.GetComponent<Player>();

        if (player.gold >= 250)
        {
            player.PurchaseSpecial2();
            specialButton2.SetActive(false);
            special2PurchaseText.SetActive(true);
            audioSource.PlayOneShot(buySFX);
            UpdatePlayerTexts();
        }
    }

    public void ExtraHealthButton()
	{
        Player player = playerObject.GetComponent<Player>();
        if(player.gold >= 100)
		{
            player.PurchaseExtraHealth();
            audioSource.PlayOneShot(buySFX);
            UpdatePlayerTexts();
            
		}
	}
    public void CameraTracker()
	{
        Vector2 playerPos = playerObject.transform.position;
        playerPos.y = mainCamera.transform.position.y;
        mainCamera.transform.position = playerPos;
	}
    
   

}
