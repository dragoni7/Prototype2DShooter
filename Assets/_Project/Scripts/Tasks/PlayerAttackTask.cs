using System;
using System.Collections.Generic;
using Util;

namespace dragoni7
{
    public class PlayerAttackTask : ITask
    {
        public void Execute()
        {
            AbstractPlayer player = PlayerController.Instance.CurrentPlayer;

            if (PlayerInputController.Instance.IsAttacking && player.CanAttack)
            {
                EventSystem.Instance.TriggerEvent(Events.OnEntityAttack, new Dictionary<string, object> { { "Entity", player } });
            }
            else
            {
                player.CurrentSpeed = player.Attributes.speed;
            }
        }
    }
}
