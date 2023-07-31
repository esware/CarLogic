using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Dev.Scripts
{
    public class LevelDataProvider:ILevelDataProvider
    {
        private LevelGenerator _levelGenerator;

        public LevelDataProvider(LevelGenerator levelGenerator)
        {
            _levelGenerator = levelGenerator;
        }

        public List<Transform> GetStartPoints()
        {
            List<Transform> startPoints = _levelGenerator.GetStartPoints();
            
            return startPoints;
        }

        public List<Transform> GetEndPoints()
        {
            List<Transform> endPoints = _levelGenerator.GetEndPoints();
            return endPoints;
        }

        public int GetCarCount()
        {
            return _levelGenerator.GetCarCount();
        }
    }
}