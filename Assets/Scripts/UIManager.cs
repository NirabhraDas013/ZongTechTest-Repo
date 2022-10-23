using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public delegate void InstrumentCategoryOpen();
    public static event InstrumentCategoryOpen OnInstrumentCategoryOpened;

    public delegate void ExitButtonPress();
    public static event ExitButtonPress OnExitButtonPressed;

    public delegate void RestartButtonPress();
    public static event RestartButtonPress OnRestartButtonPressed;

    public enum BUTTONCATEGORY
    {
        WEAPONS,
        POINTS,
        INSTRUMENTS
    }

    public GameObject mainUICanvas;
    public GameObject hudCanvas;
    public GameObject welcomePanel;
    public GameObject endPanel;
    public GameObject weaponsPanel;
    public GameObject pointsPanel;
    public GameObject instrumentsPanel;

    public Button weaponsButton;
    public Button pointsButton;
    public Button instrumentsButton;

    public Button startButton;
    public Button exitButton;
    public Button restartButton;

    public AudioSource changeCategorySound;

    private void Start()
    {
        mainUICanvas.SetActive(false);
        weaponsPanel.SetActive(false);
        pointsPanel.SetActive(false);
        instrumentsPanel.SetActive(false);

        //Set Button OnClick Functions (Because Unity is infuriating and will not let me select the function because I have custom enum in the parameters grrrrrr)
        weaponsButton.onClick.AddListener(() => ButtonClick(BUTTONCATEGORY.WEAPONS));
        pointsButton.onClick.AddListener(() => ButtonClick(BUTTONCATEGORY.POINTS));
        instrumentsButton.onClick.AddListener(() => ButtonClick(BUTTONCATEGORY.INSTRUMENTS));

        startButton.onClick.AddListener(HideWelcomePanel);
        exitButton.onClick.AddListener(() => { OnExitButtonPressed?.Invoke(); });
        restartButton.onClick.AddListener(() => { OnRestartButtonPressed?.Invoke(); });

        ShowWelcomePanel();
    }

    public void ToggleMainUI(bool state)
    {
        mainUICanvas.SetActive(state);
    }

    private void SetSelectedButtonColor(Button selectedButton)
    {
        weaponsButton.image.color = Color.white;
        pointsButton.image.color = Color.white;
        instrumentsButton.image.color = Color.white;

        selectedButton.image.color = Color.cyan;
    }

    private void SetSelectedPanel(GameObject panelToTurnOn)
    {
        weaponsPanel.SetActive(false);
        pointsPanel.SetActive(false);
        instrumentsPanel.SetActive(false);

        panelToTurnOn.SetActive(true);
    }

    private void ShowWelcomePanel()
    {
        hudCanvas.SetActive(true);
        welcomePanel.SetActive(true);
        endPanel.SetActive(false);
    }

    private void HideWelcomePanel()
    {
        hudCanvas.SetActive(false);
        welcomePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void ShowEndPanel()
    {
        hudCanvas.SetActive(true);
        welcomePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void ButtonClick(BUTTONCATEGORY buttonCategory)
    {
        changeCategorySound.Play();

        switch (buttonCategory)
        {
            case BUTTONCATEGORY.WEAPONS:
                SetSelectedButtonColor(weaponsButton);
                SetSelectedPanel(weaponsPanel);
                break;
            case BUTTONCATEGORY.POINTS:
                SetSelectedButtonColor(pointsButton);
                SetSelectedPanel(pointsPanel);
                break;
            case BUTTONCATEGORY.INSTRUMENTS:
                SetSelectedButtonColor(instrumentsButton);
                SetSelectedPanel(instrumentsPanel);
                OnInstrumentCategoryOpened?.Invoke();
                break;
            default:
                break;
        }
    }
}
