using UnityEngine;
using System.Collections;

public class MayorMovement : MonoBehaviour 
{
    public GameObject m_MayorController;

    public Vector3 m_Velocity;
    public Vector3 m_Rotation;
    public Vector3 m_Scaling;

    public Animator m_Animator;
    public Animator m_IritAnimator;
    public Animator m_SadAnimator;
    public Animator m_BoredAnimator;

    public bool m_FacingRight;
    bool onGround_;

    public float m_JumpForce;

    public bool OnGround
    {
        get
        {
            return onGround_;
        }
        set
        {
            onGround_ = value;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var Mayor = m_MayorController.GetComponent<MayorStats>();
        if (collision.collider.tag == "Ground")
        {
            onGround_ = true;
            m_Animator.SetBool("IsJumping", false);
        }

        if (collision.collider.tag == "Wolf")
        {
            Mayor.m_WolvesKilled++;

            Mayor.m_tempMad += 0.5f;
            m_IritAnimator.SetFloat("Irritation", Mayor.m_tempMad);
        }
        if (collision.collider.tag == "Bunny")
        {
            Mayor.m_tempSad += 1;
            m_SadAnimator.SetInteger("Sadness", Mayor.m_tempSad);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("OnTriggerStay");
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("OnTriggerExit");
    }

    void FixedUpdate()
    {
        if (onGround_ && Input.GetAxis("Jump") > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * m_JumpForce);
            m_Animator.SetBool("IsJumping", true);
            onGround_ = false;
        }
    }
}
