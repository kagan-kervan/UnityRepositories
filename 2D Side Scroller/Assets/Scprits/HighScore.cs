using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class HighScore : MonoBehaviour
{
	[System.Serializable]
	private class HighScoreEntry
	{
		public int score;
		public string name;
		public HighScoreEntry(string temp_name,int temp_score)
		{
			name = temp_name;
			score = temp_score;
		}
	}
	private class HighScores
	{
		public List<HighScoreEntry> highScoreEntries;
		public HighScores(List<HighScoreEntry> entry)
		{
			highScoreEntries = entry;
		}
	}

	public Transform container;
	public Transform template;
	private List<HighScoreEntry> highScoreList;
	private List<Transform> transformList;

	public void ClearAll()
	{
		highScoreList.Clear();
		transformList.Clear();
		PlayerPrefs.DeleteAll();
	}
	public void CreateList()
	{
		string jsonString = PlayerPrefs.GetString("highscoreTable");
		HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
		//sorting
		for (int i = 0; i < highscores.highScoreEntries.Count; i++)
		{
			for (int j = 0; j < highscores.highScoreEntries.Count; j++)
			{
				if (highscores.highScoreEntries[j].score < highscores.highScoreEntries[i].score)
				{
					HighScoreEntry temp = highscores.highScoreEntries[i];
					highscores.highScoreEntries[i] = highscores.highScoreEntries[j];
					highscores.highScoreEntries[j] = temp;
				}
			}
		}
		highScoreList = highscores.highScoreEntries;
		transformList = new List<Transform>();
		for (int i = 0; i < 10; i++)
		{
			HighScoreEntry entry = highScoreList[i];
			CreateHighScoreEntryTransfrom(entry, container, transformList);
		}
	}

	public void CreateNewHighScoreList()
	{

		HighScores highScores = new HighScores(highScoreList);
		string json = JsonUtility.ToJson(highScores);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
		AddPlayertoHighScore(200, "ADD");
		AddPlayertoHighScore(400, "ALI");
		AddPlayertoHighScore(320, "ATT");
		AddPlayertoHighScore(400, "AAA");
		AddPlayertoHighScore(600, "ASS");
	}

	private void CreateHighScoreEntryTransfrom(HighScoreEntry entry, Transform contaier, List<Transform> transforms)
	{
		float templateHeight = 30f;
		Transform entryTransform = Instantiate(template, contaier);
		RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = new Vector2(0, -templateHeight * transforms.Count);
		entryTransform.gameObject.SetActive(true);
		entryTransform.Find("POSText").GetComponent<TextMeshProUGUI>().text = "" + (transforms.Count + 1);
		entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = entry.name;
		entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "" + entry.score;
		transforms.Add(entryTransform);
	}

	public void AddPlayertoHighScore(int score, string name)
	{
		if (PlayerPrefs.GetInt("HighScoreCreation") <= 0)
		{
			PlayerPrefs.SetInt("HighScoreCreation", 1);
			CreateNewHighScoreList();
		}
		HighScoreEntry entri = new HighScoreEntry(name, score);
		string jsonString = PlayerPrefs.GetString("highscoreTable");
		HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
		highscores.highScoreEntries.Add(entri); 
		string json = JsonUtility.ToJson(highscores);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
	}
}
