using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
            print($"PathFollower -> LoadScene (\"Menu\");");
            Destroy(other.transform.parent.gameObject);
        }
    }
}
