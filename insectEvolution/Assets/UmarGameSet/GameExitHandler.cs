using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameExitHandler : MonoBehaviour
{

    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject mainCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitPanel)
            {
                if (Mediation_Manager.instance) Mediation_Manager.instance.ShowExitBanner();
                Time.timeScale = 0;
                exitPanel.SetActive(true);
                mainCanvas.SetActive(false);
                if (Mediation_Manager.instance) Mediation_Manager.instance.HideNativeBanner();
            }

        }

    }

    public void onUserClickYesNo(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }
        else
        {
            Mediation_Manager.instance.ShowAppOpenAd();
            Time.timeScale = 1;
            exitPanel.SetActive(false);
            mainCanvas.SetActive(true);
            if (Mediation_Manager.instance) Mediation_Manager.instance.HideExitBanner();

        }
    }



}


