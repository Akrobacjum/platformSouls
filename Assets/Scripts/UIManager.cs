using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject CharacterSubmenu;
    [SerializeField] GameObject MapSubmenu;

    bool MenuActive;
    bool CharacterSubmenuActive;
    bool MapSubmenuActive;
    private void Start()
    {
        //Resumes and turns off all menus at the beginning.
        Time.timeScale = 1;
        Menu.SetActive(false);
        MenuActive = false;

        CharacterSubmenu.SetActive(false);
        CharacterSubmenuActive = false;

        MapSubmenu.SetActive(false);
        MapSubmenuActive = false;
    }
    private void Update()
    {
        MenuPause();
        //Checks if one of submenus is already active. If so, resuming game makes player turn submenus off too.
        if (CharacterSubmenuActive || MapSubmenuActive)
        {
            CancelSubmenu();
        }
    }
    public void MenuPause()
    {
        //Checks if menu is already active and picks if they should be turned on or off.
        if (MenuActive == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
     }
    public void Pause()
    {
        //Pauses game and turns on menu.
        Time.timeScale = 0;
        Menu.SetActive(true);
        MenuActive = true;
    }
    public void Resume()
    {
        //Resumes game and tuns off menu.
        CancelSubmenu();
        Time.timeScale = 1;
        Menu.SetActive(false);
        MenuActive = false;
    }
    public void CharacterMenu()
    {
        //Turns on character menu, turns off all other submenus.
        CharacterSubmenu.SetActive(true);
        CharacterSubmenuActive = true;

        MapSubmenu.SetActive(false);
        MapSubmenuActive = false;
    }
    public void MapMenu()
    {
        //Turns on map menu, turns off all other submenus.
        MapSubmenu.SetActive(true);
        MapSubmenuActive = true;

        CharacterSubmenu.SetActive(false);
        CharacterSubmenuActive = false;
    }
    public void CancelSubmenu()
    {
        //Turns off all submenus.
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            CharacterSubmenu.SetActive(false);
            CharacterSubmenuActive = false;

            MapSubmenu.SetActive(false);
            MapSubmenuActive = false;
        }
    }
    public void OnClickChangeScene(int sceneNumberInBuildSetting)
    {
        //Changes scene.
        SceneManager.LoadScene(sceneNumberInBuildSetting);
    }
    public void OnClickExitGame()
    {
        //Quits game.
        Application.Quit();
    }
}     

