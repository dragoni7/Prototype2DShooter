namespace dragoni7
{
    public class Player1 : AbstractPlayer
    {
        public override void PerformAttack()
        {
            Weapon.PerformAttack(CurrentAttributes);
        }
    }
}
