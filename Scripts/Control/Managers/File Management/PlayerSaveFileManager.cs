public class PlayerSaveFileManager : BasePersistentDataFileManager<PlayerSaveStorage>
{
    protected override void Awake()
    {
        base.Awake();

        fileName = "PlayerSave";
    }

    public override bool IsDataExist()
    {
        return LoadData().saves.Count > 0;
    }

    protected override PlayerSaveStorage InitFirstSave()
    {
        PlayerSaveStorage newData = new()
        {
            saves = new(),

            lastSaveIndex = 0
        };

        return newData;
    }
}