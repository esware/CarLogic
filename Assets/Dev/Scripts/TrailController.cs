namespace Dev.Scripts
{
    using UnityEngine;

    public class TrailController : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;
        void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _trailRenderer.time = Mathf.Infinity;
        }

        public void StartTrail()
        {
            _trailRenderer.Clear(); 
            _trailRenderer.enabled = true;
        }
        
        public void StopTrail()
        {
            _trailRenderer.enabled = false;
        }
    }

}