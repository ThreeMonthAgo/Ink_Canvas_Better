using System.IO;
using System.IO.Compression;
using System.Windows.Ink;
using Ink_Canvas_Better.Controls.ICBInkCanvas;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services;

public class InkCanvasService(SettingsService settingsService)
{
    private readonly SettingsService settingsService = settingsService;

    public void SaveData(ICBInkCanvas inkCanvas, string? path = null)
    {
        path ??= settingsService.Settings.DataDirPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd HH_mm_ss}.zip");
        using var zipStream = new FileStream(path, FileMode.Create);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
        // Save strokes
        {
            var stkEntry = archive.CreateEntry("strokes.isf");
            using var stkStream = stkEntry.Open();
            inkCanvas.Strokes.Save(stkStream);
        }
        // Save history
        {
            var historyEntry = archive.CreateEntry("history.json");
            using var historyStream = historyEntry.Open();
            using var writer = new StreamWriter(historyStream);
            writer.Write(JsonConvert.SerializeObject(inkCanvas.History));
        }
    }

    public void LoadData(ICBInkCanvas inkCanvas, string path)
    {
        using var zipStream = new FileStream(path, FileMode.Open);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        // Load strokes
        {
            var stkEntry = archive.GetEntry("strokes.isf");
            using var stkStream = stkEntry.Open();
            inkCanvas.Strokes = new StrokeCollection(stkStream);
        }
        // Load history
        {
            var historyEntry = archive.GetEntry("history.json");
            using var historyStream = historyEntry.Open();
            using var reader = new StreamReader(historyStream);
            inkCanvas.History = JsonConvert.DeserializeObject<StrokeHistory>(reader.ReadToEnd()) ?? new();
        }
    }
}
