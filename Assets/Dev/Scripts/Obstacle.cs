using UnityEngine;

namespace Dev.Scripts
{
    public static class Obstacle
    {
        public static Vector3 scale;
        public static Vector3 position;


        public static Vector3 CalculateScale(Vector3 tempScale)
        {
            var newScale = Vector3.one;
            
            if (tempScale.x > 5)
            {
                newScale.z = Random.Range(5, 10);
                newScale.x = Random.Range(1, 3);
            }

            if (tempScale.z > 5)
            {
                newScale.x = Random.Range(5, 10);
                newScale.z = Random.Range(1, 3);
                
            }

            if (newScale == Vector3.one)
            {
                newScale = new Vector3(1, 1, 5);
            }

            return newScale;
        }

    }
}