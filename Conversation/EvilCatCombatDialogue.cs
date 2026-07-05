using FMOD;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using static EvilCat.Conversation.CommonDefinitions;
using EvilCat.External;

namespace EvilCat.Conversation;

internal class EvilCatCombatDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {
                "EvilCat_Max_Meme", new()
                {
                    type = NodeType.combat,
                    lookup = [ "EvilCatMeme" ],
                    oncePerRun = true,
                    allPresent = [ AmEvilCat, AmMax ],
                    dialogue =
                    [
                        new(AmMax, "squint", "Something feels...off.")
                    ]
                }
            },

        });
    }
}
