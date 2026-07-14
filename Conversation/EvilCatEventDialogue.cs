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
                        new(AmEvilCat, "angry", "I will kill you."),
                        new(AmShopkeeper, "You're different.", true),
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
                        new(AmShopkeeper, "Hello.", true),
                        new(AmEvilCat, "angry", "No."),
                        new(AmShopkeeper, "Very well.", true),
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




        });
    }
}
