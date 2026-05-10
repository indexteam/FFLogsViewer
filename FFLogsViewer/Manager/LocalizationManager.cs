using System.Collections.Generic;
using Dalamud.Game;
using FFLogsViewer.Properties;
using Newtonsoft.Json;

namespace FFLogsViewer.Manager;

public class LocalizationManager
{
    public enum Language
    {
        English,
        Korean
    }

    private readonly Dictionary<Language, Dictionary<string, string?>> _strings = new();

    private readonly Language currentLanguage;

    public LocalizationManager()
    {
        this.LoadStrings(Language.English);
        this.LoadStrings(Language.Korean);

        this.currentLanguage = Service.DataManager.Language == (ClientLanguage)4
                                   ? Language.Korean
                                   : Language.English;
    }

    public string? GetString(string? key)
        => _strings[currentLanguage].GetValueOrDefault(key, key);

    private void LoadStrings(Language lang)
    {
        var str = lang switch
        {
            Language.English => Resources.ko,
            Language.Korean => Resources.en,
            _ => Resources.ko,
        };

        var dict = JsonConvert.DeserializeObject<Dictionary<string, string?>>(str) ?? [];

        _strings[lang] = dict;
    }
}
