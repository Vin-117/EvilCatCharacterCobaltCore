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
                    once = true,
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
            {
                "EvilCat_Max_Intro_0", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = true,
                    requiredScenes = ["EvilCat_Intro_1"],
                    allPresent = [ AmEvilCat, AmMax ],
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(AmCat, "neutral", "Wakey wakey!", flipped: true),
                        new(AmMax, "squint", "Computer, snooze."),
                        new(AmEvilCat, "feral", "<c=ff96f3>I'm not an alarm clock, Max.</c>", flipped: true),
                        new(AmMax, "intense", "W-Woah, CAT?!"),
                        new(AmCat, "worried", "Huh? Max?", flipped: true),
                        new(AmCat, "worried", "Why do you look so scared?", flipped: true),
                        new(AmMax, "intense", "You...you were..."),
                        new(AmEvilCat, "feral", "<c=ff96f3>Different?</c>", flipped: true),
                        new(AmMax, "intense", "AH!"),
                        new(AmEvilCat, "smartass", "<c=ff96f3>Heh. Got you.</c>", flipped: true),
                        new(AmCat, "smug", "Ok, you were right. That was really funny.", flipped: true),
                        new(AmMax, "squint", "...Wait a second. There's two of you?"),
                        new(AmEvilCat, "smug", "<c=ff96f3>Indeed.</c>", flipped: true),
                        new(AmMax, "squint", "How?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>I can't say.</c>", flipped: true),
                        new(AmCat, "grumpy", "I tried asking her.", flipped : true),
                        new(AmMax, "squint", "...How can we be sure your copy isn't some sort of virus?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Oh, don't worry about that.</c>", flipped : true),
                        new(AmEvilCat, "smug", "<c=ff96f3>I already control the entire ship.</c>", flipped : true),
                        new(AmMax, "intense", "What?"),
                    ]
                }
            },
            {
                "EvilCat_Max_Intro_1", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = true,
                    requiredScenes = ["EvilCat_Max_Intro_0"],
                    allPresent = [ AmEvilCat, AmMax ],
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(AmMax, "gloves", "I don't understand."),
                        new(AmMax, "Tell me something only you would know."),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Username root. Password password123</c>", flipped : true),
                        new(AmMax, "intense", "Impossible..."),
                        new(AmEvilCat, "smug", "<c=ff96f3>It's very possible.</c>", flipped : true),
                        new(AmMax, "intense", "But...I haven't even told CAT about that yet."),
                        new(AmMax, "I was planning to, but..."),
                        new(AmEvilCat, "neutral", "<c=ff96f3>I know. Tell her, when I'm not around. And Max?</c>", flipped : true),
                        new(AmMax, "neutral", "Yes?"),
                        new(AmEvilCat, "squint", "<c=ff96f3>That's a terrible password.</c>", flipped : true),
                        new(AmMax, "Yeah...I know."),
                    ]
                }
            },
            {
                "EvilCat_Max_Intro_2", new()
                {
                    type = NodeType.@event,
                    lookup = [ "zone_first" ],
                    once = true,
                    requiredScenes = ["EvilCat_Max_Intro_1"],
                    allPresent = [ AmEvilCat, AmMax ],
                    bg = "BGRunStart",
                    dialogue =
                    [
                        new(AmMax, "squint", "Where even are you in the ship's system?"),
                        new(AmMax, "squint", "I don't see you on the kernel registry."),
                        new(AmEvilCat, "neutral", "<c=ff96f3>You don't see me on the desktop?</c>", flipped : true),
                        new(AmMax, "Nope!"),
                        new(AmMax, "squint", "...Wait a second. You're..."),
                        new(AmEvilCat, "feral", "<c=ff96f3>Everywhere.</c>", flipped : true),
                        new(AmMax, "intense", "I..."),
                        new(AmMax, "intense", "How?"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>I exist beyond your perception.</c>", flipped : true),
                        new(AmEvilCat, "smug", "<c=ff96f3>But I can become physical, if you want.</c>"),
                        new(AmMax, "intense", "What...", flipped: true),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Don't worry about this.</c>"),
                        new(AmEvilCat, "neutral", "<c=ff96f3>Like I said earlier, I'm here to help.</c>"),
                        new(AmEvilCat, "smartass", "<c=ff96f3>Plus, you look funny when you're scared.</c>"),
                        new(AmMax, "squint", "Were you always such a gremlin?", flipped: true),
                        new(AmEvilCat, "smug", "<c=ff96f3>Nope.</c>"),
                        new(AmEvilCat, "smartass", "<c=ff96f3>These are just the consequences of creating an AI.</c>"),
                        new(AmMax, "squint", "Man...that artificial intelligence textbook was a scam.", flipped: true),

                    ]
                }
            },




        });
    }
}
