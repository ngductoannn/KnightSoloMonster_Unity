using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Player player;

    public void CanMoveAndJump()
    {
        player.CanMoveAndJump();
    }
    
    public void NotCanMoveAndJump()
    {
        player.NotCanMoveAndJump();
    }
    
}
