using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _ui_Text;
    [SerializeField] private Text _recipeText;
    [SerializeField] public Text TimerText;
    [SerializeField] private Text _walletText;
    [SerializeField] private Text _walletGoalText;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private Text _congratulationText;
    [SerializeField] private GameObject _destroyPotionButton;
    
    public GameObject SellButton;

    public void DestroyButtonSetActive(bool x)
    {
        _destroyPotionButton.SetActive(x);
    }

    public void SellButtonSetActive(bool x)
    {
        SellButton.SetActive(x);
    }

    public void EndGame(int x)
    {
        _restartButton.SetActive(true);
        _exitButton.SetActive(true);
        _congratulationText.text = ("Congratulations, you won and scored " + x + " coins");
    }
    public void ChangeGoalText(int x)
    {
        _walletGoalText.text = ("Goal: " + x.ToString() + " Coins");
    }

    public void ChangeWalletText(int x)
    {
        _walletText.text = ("Coins: " + x.ToString());
    }

    public void ChangeText(string text)
    {
        _ui_Text.text = text;
    }

    public void ChangeRecipe(string text = null)
    {
        _recipeText.text = ("Recipe: " + text);
    }
}
