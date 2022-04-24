using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private UI_Manager _ui_Manager;
    private int _moneyInWallet = 0;
    [SerializeField] private int _goal = 50;

    private void Start()
    {
        _ui_Manager.ChangeWalletText(_moneyInWallet);
        _ui_Manager.ChangeGoalText(_goal);
    }
    public void MoneyPlus(int x)
    {
        _moneyInWallet += x;
        _ui_Manager.ChangeWalletText(_moneyInWallet);
        if (_moneyInWallet >= _goal)
        {
            _ui_Manager.EndGame(_moneyInWallet);
        }
    }
}
