using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text _ammoText = null;
    public int HP = 1000;
    
    void Start()
    {
        _ammoText.text = HP.ToString() + "HP";
    }
    
    public void Damage()
    {
        HP -= 20;
        _ammoText.text = HP.ToString() + "HP";
    }
}
