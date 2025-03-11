using System;
using UnityEngine;

namespace _Project.Scripts.GameItems.GameEffects
{
    public class AnnihilationEffect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        public void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
                Destroy(gameObject);
        }
    }
}