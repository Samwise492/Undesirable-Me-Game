using System.Collections.Generic;

public static class CharacterNameData
{
    private const string JackColour = "#23ad3f";
    private const string McKinseyColour = "red";
    private const string DoctorColour = "#bcbcbc";
    private const string AubreyColour = "#fc9c38";
    private const string JasonColour = "#95d3b3";
    private const string LeahColour = "#9e65a7";
    private const string PediatristColour = "#ffb6c1";
    private const string ShawnColour = "#d4612f";

    public static Dictionary<string, string> englishCharacterNames = new()
    {
        { "Jack", $"<b><color={JackColour}>Jack</color></b>" },
        { "Jacky", $"<b><color={JackColour}>Jacky</color></b>" },
        { "McKinsey", $"<b><color={McKinseyColour}>McKinsey</color></b>" },
        { "Doctor", $"<b><color={DoctorColour}>Doctor</color></b>" },
        { "Aubrey", $"<b><color={AubreyColour}>Aubrey</color></b>" },
        { "Jason", $"<b><color={JasonColour}>Jason</color></b>" },
        { "Leah", $"<b><color={LeahColour}>Leah</color></b>" },
        { "Man", $"<b>Man</b>" },
        { "Pediatrist", $"<b><color={PediatristColour}>Pediatrist</color></b>" },
        { "Shawn", $"<b><color={ShawnColour}>Shawn</color></b>" }
    };

    public static Dictionary<string, string> russianCharacterNames = new()
    {
        { "Джек", $"<b><color={JackColour}>Джек</color></b>" },
        { "Джеки", $"<b><color={JackColour}>Джеки</color></b>" },
        { "МакКинзи", $"<b><color={McKinseyColour}>МакКинзи</color></b>" },
        { "Доктор", $"<b><color={DoctorColour}>Доктор</color></b>" },
        { "Обри", $"<b><color={AubreyColour}>Обри</color></b>" },
        { "Джейсон", $"<b><color={JasonColour}>Джейсон</color></b>" },
        { "Лия", $"<b><color={LeahColour}>Лия</color></b>" },
        { "Мужчина", $"<b>Мужчина</b>" },
        { "Педиатр", $"<b><color={PediatristColour}>Педиатр</color></b>" },
        { "Шон", $"<b><color={ShawnColour}>Шон</color></b>" }
    };
}