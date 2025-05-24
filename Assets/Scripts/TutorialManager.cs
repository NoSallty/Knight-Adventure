using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText;
    private int currentStep = 0;

    private string[] tutorialSteps = {
        "Welcome to the Game!",
        "Use Arrow Keys to Move",
        "Press Space to Jump",
        "Collect Stars to Score",
        "Avoid Enemies",
        "Press K to open skill tree to learn skill!"
    };

    void Start()
    {
        ShowTutorialStep();
    }

    void Update()
    {
        // Check for user input to advance the tutorial
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextTutorialStep();
        }
    }

    void ShowTutorialStep()
    {
        // Display the current tutorial step
        if (currentStep < tutorialSteps.Length)
        {
            tutorialText.text = tutorialSteps[currentStep];
        }
        else
        {
            // If tutorial is completed, hide the text or trigger game start
            tutorialText.text = "";
            // Add your code to start the game or disable the tutorial UI
        }
    }

    void NextTutorialStep()
    {
        // Move to the next tutorial step
        currentStep++;
        ShowTutorialStep();
    }
}
