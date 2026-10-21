using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableMovementAndJump()
    {
        player.DisableMovementAndJump();
    }

    private void EnableMovementAndJump()
    {
        player.EnableMovementAndJump();
    }
}
