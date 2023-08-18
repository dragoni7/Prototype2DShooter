namespace dragoni7
{
    public class HealingItem : Item
    {
        protected override void OnPlayerPickup(AbstractPlayer player)
        {
            base.OnPlayerPickup(player);
            player.Heal(1);
        }
    }
}
