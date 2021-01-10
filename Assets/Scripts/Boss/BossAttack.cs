using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("ATTACKS DAMAGE")]
    public int swordDamage = 20;
    public int hammerDamage = 40;
    public int leapDamange = 60;

    [Header("RANGE & OFFSET")]
    public float attackRange = 1f;
    public Vector3 attackOffset;
    public LayerMask attackMask;
    public float leapRange = 1f;
    public Vector3 leapOffset;

    // inspector elements
    private Animator anim;
    private BoxCollider2D bc;

    private GameObject player;

    [Header("PROJECTILES PARAMETERS")]
    public GameObject bulletsRight;
    public GameObject bulletsLeft;
    public Transform bulletPoint;
    public Transform bulletsEnragePoint;
    public GameObject space;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bc = GetComponent<BoxCollider2D>();
    }

    // method for sword. Called as an event in animations
    public void SwordAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        
        if (!player.GetComponent<PlayerController>().GetIsDead())
        {
            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
            if (colInfo != null)
            {
                colInfo.GetComponent<PlayerController>().TakeDamage(swordDamage);
            }
        }
    }

    // method for hammer. Called as an event in animations
    public void HammerAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        if (!player.GetComponent<PlayerController>().GetIsDead())
        {
            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
            if (colInfo != null)
            {
                colInfo.GetComponent<PlayerController>().TakeDamage(hammerDamage);
            }
        }
    }
    // method for leap attack. Called as an event in animations
    public void LeapAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * leapOffset.x;
        pos += transform.up * leapOffset.y;

        if (!player.GetComponent<PlayerController>().GetIsDead())
        {
            Collider2D colInfo = Physics2D.OverlapCircle(pos, leapRange, attackMask);
            if (colInfo != null)
            {
                colInfo.GetComponent<PlayerController>().TakeDamage(leapDamange);
            }
        }
    }

    // method to shake camera
    public void CamShaker(int intensity)
    {
        CinemachineShake.Instance.ShakeCamera(intensity, 0.1f);
    }

    // method for instantating the bullets
    public void BulletInstantiate()
    {
        (Instantiate(bulletsRight, bulletPoint) as GameObject).transform.parent = space.transform;
        (Instantiate(bulletsLeft, bulletPoint) as GameObject).transform.parent = space.transform;
    }

    // method for instantating the bullets in phase 2
    public void BulletEnrageInstantiate()
    {
        StartCoroutine(BulletsEnrage());
    }

    private IEnumerator BulletsEnrage()
    {
        Debug.Log("Henlo");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i);
            (Instantiate(bulletsRight, bulletsEnragePoint) as GameObject).transform.parent = space.transform;
            (Instantiate(bulletsLeft, bulletsEnragePoint) as GameObject).transform.parent = space.transform;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ColliderOut()
    {
        bc.enabled = false;
    }

    public void ColliderIn()
    {
        if (bc.enabled == false)
        {
            bc.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * leapOffset.x;
        pos += transform.up * leapOffset.y;

        if (pos == null)
            return;

        Gizmos.DrawWireSphere(pos, leapRange);
    }
}
