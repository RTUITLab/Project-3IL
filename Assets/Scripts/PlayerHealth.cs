using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text AmmoText = null;
    public int HP = 1000;
    public GameObject EndGamePanel;
    
    void Start()
    {
        AmmoText.text = HP.ToString() + "HP";
    }
    
    public void Damage()
    {
        HP -= 20;
        AmmoText.text = HP.ToString() + "HP";

        if (HP <= 0)
        {
            HP = 0;
            EndGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
