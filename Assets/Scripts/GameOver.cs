using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public Text scoreText;
    public AudioClip gameOverClip;
    public GameObject highscore;
    public Text initials;
    public Text topScore;


    public Text letter1;
    public Text letter2;
    public Text letter3;

    char l1 = 'A';
    char l2 = 'A';
    char l3 = 'A';

    int _topScore  = 1232;
    string _initials = "JBB";

    enum HighScoreState { Not, Letter1, Letter2, Letter3, Done};
    enum KeyPress { NONE, LEFT, RIGHT, UP, DOWN};
    HighScoreState currentState;

    // Start is called before the first frame update
    void Start()
    {
       int score = PlayerPrefs.GetInt("score");

       SoundSystem._instance.Quiet();
       SoundSystem._instance.PlayMusic(gameOverClip);

        if (PlayerPrefs.GetInt("highscore") > 0)
        {
            initials.text = PlayerPrefs.GetString("initials");
            topScore.text = PlayerPrefs.GetInt("highscore").ToString();
            _topScore = PlayerPrefs.GetInt("highscore");
        }
        else
        {
            initials.text = _initials;
            topScore.text = _topScore.ToString();
        }

        if (score <= _topScore)
        {
            // Hide UI for entering initials
            highscore.SetActive(false);
            currentState = HighScoreState.Not;

        }
        else
        {
            currentState = HighScoreState.Letter1;
            letter2.fontSize = 86;
            letter3.fontSize = 86;
        }


        if (scoreText != null)
        {
            scoreText.text = "Your Final Score :" + score.ToString();
        }
        else
        {
            scoreText.text = "See how far you can go with our astronaut!";
        }

        Debug.Log(currentState);


    }

    void SizeLetters(HighScoreState s)
    {
        if (s == HighScoreState.Letter1)
        {
            letter1.fontSize = 128;
            letter2.fontSize = 64;
            letter3.fontSize = 64;
        }
        if (s == HighScoreState.Letter2)
        {
            letter1.fontSize = 64;
            letter2.fontSize = 128;
            letter3.fontSize = 64;
        }
        if (s == HighScoreState.Letter3)
        {
            letter1.fontSize = 64;
            letter2.fontSize = 64;
            letter3.fontSize = 128;
        }
    }

    Text GetLetter()
    {
        if (currentState == HighScoreState.Letter1) return letter1;
        if (currentState == HighScoreState.Letter2) return letter2;
        return letter3;
    }

    char GetChar()
    {
        if (currentState == HighScoreState.Letter1) return l1;
        if (currentState == HighScoreState.Letter2) return l2;
        return l3;
    }

    void SetChar(char myChar)
    {
        if (currentState == HighScoreState.Letter1) l1 = myChar;
        if (currentState == HighScoreState.Letter2) l2 = myChar;
        if (currentState == HighScoreState.Letter3) l3 = myChar;
    }


    void UpArrow()
    {
        Text _letter = GetLetter();
        char myChar = GetChar();
        myChar++;
        if (myChar > 'Z') myChar = 'A';
        _letter.text = "" + myChar;
        SetChar(myChar);

    }

    void DownArrow()
    {

        Text _letter = GetLetter();
        char myChar = GetChar();

        myChar--;
        if (myChar < ' ') myChar = 'Z';
        _letter.text = "" + myChar;
        SetChar(myChar);

    }

    private void Update()
    {
        if (currentState == HighScoreState.Done || currentState == HighScoreState.Not) { return; }

        SizeLetters(currentState);



        // We are in a letter
        if (Input.GetKeyDown(KeyCode.UpArrow)) UpArrow();
        if (Input.GetKeyDown(KeyCode.DownArrow)) DownArrow();
        if (Input.GetKeyDown(KeyCode.RightArrow)) currentState = NextState(currentState);

        if (currentState == HighScoreState.Done)
        {
            // Replace initials.
            // Save to prefs
            string hsname = "" + l1 + "" + l2 + "" + l3;
            initials.text = hsname;
            topScore.text = PlayerPrefs.GetInt("score").ToString();
            // int score = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
            PlayerPrefs.SetString("initials", hsname);
            highscore.SetActive(false);
        }



    }


    HighScoreState NextState(HighScoreState s)
    {
        if (s == HighScoreState.Not) return HighScoreState.Letter1;
        if (s == HighScoreState.Letter1) return HighScoreState.Letter2;
        if (s == HighScoreState.Letter2) return HighScoreState.Letter3;
        if (s == HighScoreState.Letter3) return HighScoreState.Done;
        return HighScoreState.Not;
    }





}
