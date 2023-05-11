using UnityEngine;
using static dragoni7.GameController;
using static dragoni7.ScriptablePlayer;

namespace dragoni7
{
    public abstract class AbstractPlayer : BaseEntity
    {
        public Vector2 EquipPos { get; set; }
        public AbstractWeapon Weapon { get; set; }
        public PlayerStats Stats { get; private set; }

        protected float currentSpeed;
        public float CurrentSpeed
        {
            get { return currentSpeed; } 
            set
            {
                if (value != currentSpeed)
                {
                    currentSpeed = value;
                }
                return;
            }
        }

        protected virtual void Start()
        {
            CurrentSpeed = Stats.speed;
            canMove = true;
            canAttack = true;

            Vector2 weaponPosition = (Vector2)transform.position + EquipPos;
            Weapon.transform.position = weaponPosition;
        }

        public void SetStats(PlayerStats stats)
        {
            Stats = stats;
        }
        protected virtual void OnStateChanged(GameState newState)
        {
            // change logic according to state
        }
        private void Awake()
        {
            // Subscribe events
            GameController.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            // Unsubscribe events
            GameController.OnBeforeStateChanged -= OnStateChanged;
        }
    }
}
