using UnityEngine;

public class TransferDamage : MonoBehaviour
{
    public Target target;

    public void Damage(float damage)
    {
        target.TakeDamage(damage);
    }
}
