using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    private const float locomotionAnimationSmoothTime = .1f;

    private NavMeshAgent agent;
    private Animator animator;

	void Start ()
	{
	    agent = GetComponent<NavMeshAgent>();
	    animator = GetComponentInChildren<Animator>();
	}
	

	void Update ()
	{
	    float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
