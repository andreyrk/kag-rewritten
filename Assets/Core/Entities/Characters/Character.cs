using Mirror;
using UnityEngine;

public class Character : NetworkBehaviour
{
    public CapsuleCollider2D col;
    public SpriteRenderer sprite;

    public Animator anim;
    public AnimationClip animIdle;
    public AnimationClip animJump;
    public AnimationClip animMove;

    void Start()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.5f)
        {
            sprite.flipX = false;
            anim.Play(animMove.name);
        }

        if (Input.GetAxis("Horizontal") < -0.5f)
        {
            sprite.flipX = true;
            anim.Play(animMove.name);
        }
    }
}
