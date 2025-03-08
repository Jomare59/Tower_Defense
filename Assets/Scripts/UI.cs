using TMPro;
using UnityEngine;

public class UI : MonoBehaviour{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI moneyText;
    
    private void Update() {
        lifeText.text = PlayerStats.lives.ToString() + " pv";
        moneyText.text = PlayerStats.money.ToString() + "$";
    }

    public void AnimeTextPV(){
        lifeText.fontSize = 150;
        lifeText.color = Color.red;
        Invoke("ResetFontSizeLife", 0.2f);
    }
    private void ResetFontSizeLife(){
        lifeText.fontSize = 75;
        lifeText.color = Color.white;
    }
}
