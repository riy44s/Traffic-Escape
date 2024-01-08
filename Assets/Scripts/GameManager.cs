using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winningPanel;
    public static GameManager instance;
    public TextMeshProUGUI Count;
    public int count = 2;

    private bool isActive = false;
    public Button button;
    public GameObject timeline;

    public Animation animation1;

    public GameObject gameOver;

    public GameObject helicopterText;

    public Button heli;
    public Button sign;

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        animation1 = GetComponentInChildren<Animation>();
    }

    private void Update()
    {
        if (isActive)
        {
            Helicopter();
        }
       
    }

    public void Helicopter()
    {
        if (button != null)
        {
            isActive = true;

            if (isActive)
            {
                if(helicopterText != null)
                {
                    helicopterText.SetActive(true);
                    Destroy(helicopterText, 2f);
                }
             
                Debug.Log("Button Pressed");
                if (Input.GetMouseButtonUp(1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Car"))
                    {
                        if (count > 0)
                        {
                            count--;
                            Count.text = count.ToString();
                            transform.position = hit.point;
                            animation1.Play();
                            timeline.SetActive(true);
                            Destroy(hit.collider.gameObject, 2f);
                            isActive = true;

                            if (count <= 0)
                            {
                                button.interactable = false;
                                ResetHolicopterAction();
                            }
                        }
                    }
                }
            }
         
        }
    }


    public void ResetHolicopterAction()
    {
        isActive = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }

    public void LevelWinning()
    {
        Time.timeScale = 0;
        winningPanel.SetActive(true);
        heli.interactable = false;
        sign.interactable = false;
    }

    public void LevelUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
