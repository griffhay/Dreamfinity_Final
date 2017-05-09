using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    public Canvas mainCanvasRef;
    public Button startGameBtn;
    public Button exitGameBtn;

    public GameObject quitCheckCanvasRef;
    public Button yesBtn;
    public Button noBtn;
    Scene startMenuScn;
    Scene Level01;

    // Use this for initialization
    void Start () {
        startMenuScn = SceneManager.GetActiveScene();
        Level01 = SceneManager.GetSceneAt(startMenuScn.buildIndex + 1);
             

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnNewGame()
    {
        SceneManager.LoadScene(startMenuScn.buildIndex + 1);
        SceneManager.SetActiveScene(Level01);
    }


    void OnQuitGame()
    {
        quitCheckCanvasRef.SetActive(true);
    }

    void OnConfirmQuit()
    {
        Application.Quit();
    }

    void OnCancelQuit()
    {
        quitCheckCanvasRef.SetActive(false);
    }
}
