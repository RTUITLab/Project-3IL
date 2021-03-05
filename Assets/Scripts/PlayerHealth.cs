using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text HPText = null;
    public int HP = 3000;

    void Start()
    {
        HPText.text = HP.ToString();
    }

    public void Damage()
    {
        HP -= 10;
        HPText.text = HP.ToString();

        if (HP <= 0)
        {
            HP = 0;
            Time.timeScale = 0;
        }
    }
}
