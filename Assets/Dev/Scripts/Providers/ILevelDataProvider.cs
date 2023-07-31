using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Dev.Scripts
{
    public interface ILevelDataProvider
    {
        List<Transform> GetStartPoints();

        List<Transform> GetEndPoints();

        int GetCarCount();
    }
    
}