using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    private Animator animator;
    public float animationDuration = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        // 현재 애니메이션의 길이를 가져와서 원하는 시간만큼 재생되도록 설정
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        animator.speed = animationLength / animationDuration;

        // 'Lying' 애니메이션을 재생
        animator.Play("Lying");
    }

    void Update()
    {
        // 애니메이션이 끝났을 때 'wakeup' 애니메이션으로 전환
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lying") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.Play("wakeup");
        }
    }
}
