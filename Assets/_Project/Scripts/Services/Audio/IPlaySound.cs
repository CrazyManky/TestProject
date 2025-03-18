namespace _Project.Scripts.Services.Audio
{
    public interface IPlaySound
    {
        public void PlayEnemyShotSound();
        public void PlayCollisionExitZone();
        public void PlayBackgroundSound();
    }
}