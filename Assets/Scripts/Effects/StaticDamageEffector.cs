using UnityEngine;

public class StaticDamageEffector : DamageEffector
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.GetComponent<Smasher>())
        {
            enabled = false;
            collision.rigidbody.gameObject.layer = 11;
        }
    }
}
