using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelTransition : MonoBehaviour {

    public int currentSceen;
    void OnTriggerEnter(Collider other )
    {
        if(other.gameObject.transform.tag == "Player")
        {
        currentSceen = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceen + 1);
        }
    }
}
