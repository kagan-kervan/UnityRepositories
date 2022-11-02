using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public GameObject tileObject;
    public Sprite[] backgroundSprites;
    public Sprite[] scenerarySprites;
    public Sprite[] cloudSprites;
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer sceneraryRenderer;
    public SpriteRenderer[] cloudRenderers;
    // Start is called before the first frame update
    void Start()
    {
        TileDrawing(-8.7f, -4.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackgroundRandomizer()
	{
        int random = Random.Range(0, 3);
        backgroundRenderer.sprite = backgroundSprites[random];
        sceneraryRenderer.sprite = scenerarySprites[random];
		for (int i = 0; i < cloudRenderers.Length; i++)
		{
            cloudRenderers[i].sprite = cloudSprites[random];
		}
	}

    public void TileDrawing(float startpos_x,float startpos_y, float lengthoftile)
	{
		for (int i = 0; i < 20; i++)
		{
            Vector3 tileposition = new Vector3(startpos_x+(lengthoftile*i),startpos_y, 1);
            Instantiate(tileObject, tileposition, tileObject.transform.rotation);
		}
	}
}
