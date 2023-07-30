using System;
using UnityEngine;

namespace Dev.Scripts
{
    public class ObjectPreview:MonoBehaviour
    {
        private LevelBuilder _levelBuilder;

        private void Start()
        {
            _levelBuilder = FindObjectOfType<LevelBuilder>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Platform"))
            {
                _levelBuilder.haveInteraction = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Platform"))
            {
                _levelBuilder.haveInteraction = false;
            }
        }
    }
}