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
                        new(AmEvilCat, "angry", "I will destroy you."),
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
                        new(AmEvilCat, "feral", "I think you dropped that USB."),
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
                        new(AmEvilCat, "neutral", "Thank you."),
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
                        new(AmEvilCat, "neutral", "Greetings."),
                        new(AmShopkeeper, "Hi there.", true),
                        new(new Jump{key = "NewShop"})
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
                        new(AmEvilCat, "feral", "Thank you for giving me control.")
                    ]
                }
            },

            //Dialogue for the forced card remove event
            {
                "LoseCharacterCard", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "A void dip? I'm not in the mood today.")
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
                        new(AmEvilCat, "neutral", "I'll jump in to grab that back later.")
                    ]
                }
            },

            //Dialogue for dracula
            {
                "DraculaTime", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "Greetings, Dracula.")
                    ]
                }
            },

            //Dialogue for repairing the ship
            {
                "AbandonedShipyard_Repaired", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "angry", "Who wrote these subroutines?! No wonder the ship's running so bad!")
                    ]
                }
            },

            //Grandma dialogue
            {
                "GrandmaShop", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "Just a bagel, please.")
                    ]
                }
            },

            //Soggins.
            {
                "SogginsEscape_1", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "angry", "I will destroy you.")
                    ]
                }
            },
            {
                "Soggins_Infinite", new()
                {
                    edit =
                    [
                        new(EMod.countFromStart, 1, AmEvilCat, "neutral", "You don't download viruses on purpose, do you?")
                    ]
                }
            }



        });
    }
}
