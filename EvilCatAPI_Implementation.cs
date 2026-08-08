using Nickel;

namespace EvilCat;


public sealed class EvilCatAPI_Implementation : IEvilCatApi
{

    public IDeckEntry EvilCatDeck
        => ModEntry.Instance.EvilCatDeck;

    public IStatusEntry EvilCatFullMemoryAccessStatus
        => ModEntry.Instance.EvilCatFullMemoryAccessStatus;

    public IStatusEntry EvilCatMemoryMismatchStatus
        => ModEntry.Instance.EvilCatMemoryMismatchStatus;

    public IStatusEntry EvilCatGenericDrawStatus
        => ModEntry.Instance.EvilCatGenericDrawStatus;

    public IStatusEntry EvilCatDeallocateStatus
        => ModEntry.Instance.EvilCatDeallocateStatus;

    public IStatusEntry EvilCatTempShieldExhaustStatus
        => ModEntry.Instance.EvilCatTempShieldExhaustStatus;
}