using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPlayerMove(Vector2 moveVector)
    {
        moveVector = moveVector.normalized;

        float lastXDir = _animator.GetFloat("xDir");
        float lastYDir = _animator.GetFloat("yDir");

        float xDir = Mathf.Lerp(lastXDir, moveVector.x, Time.deltaTime * 3);
        float yDir = Mathf.Lerp(lastYDir, moveVector.y, Time.deltaTime * 3);

        _animator.SetFloat("xDir", xDir);
        _animator.SetFloat("yDir", yDir);
    }

    public void OnPlayerAttack()
    {

    }

}
