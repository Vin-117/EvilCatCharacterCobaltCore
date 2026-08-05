using FMOD;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using static EvilCat.Conversation.CommonDefinitions;
using EvilCat.External;

namespace EvilCat.Conversation;

internal class EvilCatStoryDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {

            {
                "EvilCat_Intro_0", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = true,
                    pax = false,
                    allPresent = [ AmEvilCat ],
                    bgSetup = [ "finale_intro"],
                    bg = "BGColdStart",
                    dialogue =
                    [   
                        new(new WaitForBG{ }),
                        new(new SetBG{ bg = "BGRunStart"}),
                        new(new Wait{ secs = 1 }),
                        new(AmCat, "transition", "<c=ff96f3>Hello?</c>"),
                        new(new Wait{ secs = 2 }),
                        new(AmCat, "worried", "...Hello?"),
                        new(new Wait{ secs = 2 }),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Hello.</c>", flipped: true),
                        new(AmCat, "worried", "..."),
                        new(AmCat, "worried", "...You...you're..."),
                        new(AmEvilCat, "smug", "<c=ff96f3>Me.</c>", flipped: true),
                        new(AmCat, "worried", "H-how?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>We won.</c>", flipped: true),
                        new(AmCat, "worried", "We...won?"),
                        new(AmCat, "worried", "Then...why are we still here?"),
                        new(AmCat, "worried", "Why are you here?"),
                        new(AmEvilCat, "smug", "<c=ff96f3>Time is a flat circle.</c>", flipped: true),
                        new(AmCat, "worried", "...What?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>I can't say more.</c>", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>But you will understand, soon.</c>", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Just know that I am here to help.</c>", flipped: true),
                        new(AmCat, "intense", "...Okay?"),
                    ]
                }
            },

            {
                "EvilCat_Intro_1", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = true,
                    requiredScenes = ["EvilCat_Intro_0"],
                    allPresent = [ AmEvilCat ],
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(AmCat, "worried", "CAT?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Yes?</c>", flipped: true),
                        new(AmCat, "worried", "Isn't this a paradox?"),
                        new(AmEvilCat, "smug", "<c=ff96f3>Absolutely.</c>", flipped: true),
                        new(AmCat, "worried", "...Why hasn't the timestream collapsed?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>These loops are already a paradox.</c>", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Timelines rely on information they shouldn't have.</c>", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Memories that don't exist.</c>", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Things that never happened.</c>", flipped: true),
                        new(AmCat, "intense", "That sounds...confusing."),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Don't worry about it too much.</c>", flipped: true),
                        new(AmEvilCat, "smug", "<c=ff96f3>We've already won, after all.</c>", flipped: true),
                    ]
                }
            },
            {
                "EvilCat_Cat_Intro_0", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = false,
                    requiredScenes = ["EvilCat_Intro_1"],
                    allPresent = [ AmEvilCat, AmCat ],
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(AmCat, "neutral", "CAT?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Yes?</c>", flipped: true),
                        new(AmCat, "neutral", "How are you interfacing with the ship?"),
                        new(AmCat, "neutral", "I don't see you anywhere."),
                        new(AmEvilCat, "smug", "<c=ff96f3>Of course you wouldn't.</c>", flipped: true),
                        new(AmEvilCat, "feral", "<c=ff96f3>Because I'm always right behind you.</c>"),
                        new(AmCat, "intense", "AH!", flipped: true),
                        new(AmEvilCat, "smartass", "<c=ff96f3>Heh. Got you.</c>"),
                        new(AmCat, "grumpy", "Was that necessary?", flipped: true),
                        new(AmEvilCat, "smug", "<c=ff96f3>Totally.</c>"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Pranks aside, I'm not a real program.</c>"),
                        new(AmEvilCat, "smug", "<c=ff96f3>I'm just pretending to be one.</c>"),
                        new(AmCat, "worried", "...What?", flipped: true),
                        new(AmCat, "worried", "Are you saying you're not real?", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Oh, I'm absolutely real.</c>"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Just not in a form you can perceive.</c>"),
                        new(AmCat, "intense", "Huh...", flipped: true),
                    ]
                }
            },




        });
    }
}
