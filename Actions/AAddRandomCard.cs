using EvilCat.Actions;
using EvilCat.External;
using EvilCat.Features;
using FSPRO;
using HarmonyLib;
using JetBrains.Annotations;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace EvilCat.Actions;



public class AAddRandomCard : CardAction
{
    //public Card ?card;

    public Deck deck;

    public CardDestination destination;

    public int amount = 1;

    public bool insertRandomly = true;

    public bool showCardTraitTooltips;

    public int? handPosition;

    public bool callItTheDeckNotTheDrawPile;

    public double waitBeforeMoving = 0.4;

    public override void Begin(G g, State s, Combat c)
    {

        Card card = GenerateRandomCard(s, deck, true);

        if (destination != CardDestination.Hand && s.storyVars.hasAddedCardThisRun.Add(card!.Key()))
        {
            waitBeforeMoving = 1.2;
        }

        timer = waitBeforeMoving + 0.2;
        if (s.route is Combat)
        {
            card!.pos = new Vec(G.screenSize.x * 0.5 - 30.0, 30.0);
            card.waitBeforeMoving = waitBeforeMoving;
            card.drawAnim = 1.0;
            foreach (Artifact item in g.state.EnumerateAllArtifacts())
            {
                item.OnPlayerRecieveCardMidCombat(g.state, c, card);
            }
        }

        s.OnHasCard(card!);
        switch (destination)
        {
            case CardDestination.Deck:
                s.SendCardToDeck(card!, doAnimation: true, insertRandomly);
                break;
            case CardDestination.Hand:
                c?.SendCardToHand(s, card!, handPosition);
                break;
            case CardDestination.Discard:
                c?.SendCardToDiscard(s, card!);
                break;
            case CardDestination.Exhaust:
                c?.SendCardToExhaust(s, card!);
                break;
        }

        Audio.Play(Event.CardHandling);
        s.DebugSafeIdCheck();
        if (amount > 1)
        {
            amount--;
            s.GetCurrentQueue().QueueImmediate(new AAddCard
            {
                card = card!.CopyWithNewId(),
                destination = destination,
                amount = amount,
                insertRandomly = insertRandomly
            });
        }
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(StableSpr.icons_addCard, amount, Colors.textMain);
    }


    public static Card GenerateRandomCard(State s, Deck? limitDeck = null, bool makeCardsTemporary = false) //bool inCombat = false, bool isEvent = false)
    {
        Rand rng = s.rngCardOfferingsMidcombat;
        List<Deck?> list = s.characters.Select((Character c) => c.deckType).ToList();

        int count = 1;

        bool deservesToGetWeirdCards = true;
        List<Card> list2 = new List<Card>();
        int num = 100;
        int num2 = 0;
        while (list2.Count < count && num2++ < num)
        {
            //Deck? deck = limitDeck ?? list.Random(rng);
            Deck? deck = list.Random(rng);
            Rarity rarity = GetTrueRandomRarity(rng);
            List<Card> list3 = DB.releasedCards.Where(delegate (Card c)
            {
                CardMeta meta = c.GetMeta();
                if (meta.rarity != rarity)
                {
                    return false;
                }

                if (deck != meta.deck)
                {
                    return false;
                }

                if (meta.dontOffer)
                {
                    return false;
                }

                if (meta.unreleased)
                {
                    return false;
                }

                return (!meta.weirdCard || deservesToGetWeirdCards) ? true : false;
            }).ToList();
            if (list3.Count() != 0)
            {
                Card card = (Card)Activator.CreateInstance(list3.Random(rng).GetType())!;
                card.drawAnim = 1.0;
                if (!list2.Any((Card c) => c.GetType() == card.GetType()))
                {
                    card.upgrade = Upgrade.None;
                    card.flipAnim = 1.0;
                    list2.Add(card);
                }
            }
        }

        if (makeCardsTemporary)
        {
            foreach (Card item2 in list2)
            {
                item2.temporaryOverride = true;
            }
        }

        return list2.ToList()[0];
    }


    public static Rarity GetTrueRandomRarity(Rand rng)
    {

        return Mutil.Roll(rng.Next(), (chance: 0.33, val: Rarity.common), (0.34, Rarity.uncommon), (0.33, Rarity.rare));
    }


}