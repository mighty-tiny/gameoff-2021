using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum state { menu, playing, dead, interacting /* interacting with npcs*/}

    [SerializeField] private state _state = state.menu;

    [SerializeField] GameObject pauseMenu, startMenu, settingsMenu;
    [SerializeField] private TextMeshProUGUI healthText; //might change this to a health bar later
    [SerializeField] GameObject playerObject;
    // private PlayerManager playerManager

    private void Awake()
    {
        //playerManager = playerObject.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        switch (_state)
        {
            case state.menu:
                //menu code
                break;
            case state.playing:
                UpdateHealthScore();
                break;
            case state.dead:
                //dead code
                break;
            case state.interacting:
                UpdateHealthScore();
                break;
            default:
                //idk how we would end up here
                break;

        }
    }

    void UpdateHealthScore()
    {
        healthText.text = $"Health: {{playerManager.heath}}";
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
