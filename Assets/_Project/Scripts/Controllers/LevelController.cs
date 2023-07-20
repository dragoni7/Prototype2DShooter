using UnityEngine;
using Utils;

namespace dragoni7
{
    public class LevelController : Singletone<LevelController>
    {
        private Level _level;
        public Level Level => _level;

        [SerializeField]
        private LevelGenerator generator;

        protected override void Awake()
        {
            base.Awake();
            generator.OnGenerationFinished += GetGeneratedLevel;
        }
        public void InitLevel(GenerationData generationParams)
        {
            generator.GenerateLevel(generationParams);
        }

        private void GetGeneratedLevel(Level level)
        {
            _level = level;
            GameController.Instance.ChangeState(GameController.GameState.SpawningPlayers);
        }
    }
}
