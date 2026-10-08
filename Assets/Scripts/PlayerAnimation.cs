using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Check if either axis has input
        bool isWalking = moveX != 0 || moveY != 0;

        anim.SetBool("isWalking", isWalking);
    }
}