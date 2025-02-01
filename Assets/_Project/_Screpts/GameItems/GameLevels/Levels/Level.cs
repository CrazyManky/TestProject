using _Project._Screpts.Services;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.GameItems.GameLevels.Levels
{
    public class Level : BaseLevel
    {
        
        private PlayerObjectCollector _playerObjectsl;
        private SwitchObjectService _switchObjectService;

        [Inject]
        public void Construct(SwitchObjectService switchObjectService, PlayerObjectCollector playerObjectsl)
        {
            _switchObjectService = switchObjectService;
            _playerObjectsl = playerObjectsl;
        }

        //public void
    }
}