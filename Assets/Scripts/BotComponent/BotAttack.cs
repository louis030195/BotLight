using UnityEngine;
using UnityEngine.UI;

namespace BotLight
{
    public class BotAttack : MonoBehaviour
    {
        public int damage = 10;
        private bool attacked;
        private float nextAttackTime; // kind of attack speed thing, useful

        
        public void Attack(float attackRate, RaycastHit hit) // attackSpeed ?
        {
            BotHealth target = hit.rigidbody.GetComponent<BotHealth>();
            Debug.Log("BotAttack : " + Time.time + " | " + nextAttackTime + " | " + target);
            // Find the BotHealth script associated with the rigidbody.
            if (target)
            {
                
                if (Time.time > nextAttackTime && target.isActiveAndEnabled)
                {
                    nextAttackTime = Time.time + attackRate;
                    // Set the fired flag so only Fire is only called once.
                    attacked = true;



                    if (target.getHealth() <= 0)
                        return;

                    target.TakeDamage(damage);

                }
            }

        }
    }
}