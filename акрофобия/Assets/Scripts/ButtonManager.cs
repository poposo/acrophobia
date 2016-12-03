using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class  ButtonManager: MonoBehaviour {
    public void StartBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }
    public void ExitGameBtn(){
        Application.Quit();
    }

}
