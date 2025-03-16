namespace MagicTown_TownAPI.Infastructure
{
    interface IUnitOfWork
    {
        ITownRepo townRepo { get; }
        void Save();
    }
}
