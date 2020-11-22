using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StateKeeper : MonoBehaviour
{
    Image heart1;
    Image heart2;
    Image heart3;
    Image heart4;
    Image background;
    Image button;
    Text backgroundText;
    Text buttonText;

    Image[] hearts;

    bool hit;
    int iFrame;
    int iFrames;
    int heartCurrent;
    float healthMax;
    float healthInterval;
    float healthCurrent;
    [SerializeField] float fadeTime;
    int levelNumber;
    

    [SerializeField] Image fullHeart;
    [SerializeField] Image halfHeart;
    [SerializeField] Image emptyHeart;

    [SerializeField] RunTimeData _data;
    [SerializeField] string levelOneName;

    bool GameOver;

    // Start is called before the first frame update
    void Start()
    {
        heart1 = this.transform.GetChild(0).GetComponent<Image>();
        heart2 = this.transform.GetChild(1).GetComponent<Image>();
        heart3 = this.transform.GetChild(2).GetComponent<Image>();
        heart4 = this.transform.GetChild(3).GetComponent<Image>();
        background = this.transform.GetChild(4).GetComponent<Image>();
        backgroundText = this.transform.GetChild(5).GetComponent<Text>();
        button = this.transform.GetChild(6).GetComponent<Image>();
        buttonText = this.transform.GetChild(6).GetChild(0).GetComponent<Text>();

        hearts = new Image[4];
        hearts[0] = heart1;
        hearts[1] = heart2;
        hearts[2] = heart3;
        hearts[3] = heart4;

        heartCurrent = 3;
        healthMax = _data.startingHealth;
        healthCurrent = _data.startingHealth;
        healthInterval = _data.startingHealth / 4;

        GameEvents.PlayerHit += OnPlayerHit;
        GameEvents.GameOver += OnGameOver;
        GameEvents.BeatLevel += OnBeatLevel;

        GameOver = false;
        iFrame = 0;
        iFrames = _data.iFrames;
        button.gameObject.GetComponent<Button>().interactable = false;

        String sceneName = SceneManager.GetActiveScene().name;
        String sceneNumber = sceneName.Substring(sceneName.Length - 1);
        levelNumber = int.Parse(sceneNumber);
    }

    private void Update()
    {
        if (GameOver) 
        {
            DisplayBackground();
        }

        if (hit) 
        {
            iFrame += 1;
            if (iFrame >= iFrames)
                hit = false;
        }
    }

    // Update is called once per frame
    void OnPlayerHit(object sender, BaddieEventArgs args)
    {
        Baddie baddie = args.baddiePayload;
        float damage = baddie.damage;
        if (healthCurrent - damage > 0)
        {
            if (!hit)
            {
                ChangeHeart(baddie.damage);
                hit = true;
                iFrame = 0;
            }
        }
        else 
        {
            GameOver = true;
            button.gameObject.GetComponent<Button>().interactable = true;
            GameEvents.InvokeGameOver();
        }
    }

    void ChangeHeart(float damage) 
    {
        float damageLeftOver = 0f;
        float currDamage;
        currDamage = damage;


        float heartTop = (heartCurrent + 1) * healthInterval;
        float heartBottom = (heartCurrent) * healthInterval;


        if ((healthCurrent - damage) < heartBottom)
        {
            currDamage = healthCurrent - heartBottom;
            damageLeftOver = damage - currDamage;
        }

        healthCurrent -= currDamage;

        if (healthCurrent <= heartTop - healthInterval)
        {
            hearts[heartCurrent].sprite = emptyHeart.sprite;
            heartCurrent -= 1;
        }
        else if (healthCurrent <= heartTop - (healthInterval/2))
        {
            hearts[heartCurrent].sprite = halfHeart.sprite;
        }

        if (damageLeftOver != 0) 
        {
            ChangeHeart(damageLeftOver);
        }

    }

    void DisplayBackground() 
    {
        Color dummyColorBackground = background.color;
        Color dummyColorText = backgroundText.color;
        Color dummyColorButton = button.color;
        Color dummyColorButtonText = buttonText.color;


        dummyColorBackground.a = Mathf.Lerp(dummyColorBackground.a, 255f, .01f * Time.deltaTime * fadeTime);
        dummyColorText.a = Mathf.Lerp(dummyColorBackground.a, 255f, .01f * Time.deltaTime * fadeTime);
        dummyColorButton.a = Mathf.Lerp(dummyColorButton.a, 255f, .01f * Time.deltaTime * fadeTime);
        dummyColorButtonText.a = Mathf.Lerp(dummyColorButtonText.a, 255f, .01f * Time.deltaTime * fadeTime);

        background.color = dummyColorBackground;
        backgroundText.color = dummyColorText;
        button.color = dummyColorButton;
        buttonText.color = dummyColorButtonText;
    }

    void OnBeatLevel(object sender, EventArgs args)
    {
        GameEvents.PlayerHit -= OnPlayerHit;
        GameEvents.GameOver -= OnGameOver;
        GameEvents.BeatLevel -= OnBeatLevel;

        StartCoroutine("GoToNextLevel");
    }

    IEnumerator GoToNextLevel() 
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        String sceneName = "Level" + (levelNumber + 1);
        SceneManager.LoadScene(sceneName);
    }

    void OnGameOver(object sender, EventArgs args)
    {
        GameEvents.PlayerHit -= OnPlayerHit;
        GameEvents.GameOver -= OnGameOver;
        GameEvents.BeatLevel -= OnBeatLevel;
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(levelOneName);
    }
}
