using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Button nextButton;
    public int step = 0;

    void Start()
    {
        ShowTutorialStep();
        nextButton.onClick.AddListener(NextStep);
    }

    void ShowTutorialStep()
    {
        switch (step)
        {
            case 0:
             
                break;
            case 1:
            
                break;
            case 2:
           
                break;
              
        }
    }

    void NextStep()
    {
        step++;
        ShowTutorialStep();
    }
}