using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float electricity = 50f;
    public bool gameover = false;
    public Slider electrictyBar;
    private float maxElectricity = 100f;
    public float countdown = 2f;
    public GameObject gameoverPanel;


    void Start() {
        maxElectricity = 100f;
        electricity = 100f;
        electrictyBar.value = CalculateElectricity();
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (electricity <= 0f) {
            gameover = true;
        }

        if (gameover) {
            gameoverPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (!gameover) {
            electricity -= Time.deltaTime * countdown;
            electrictyBar.value = CalculateElectricity();
        }

    }

    public void UpdateElectricity(float points) {
        if (electricity < maxElectricity && (!gameover)) {
            electricity += points;
            electrictyBar.value = CalculateElectricity();
            Debug.Log(electricity);
        }
    }

    float CalculateElectricity() {
        return electricity / maxElectricity;
    }
}
