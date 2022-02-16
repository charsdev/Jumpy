using Jumpy;
using System;
using System.Collections.Generic;
using UnityEngine;
using Chars.Tools;

namespace Chars
{
    public class Room : MonoBehaviour
    {
        public Camera Camera;
        public bool CurrentRoom = false;
        List<IPooleable> _pooleablesObjects = new List<IPooleable>();
        private BoxCollider2D  _collider;
        public ContactFilter2D contactFilter = new ContactFilter2D();
        public bool OnStart;


        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _pooleablesObjects = GetPooleablesObjects();
            OnStart = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CurrentRoom = true;
                foreach (var pooleableObject in _pooleablesObjects)
                {
                    pooleableObject.Capture();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CurrentRoom = false;
                
                foreach (var pooleableObject in _pooleablesObjects)
                {
                    pooleableObject.Release();
                }

            }
        }

        private void Update() 
        {
            if (!CurrentRoom && OnStart)
            {
                foreach (var pooleableObject in _pooleablesObjects)
                {
                    pooleableObject.Release();
                }
                OnStart = false;
            }

            Camera.gameObject.SetActive(CurrentRoom);
        }

        private List<IPooleable> GetPooleablesObjects()
        {
            List<IPooleable> pooleables = new List<IPooleable>();

            Collider2D[] colliders = Physics2D.OverlapAreaAll(_collider.bounds.min, _collider.bounds.max);
            
            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out IPooleable pooleable))
                {
                    pooleables.Add(pooleable);
                }
            }
            
            return pooleables;
        }
    }
}
