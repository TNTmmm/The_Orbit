using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Name of the gameplay scene to load")]
    public string gameplaySceneName = "GameScene";

    [Header("UI Panels")]
    public GameObject creditsPanel;
    public GameObject settingsPanel;
    public GameObject tutorialPanel;

    // Exit Confirmation Panel
    [Header("Exit Confirmation")]
    public GameObject exitConfirmationPanel;

    [Header("Game Result Panels")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    [Header("HUD Control")]
    [Tooltip("Drag the HUD_Group (which contains health and weapon UI) here to hide it when menus are opened")]
    public GameObject hudGroup;

    [Tooltip("Drag the weapon UI group (CurrentWeaponUI) here")]
    public GameObject weaponUIGroup;

    public GameObject[] extraUIElements;
    // -------------------------------------------

    //Array to hold objects to hide
    [Header("Objects to Hide")]
    [Tooltip("Drag the BGM and SFX buttons here to hide them when the Exit confirmation appears")]
    public GameObject[] objectsToHideWhenExit;
    // ---------------------------------------------

    private void Start()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (tutorialPanel != null) tutorialPanel.SetActive(false);

        // Hide Exit Confirmation Panel at start
        if (exitConfirmationPanel != null) exitConfirmationPanel.SetActive(false);

    }
    // <summary>
    // ---------------------------------------------
    // </summary>
    private void Update()
    {
        bool isAnyMenuOpen = false;

        if (creditsPanel != null && creditsPanel.activeSelf) isAnyMenuOpen = true;
        if (settingsPanel != null && settingsPanel.activeSelf) isAnyMenuOpen = true;
        if (tutorialPanel != null && tutorialPanel.activeSelf) isAnyMenuOpen = true;
        if (exitConfirmationPanel != null && exitConfirmationPanel.activeSelf) isAnyMenuOpen = true;


        //Victory and Game Over Panels
        if (gameOverPanel != null && gameOverPanel.activeSelf) isAnyMenuOpen = true;
        if (victoryPanel != null && victoryPanel.activeSelf) isAnyMenuOpen = true;
        // --------------------------------

        //If any menu is open -> hide HUD
        bool showHUD = !isAnyMenuOpen;

        if (hudGroup != null) hudGroup.SetActive(showHUD);
        if (weaponUIGroup != null) weaponUIGroup.SetActive(showHUD);

        if (extraUIElements != null)
        {
            foreach (var ui in extraUIElements)
            {
                if (ui != null) ui.SetActive(showHUD);
            }
        }
    }
    // ---------------------------------------------

    public void OnClickStart()
    {
        if (Application.CanStreamedLevelBeLoaded(gameplaySceneName))
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + gameplaySceneName);
        }
    }

    public void OnClickCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }
    }

    public void OnClickSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

    // Tutorial Panel
    public void OnClickTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }
    }

    //Exit Confirmation System

    //3. Function for GameExitButton (click to open confirmation dialog)
    public void OnClickOpenExitConfirmation()
    {
        if (exitConfirmationPanel != null)
        {
            exitConfirmationPanel.SetActive(true);
        }

        //Bury the buttons we dragged in
        foreach (var obj in objectsToHideWhenExit)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    // Function for NO button (click to cancel/close confirmation dialog)
    public void OnClickCloseExitConfirmation()
    {
        if (exitConfirmationPanel != null)
        {
            exitConfirmationPanel.SetActive(false);
        }

        //Bury the buttons we dragged in
        foreach (var obj in objectsToHideWhenExit)
        {
            if (obj != null) obj.SetActive(true);
        }
    }

    // Function for YES button (click to really exit the game) - can use the same one as before
    public void OnClickExit()
    {
        Debug.Log("Player pressed YES to Exit!");
        Application.Quit();
    }

    // ----------------------------------------------

    public void OnClickExitToMainMenu()
    {

        SceneManager.LoadScene(0);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
