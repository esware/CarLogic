using System;
using UnityEngine;

namespace Dev.Scripts
{
    public class LevelBuilder:MonoBehaviour
    {
        public GameObject[] prefabs;
        public GameObject[] prefabViews;

        private bool _canGameObjectCreated = false;
        public bool haveInteraction = false;

        private Transform _socket;
        private GameObject _selectedGameObject;

        private void Start()
        {
            prefabViews[0].SetActive(true);
            _selectedGameObject = prefabViews[0];
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                prefabViews[1].SetActive(false);
                prefabViews[0].SetActive(true);
                _selectedGameObject = prefabViews[0];
                _selectedGameObject.GetComponent<Renderer>().material.color = Color.green;
                
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                prefabViews[0].SetActive(false);
                prefabViews[1].SetActive(true);
                _selectedGameObject = prefabViews[1];
                _selectedGameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            
            if (_canGameObjectCreated)
            {
                _selectedGameObject.GetComponent<Renderer>().material.color =Color.green;
            }
            else
            {
                _selectedGameObject.GetComponent<Renderer>().material.color =Color.red;
            }
            
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out RaycastHit hit , 10f))
            {
                _selectedGameObject.SetActive(true);
                
                if (hit.transform.CompareTag("Platform")|| haveInteraction)
                {
                    _canGameObjectCreated = false;
                }
                else
                {
                    _canGameObjectCreated = true;
                }

                if (hit.transform.CompareTag("Socket")&& _selectedGameObject.name == "G")
                {
                    _socket = hit.transform;

                    if (_canGameObjectCreated)
                    {
                        _selectedGameObject.transform.position = _socket.transform.position;
                    }
                    
                    if (Input.GetMouseButtonDown(0) && _canGameObjectCreated)
                    {
                        Instantiate(prefabs, _selectedGameObject.transform.position, _selectedGameObject.transform.rotation);
                        _socket = null;
                    }
                }
                else if (_selectedGameObject.name == "G")
                {
                    _selectedGameObject.transform.position = hit.point;
                    
                    if (Input.GetMouseButtonDown(0) && _canGameObjectCreated)
                    {
                        Instantiate(prefabs, _selectedGameObject.transform.position, _selectedGameObject.transform.rotation);
                        _socket = null;
                    }
                }
            }
            else
            {
                _selectedGameObject.SetActive(false);
            }
        }
    }
}