using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct GameState {
  public float StartSeconds;
  public float EndSeconds;
  public GameObject SpawnerPrefab;
}

public class HUD : MonoBehaviour
{

    public AudioClip sceneMusic;
    public AudioClip ouchSFX;
    public AudioClip healSFX;

    public List<GameState> gamestates = new List<GameState>();
    int _currentGameState = -1;

    public GameObject currentSpawner;

    public float deadTime = 2.0f;
    float timer;
    float _score;
    int _stability;

    bool _alive = true;

    public float scoreRate = 0.05f;

    TextMeshProUGUI hudText;
    public TextMeshProUGUI lvlText;

    // Start is called before the first frame update
    void Start()
    {

        hudText = gameObject.GetComponent<TextMeshProUGUI>();
        if (scoreRate < 0.0001f) {
            scoreRate = 0.001f;
        }

        // Font size default is 36.

        _stability = 100;
        _alive = true;

        // Since we are going to monkey with the HUD text
        // Set the defaults.
        // Left aligned and 36 point

        hudText.fontSize = 36; // Default to 36
        hudText.alignment = TextAlignmentOptions.Left;

        SoundSystem._instance.PlayMusic(sceneMusic);
        hudText.SetText(hudString());
      
    }

    public int Destabilize(int ouch)
    {
        _stability -= ouch;
        _stability = Mathf.Clamp(_stability, 0, 100);

        if (ouch < 0)
        {
            SoundSystem._instance.PlaySFX(healSFX);
        }
        else
        {
            SoundSystem._instance.PlaySFX(ouchSFX);
        }

        // Is he dead?
        if (_stability < 1)
        {
            int finalScore = Mathf.FloorToInt(_score);
            PlayerPrefs.SetInt("score", finalScore);
            // LevelManager.instance.LoadLevel("Game Over");
            _alive = false;
            hudText.fontSize = 48;
            hudText.alignment = TextAlignmentOptions.Center;
        }
        return _stability;
    }


    string hudString()
    {
        string retval;
        if (_alive)
        {
            retval = "Score :" + Mathf.FloorToInt(_score).ToString() + "\nStability: " + _stability.ToString() + " %\nLevel:" + (_currentGameState+1).ToString();
        }
        else
        {
            retval = "\n\nFinal Score :" + Mathf.FloorToInt(_score).ToString();
        }
        return retval;
    }

    // Update is called once per frame
    void Update()
    {

        if (_alive)
        {
            _score += (scoreRate * (int)(Time.deltaTime * 1000));
        }
        else
        {
            // If we are dead start counting through the dead Time
            timer += Time.deltaTime;
        }

        if (timer > deadTime)
        {
            LevelManager.instance.LoadLevel("Game Over");
        }

        hudText.SetText(hudString());

        if (_currentGameState != FindGamestate()) SetGameState(FindGamestate());
    }

    int FindGamestate() {
      float seconds = GetSecondsAlive();
      for (int i = 0; i < gamestates.Count; i++) {
        if (seconds >= gamestates[i].StartSeconds && seconds < gamestates[i].EndSeconds) {
          return i;
        }
      }
      return _currentGameState;
    }

    void SetGameState(int newGameState) {
      if (currentSpawner != null) {
        Destroy(currentSpawner);
      }

      if (lvlText) {
        lvlText.text = "LEVEL " + (newGameState+1).ToString();
        Animator lvlAni = lvlText.GetComponent<Animator>();
        if (lvlAni) {
          lvlAni.SetTrigger("Show");
        }
      }
      _currentGameState = newGameState;
      currentSpawner = Instantiate(gamestates[_currentGameState].SpawnerPrefab);
    }

    public float GetSecondsAlive() {
      return _score / scoreRate / 1000f;
    }

    public bool isDead() {
      return !_alive;
    }

  public float GetLastLevelStartTime() {
      float seconds = 0;
      foreach (GameState gs in gamestates) {
        seconds = Mathf.Max(seconds, gs.EndSeconds);
      }

      return seconds;
  }
}
