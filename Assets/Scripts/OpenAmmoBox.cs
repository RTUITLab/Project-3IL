using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAmmoBox : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject Box_Top = null;
    public void OpenBox() => _animator.SetTrigger("OpenBox");
}
