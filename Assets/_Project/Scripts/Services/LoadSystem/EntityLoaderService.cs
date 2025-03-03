using System.Collections.Generic;
using _Project.Scripts.Services.LoadSystem.LoaderEntity;

namespace _Project.Scripts.Services.LoadSystem
{
    public class EntityLoaderService
    {
        private List<ILoadingEntity> _loadingEntities = new();

        public void AddLoadingEntity(ILoadingEntity loadingEntity) => _loadingEntities.Add(loadingEntity);

        public void LoadEntities()
        {
            foreach (var entity in _loadingEntities)
                entity.Load();
        }
    }
}