using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text HPText = null;
    int HP = 3000;
    [SerializeField] GameObject EndGamePanel;

    void Start()
    {
        HPText.text = HP.ToString();
    }

    public void Damage()
    {
        if (HP > 0)
        {
            HP -= 10;
            HPText.text = HP.ToString();
        }
        else
        {
            HPText.text = " ";
            StartCoroutine("EndGame");
        }
    }

    IEnumerator EndGame()
    {
        EndGamePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
