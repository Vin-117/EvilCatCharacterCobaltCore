using Nickel;

namespace EvilCat;

public interface IEvilCatApi 
{
    IDeckEntry EvilCatDeck { get; }
    IStatusEntry EvilCatFullMemoryAccessStatus { get; }
    IStatusEntry EvilCatMemoryMismatchStatus { get; }
    IStatusEntry EvilCatGenericDrawStatus { get; }

    IStatusEntry EvilCatDeallocateStatus { get; }

    IStatusEntry EvilCatTempShieldExhaustStatus { get; }

}