using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text HPText = null;
    public int HP = 3000;

    private void Awake()
    {
        HPText = GameObject.Find("HPText").GetComponent<TMP_Text>();
    }

    void Start()
    {
        HPText.text = HP.ToString() + "HP";
    }
    
    public void Damage()
    {
        HP -= 20;
        HPText.text = HP.ToString() + "HP";

        if (HP <= 0)
        {
            HP = 0;
            Time.timeScale = 0;
        }
    }
}
