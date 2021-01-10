using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // inspector elements
    private Animator anim;
    private CapsuleCollider2D coll;

    private bool isAttacking = false;

    // Layer masks
    [Header ("LAYER MASKS")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask guardLayers;
    [SerializeField] private LayerMask bossLayers;
    [SerializeField] private LayerMask bulletLayers;

    // attack parameters
    [Header("ATTACK PARAMETERS")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 60;
    
    // deflect parameters
    [Header("DEFLECT PARAMETERS")]
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float deflectRange = 0.5f;
    [SerializeField] private Transform deflectPoint;
    [SerializeField] private GameObject deflectedBullet;

    [Header("AUDIOS")]
    public AudioSource deflectSound;
    public AudioSource[] saberSounds;

    private int randomSaberSound;

    [Header("OTHERS")]
    public GameObject space;
    
    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Attack();
        }
    }

    // main attack method
    private void Attack()
    {
        if (!isAttacking)
        {
            // three random sounds to not make it repetitive
            randomSaberSound = (int)Random.Range(1, 3);
            switch (randomSaberSound)
            {
                case 1:
                    saberSounds[0].Play();
                    break;
                case 2:
                    saberSounds[1].Play();
                    break;
                case 3:
                    saberSounds[2].Play();
                    break;
                default:
                    break;
            }
            // There are two attack animations for when it is in the air or it is on the ground
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetTrigger("BasicAttack");
                isAttacking = true;
            }
            else
            {
                anim.SetTrigger("SpinAttack");
                isAttacking = true;
            }

            //detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            Collider2D[] hitGuards = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, guardLayers);
            Collider2D[] deflectedBullets = Physics2D.OverlapCircleAll(attackPoint.position, deflectRange, bulletLayers);
            Collider2D[] bosses = Physics2D.OverlapCircleAll(attackPoint.position, deflectRange, bossLayers);

            // damage the enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MarineController>().TakeDamage(attackDamage);
            }

            // damage the guards
            foreach (Collider2D guard in hitGuards)
            {
                guard.GetComponent<GuardController>().TakeDamage(attackDamage);
            }

            // deflect bullets
            foreach (Collider2D bullet in deflectedBullets)
            {
                bullet.GetComponent<MarineBullet>().SetDeflected(true);
                deflectSound.Play();
                (Instantiate(deflectedBullet, deflectPoint) as GameObject).transform.parent = space.transform;
            }

            // damange the boss
            foreach (Collider2D boss in bosses)
            {
                boss.GetComponent<BossHealth>().TakeDamage(attackDamage);
            }
        }
    }

    // method to show the ranges on editor mode
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint.position, deflectRange);
    }

    // method event for animation
    public void AttackOff()
    {
        isAttacking = false;
    }
}
