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
                GameEventManager.Instance.EventBus.Raise(new PlayerAttackEvent());
            }
            else
            {
                player.CurrentSpeed = player.BaseSpeed;
            }
        }
    }
}
