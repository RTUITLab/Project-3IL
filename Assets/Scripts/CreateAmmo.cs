using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAmmo : MonoBehaviour
{
    [SerializeField]GameObject Ammo;
    [SerializeField]int _HowManyAdd=60;
    void Start()
    {
        var transform=this.gameObject.GetComponent<Transform>();
        for (int i = 0; i <_HowManyAdd ; i++)
            StartCoroutine(Create(i));
    }
IEnumerator Create(int time){
  yield return new WaitForSeconds(Random.Range(0,time/10));
  Instantiate(Ammo,transform);
}

}
