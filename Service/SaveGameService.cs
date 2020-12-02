using System;
using System.IO;
using System.Threading.Tasks;
using XiaSaveFileLib;

namespace XiaSaveEditorWeb.Service
{
    public class SaveGameService
    {
        public event Action OnLoadComplete;
        public SaveFile SaveFile { get; private set; }

        public async Task LoadSaveFile(Stream stream, string fileName)
        {
            //Copy to new stream bc FileInput does not allow sync read
            var mem = new MemoryStream();
            await stream.CopyToAsync(mem);
            await Task.Yield();
            mem.Position = 0;
            SaveFile = await SaveFileManager.LoadSaveFileAsync(mem, fileName);
            System.Console.WriteLine(SaveFile.Header.FileName + " was loaded");
            OnLoadComplete?.Invoke();
        }
    }
}