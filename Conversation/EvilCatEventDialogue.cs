using FMOD;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using static EvilCat.Conversation.CommonDefinitions;
using EvilCat.External;

namespace EvilCat.Conversation;

internal class EvilCatEventDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {


            //Dialogue related to getting to a repair yard.
            {
                "EvilCat_Dialogue_Shopkeeper_0", new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmShopkeeper, "Meowdy!", true),
                        new(AmEvilCat, "feral", "<c=ff96f3>I will destroy you.</c>"),
                        new(AmShopkeeper, "Ouch.", true),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "EvilCat_Dialogue_Shopkeeper_1", new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "smartass", "<c=ff96f3>I think you dropped that USB.</c>"),
                        new(AmShopkeeper, "I'm not falling for that.", true),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "EvilCat_Dialogue_Shopkeeper_2", new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmShopkeeper, "Love the new look.", true),
                        new(AmEvilCat, "smug", "<c=ff96f3>Thank you.</c>"),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },
            {
                "EvilCat_Dialogue_Shopkeeper_3", new()
                {
                    type = NodeType.@event,
                    lookup = [ "shopBefore" ],
                    bg = "BGShop",
                    allPresent = [ AmEvilCat ],
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>Greetings.</c>"),
                        new(AmShopkeeper, "Hi there.", true),
                        new(new Jump{key = "NewShop"})
                    ]
                }
            },






            //Dialogue for the ephermeral events
            {
                $"ChoiceCardRewardOfYourColorChoice_{AmEvilCat}", new()
                {
                    type = NodeType.@event,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    bg = "BGBootSequence",
                    dialogue =
                    [
                        new(AmVoid, "You returned.", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Why wouldn't I?</c>")
                    ]
                }
            },
            {
                "ForeignCardOffering_After", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "<c=ff96f3>This timestream was even less stable than I remember.</c>")
                    ]
                }
            },
            {
                "ForeignCardOffering_Refuse", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "<c=ff96f3>I was already assisting them. Your offering is unneccessary.</c>")
                    ]
                }
            },
            {
                "EphemeralCardGift", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "squint", "<c=ff96f3>In retrospect, your intrusion was rather rude.</c>")
                    ]
                }
            },



            //Dialogue for picking EvilCat from the crystal pilot event
            {
                $"CrystallizedFriendEvent_{AmEvilCat}", new()
                {
                    type = NodeType.@event,
                    oncePerRun = true,
                    allPresent = [ AmEvilCat ],
                    bg = "BGCrystalizedFriend",
                    dialogue =
                    [
                        new(new Wait{secs = 1.5}),
                        new(AmEvilCat, "feral", "<c=ff96f3>Thank you for giving me control.</c>")
                    ]
                }
            },

            //Dialogue for the forced card remove event
            {
                "LoseCharacterCard", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "<c=ff96f3>I'm not in the mood for a void dip today.</c>")
                    ]
                }
            },

            {
                $"LoseCharacterCard_{AmEvilCat}", new()
                {
                    type = NodeType.@event,
                    allPresent = [ AmEvilCat ],
                    oncePerRun = true,
                    bg = "BGSupernova",
                    dialogue =
                    [
                        new(AmEvilCat, "neutral", "<c=ff96f3>I'll jump in to get that back later.</c>")
                    ]
                }
            },

            //Dialogue for dracula
            {
                "DraculaTime", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "<c=ff96f3>Greetings, Dracula.</c>")
                    ]
                }
            },

            //Dialogue for repairing the ship
            {
                "AbandonedShipyard_Repaired", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "angry", "<c=ff96f3>Who wrote these subroutines?! No wonder the ship's running so bad!</c>")
                    ]
                }
            },

            //Grandma dialogue
            {
                "GrandmaShop", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "smug", "<c=ff96f3>Just a bagel, please.</c>")
                    ]
                }
            },

            //Soggins.
            {
                "SogginsEscape_1", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "feral", "<c=ff96f3>I will destroy you.</c>")
                    ]
                }
            },
            {
                "Soggins_Infinite", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "squint", "<c=ff96f3>You didn't download those viruses on purpose, did you?</c>")
                    ]
                }
            }



        });
    }
}
