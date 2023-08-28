namespace dragoni7
{
    public class TestWeapon : AbstractWeapon
    {
        public override void PerformAttack(Attributes attributes)
        {
            Emitter.TryEmitBullets(attributes);
        }
    }
}
