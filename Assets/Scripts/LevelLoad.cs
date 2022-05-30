using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour {
    
    public void LoadLevel(string name) {
        LevelManager.instance.LoadLevel (name);
    }

    public void ExitGame() {
      LevelManager.instance.QuitRequest();
    }
}
