using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public Text txtStory;
    public GameObject dim;
    public GameObject loading;

    void Start() {
        if (Utils.currentLevel == 0) {
            txtStory.text = Utils.Story_Tutorial;
        } else {
            txtStory.text = "";
        }
    }

    public void OnClickClose() {
        dim.SetActive(false);
        loading.SetActive(true);

        Invoke("GoNextScene", 1f);
    }

    private void GoNextScene() {
        if (Utils.currentLevel == 0) {
            SceneManager.LoadScene("Level_Tutorial");
        } else {
            SceneManager.LoadScene("Level_" + AddZero(Utils.currentLevel));
        }
    }

    private string AddZero(int val) {
        if (val<10) {
            return "0" + val;
        } else {
            return "" + val;
        }
    }
}
