using UnityEngine;
using UnityEngine.UI;

public class CoinHandler : MonoBehaviour {
    public int StartingCoins = 50;
    public Text Text;
    private int coins = 0;
    
    public int Coins {
        get {
            return coins;
        }
        set {
            coins = value;
            Text.text = coins.ToString();
        }
    }

    private void Start() {
        Coins = StartingCoins;
    }

    public bool CanAfford (int amount) {
        if (amount <= coins) {
            return true;
        }
        return false;
    }

    public void Purchase (int amount) {
        if (!CanAfford(amount))
            return;
        Coins -= amount;
    }

    public void GainCoins(int amount) {
        Coins += amount;
    }

}
