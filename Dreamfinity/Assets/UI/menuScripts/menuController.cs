using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class menuController : MonoBehaviour {

    Canvas mainMenu;
    Button play;
    Canvas controlsMenu;
    Button controls;
    Button kbm_txt;
    Button controller_txt;
    Button back;
    Canvas kbm_screen;
    Canvas xbox_screen;
    Canvas creditsScreen;
    Button credits;
    Canvas quitMenu;
    Button quit;
    Button no;
    Button yes;

    void Awake()
    {
        
        mainMenu = GameObject.Find("mainMenu").gameObject.GetComponent<Canvas>();
        controlsMenu = GameObject.Find("controlsMenu").gameObject.GetComponent<Canvas>();
        kbm_screen = GameObject.Find("kbm_screen").gameObject.GetComponent<Canvas>();
        xbox_screen = GameObject.Find("xbox_screen").gameObject.GetComponent<Canvas>();
        creditsScreen = GameObject.Find("creditsScreen").gameObject.GetComponent<Canvas>();
        quitMenu = GameObject.Find("quitMenu").gameObject.GetComponent<Canvas>();

        AudioSource audio = GameObject.FindWithTag("audio_bgm").GetComponent<AudioSource>();
        audio.Play();

    }

    void Start()
    {
        kbm_screen.enabled = false;
        mainMenu.enabled = true;
        controlsMenu.enabled = false;
        xbox_screen.enabled = false;
        creditsScreen.enabled = false;
        quitMenu.enabled = false;
        
    }

    void Update()
    {

    }

    void OnPressPlay()
    {
        Debug.Log("kill me");
        mainMenu.enabled = false;
        SceneManager.LoadScene("LoadScreen");

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressControls()
    {
        Debug.Log("fuck me");
        mainMenu.enabled = false;
        controlsMenu.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressCredits()
    {
        Debug.Log("eat me");
        mainMenu.enabled = false;
        creditsScreen.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressQuit()
    {
        Debug.Log("choke me");
        mainMenu.enabled = false;
        quitMenu.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressKbm()
    {
        Debug.Log("spank me");
        controlsMenu.enabled = false;
        kbm_screen.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressXbox()
    {
        Debug.Log("bite me");
        controlsMenu.enabled = false;
        xbox_screen.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressBack()
    {
        Debug.Log("slap me");
        controlsMenu.enabled = false;
        kbm_screen.enabled = false;
        xbox_screen.enabled = false;
        creditsScreen.enabled = false;
        quitMenu.enabled = false;
        mainMenu.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressNo()
    {
        Debug.Log("marry me");
        quitMenu.enabled = false;
        mainMenu.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressYes()
    {
        Debug.Log("bury me");
        quitMenu.enabled = false;
        Application.Quit();

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }
}
