using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run_enrage : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private BossController boss;

    public float speed = 4f;
    public float leapRange = 3f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.TurnAround();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= leapRange)
        {
            animator.SetTrigger("LeapAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LeapAttack");
        //bossPosition.position = new Vector2(4.534f, bossPosition.position.y);
    }
}
