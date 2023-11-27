
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public LvlsData data;

        public SavesYG()
        {
            data = null;
        }

        public SavesYG(LvlsData data)
        {
          this.data = data;
        }
    }
}
