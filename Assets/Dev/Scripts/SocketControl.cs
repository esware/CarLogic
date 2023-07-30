using UnityEngine;

namespace Dev.Scripts
{
    public class SocketControl:MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Platform"))
            {
                Destroy(gameObject);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Platform"))
            {
                Destroy(gameObject);
            }
        }
    }
}