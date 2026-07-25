using System.Collections;
using UnityEngine;

public class GameOver : IAction
{
    public bool Victory;

    public GameOver(bool victory)
    {
        Victory = victory;
    }

    public IEnumerator Execute()
    {
        if (Victory)
        {
            UIManager.Instance.ShowGameOverScreen("You win!");
        } else
        {
            UIManager.Instance.ShowGameOverScreen("You died.");
        }

        PlayerPrefs.SetInt("HighScore", GameManager.Instance.Score);

        return null;
    }
}
