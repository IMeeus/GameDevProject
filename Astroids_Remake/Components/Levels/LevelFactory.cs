using Astroids_Remake.Components.Entities;
using Astroids_Remake.Components.Entities.Meteor;
using Astroids_Remake.Extra;
using Astroids_Remake.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Components.Levels
{
    public interface ILevelFactory
    {
        Level CreateLevel(LevelDifficulty difficulty);
    }

    public class MeteorLevelFactory : ILevelFactory
    {
        private IEntityManager _entityManager;
        private IMeteorFactory _meteorFactory;
        private IGameDimensions _gameDimensions;

        public MeteorLevelFactory(IEntityManager entityManager, IMeteorFactory meteorFactory, IGameDimensions gameDimensions)
        {
            _entityManager = entityManager;
            _meteorFactory = meteorFactory;
            _gameDimensions = gameDimensions;
        }

        public Level CreateLevel(LevelDifficulty difficulty)
        {
            switch (difficulty)
            {
                case LevelDifficulty.EASY: return CreateEasyLevel();
                case LevelDifficulty.NORMAL: return CreateNormalLevel();
                case LevelDifficulty.HARD: return CreateHardLevel();
            }

            throw new InvalidOperationException("Invalid Parameter: difficulty!");
        } 

        private Level CreateEasyLevel()
        {
            Level level = new MeteorLevel(_entityManager, new Background(TextureHolder.Textures["planet_blue"], ScreenLocation.BOTTOMLEFT, _gameDimensions));

            for (int i = 0; i < 3; i++)
                level.AddEntity(_meteorFactory.CreateMeteor(MeteorType.BIG, false));

            return level;
        }

        private Level CreateNormalLevel()
        {
            Level level = new MeteorLevel(_entityManager, new Background(TextureHolder.Textures["planet_brown"], ScreenLocation.TOPRIGHT, _gameDimensions));

            for (int i = 0; i < 5; i++)
                level.AddEntity(_meteorFactory.CreateMeteor(MeteorType.BIG, false));

            return level;
        }

        private Level CreateHardLevel()
        {
            Level level = new MeteorLevel(_entityManager, new Background(TextureHolder.Textures["planet_red"], ScreenLocation.TOPRIGHT, _gameDimensions));

            for (int i = 0; i < 8; i++)
                level.AddEntity(_meteorFactory.CreateMeteor(MeteorType.BIG, false));

            return level;
        }
    }

    public enum LevelDifficulty
    {
        EASY, NORMAL, HARD
    }
}
