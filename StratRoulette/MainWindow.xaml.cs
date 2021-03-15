/*
Copyright (c) 2021 StarchOnSteam

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/


/*
Basic WPF app which will shuffle given strategies and display them.
Last updated 15th March 2021
Send all issues to my github (StarchOnSteam)
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Strat_Roulette 
{
    public partial class MainWindow : Window
    {
        int j = 0;
        int lastpickedplayer = 0;
        int newpickedplayer = 0;
        readonly Random rand;
        readonly List<String> stratlist = new List<String>
        {
            "TRUST EXERCISE\r\n\r\nAll players wear GoPros.\r\nNo lights and no torches (One UV torch permitted).\r\nThe special player stays in the van, and guides players through the house over radio, using the map in the van.\r\nThe other players cannot return to the truck after they’ve entered.\r\nRecommend bring equipment to the door of the building.",
            "RADIO SILENCE\r\n\r\nOnce you have entered the building it's as if a radio jammer has come on.\r\nYou can communicate as normal but only locally. Only people OUTSIDE may use radio.\r\nBest On Larger Maps.",
            "LIGHTS OUT\r\n\r\nYou may only use candles and lighters.\r\nNo house lights or torches.\r\nOne UV torch is permitted.",
            "ANTI VAX\r\n\r\nNo sanity pills allowed.\r\nCannot leave until 3 pieces of evidence are confirmed.\r\nThe power cannot be turned on, it causes autism",
            "APARTMENT BLOCK\r\n\r\nYou all live in a complex and have your own rooms.\r\nYou may bring whatever you want but it is not to be shared, you aren't frindly neighbors.\r\nWithout the use of a thermometer everyone picks their own rooms to investigate and is not allowed to stray from them.\r\nBest on small maps.",
            "THE TALLOCAUST\r\n\r\nYou are all short detectives, you must always be crouched.\r\nNo exceptions, even during hunts.",
            "CAMERA MEN\r\n\r\nThe shop was all out of video cameras, you could only buy GoPros.\r\nIf you wish to get proof of ghost orbs you must use a player as a cameraman\r\nOnce positioned, the cameraman is not allowed to move no matter what phase the ghost is in.\r\nThe cameraman is released from duty only by their peers.",
            "WOMEN'S JEANS\r\n\r\nYou can only carry one item.\r\nThis means you may not have space for a torch.",
            "BUDGET CUTS\r\n\r\nYour ghost hunt show has hit a rough patch, your producer has run out of money.\r\nYou can each only bring one item and there are to be no repeated items.\r\nUnion healthcare permits one container of sanity pills each.",
            "SPEC OPS\r\n\r\nYou can only use the radio after spelling your name out in the NATO phonetic alphabet.\r\nIf you want to talk to another player specifically, you must spell out their name as well.\r\nName shortenings allowed (for names with more than 1 syllable)",
            "DARK MODE\r\n\r\nNo lights.\r\nNo exceptions.",
            "LONERS\r\n\r\nAfter first entering the house, no player may enter a room which another player is occupying.\r\nNo exceptions, even in a hunt. The van counts as a room.",
            "HAIKU\r\n\r\n ---Speak using haiku---\r\n-Pattern is five seven five-\r\n ---Example as shown---\r\n[A haiku is a poem consisting of three lines, the first line containing five syllables, the second seven and the third contianing five again.]",
            "LOST IN TRANSLATION\r\n\r\nPlayers may need to write this one down.\r\nPlayers may only refer to items by the following codewords:\r\nEMF Reader = The Pager\r\nSpirit Box = Steven Hawking\r\nThermometer = The Coziness Checker\r\nGhost Writing Book = The Homework",
            "'W'\r\n\r\nPlayers must always move.\r\nThey may never let go of the forwards control.\r\nNo exceptions.",
            "BROKEN TROLLEY WHEEL\r\n\r\nPlayers may only ever turn left.\r\nIf a player needs to turn right, they should turn left until facing the correct direction.",
            "THAT HEALTHY GLOW\r\n\r\nNo torches.\r\nAll players carry a glowstick.",
            "SUPER SMUDGE BROS\r\n\r\nAll players enter carrying a thermometer, a lighter, and a smudge stick.\r\nUpon discovery of the room, all players light up their smudge sticks.\r\n[Insert your own 420 joke here]",
            "BLAIR WITCH\r\n\r\nEveryone must have at least one camera type on them at all times (Head cameras do not count).\r\nEvery time a hunting phase starts no matter where in the building you are you must turn to the closest wall and stare at it until the phase it over\r\nWhen a player is killed you must enter every room on the map calling out the ghost's name until the body is found. (best played on smaller maps)",
            "THREE POINT SIX ROENTGEN\r\n\r\nOnly one EMF reader is allowed, and is the only item held by the special player (they may also have a flashlight)\r\nIf the EMF reader displays anything ABOVE level 3, all players must evacuate the building.\r\nOnce all players are outside the search may resume.",
            "FREEZE!\r\n\r\nDuring a hunt, players may not move\r\nNo exceptions.",
            "TWO MINUTE MADNESS\r\n\r\nAll players must wear GoPros.\r\nOnly one player is allowed inside at a time.\r\nEvery two minutes the player MUST leave and swaps with another of their choosing.\r\nWhile inside a player cannot return for items until their time is up.\r\nBest on small maps",
            "SPIRIT BOX STEVE\r\n\r\nThe special player is spirit box steve. \r\nThey can only communicate to other players using the following words\r\nKill, Death, Die, Hate, Leave, Hurt, Attack, Catch, Here, Close, Next, Behind, Away, Far, Child, Kid, Adult, Old.\r\nThey may wish to write these down somewhere.",
            "THE DARK ONE\r\n\r\nThe special player is the dark one.\r\nThey will turn off the lights of any room they enter, and any players who can see them or are near them must turn off their flashlights.\r\nIf the dark one dies, the player who first spots their corpse becomes the new dark one.",
            "THAT SINKING FEELING\r\n\r\nAll sinks must be turned on before investigation can begin.\r\nPlayers must only carry a torch until this is done.\r\nUpon finishing, all sinks must be turned off and photographed, regardless of if there is dirty water.\r\n(Bring at least one photo camera to set aside for this task)",
            "PROP HUNT\r\n\r\nThe special player is the saboteur\r\nThey must scatter the default items around the map, hiding them before other players may enter.\r\nNo additional default items may be brought along.\r\nAfter this, the saboteur becomes a team player again but will only help if given a found item by another player.",
            "THICK AS THEIVES\r\n\r\nAll players enter the site with a flashlight only (any kind).\r\nThey may not begin searching for the ghost until every collectable electronic device is stolen.\r\nStolen devices must be placed in the van.\r\nWorks best on house maps",
            "DOOR DADDY\r\n\r\nThe special player is the 'Door Daddy'.\r\nThey cannot assist in the search and must try to close the door on every other player, to prevent them from having escape paths.\r\nThey may also hold doors shut against players in a hunt phase.\r\nIf a player says \"Door Daddy, Door Daddy, leave me be!\" the Door Daddy must open the door.",
            "EMP!\r\n\r\nWhat happened to the power, did a nuke go off in the upper atmosphere?\r\nPower is off in the house and use of any electronic devices apart from normal flashlights and a single EMF reader is prohibited.",
            "BFFL\r\n\r\nYou buddy up with one other player, making two teams of two.\r\nYou may only communicate to this one other player, and must avoid the other two.\r\nIf your buddy dies, you can no longer assist in the investigation and must weep at their corpse.\r\nIf each pair loses a buddy, they may join together. ",
            "COME ON IN!\r\n\r\nYou sure are a welcoming bunch.\r\nOnce a door is open you are not allowed to close it for any reason.\r\nEver! No exceptions...",
            "YALL NEED JESUS\r\n\r\nAll players must remain in the same room as a crucifix at all times.\r\nTwo players must carry the crucifixes.\r\nThey may use other items but must hold the crucifix during hunt phases.",
            "SUPERMODEL\r\n\r\nThe special player is the supermodel.\r\nAll other players carry cameras.\r\nOther players may only use their cameras if the supermodel is in frame.\r\nNo exceptions.",
            "NYCTOPHOBIA\r\n\r\nYou sleep with a nightlight despite your age.\r\nEvery room you enter you must turn the light on and any electronic device in the room.\r\nif the power goes out at least two players must go together to turn the power back on.",
            "ENEMY OF MY ENEMY\r\n\r\nWhile alive one of your friends (the special player) is in league with the ghost, their job is to try and sabotage the investigation however they can.\r\nBUT, during an active phase they aren't allowed to hide, why would they the ghost is their friend?\r\nIf they are killed they must pile as many objects as they can where the ghosts spawns to assist with the investigation.",
            "THE ARMOURER\r\n\r\nThe special player is the armourer. The armourer plays as normal (does not just stay in van).\r\nThey are the only people who may remove items from the van, and give only two items at a time to other players.\r\nIf a player wishes to take additional items, they must return their equipment to the armourer.\r\nConsumed items must also be returned.\r\nIf a player dies, the armourer must collect their dropped gear themselves.\r\nIf the armourer dies, no additional items may be taken.",
        };
        readonly List<String> shuffledlist = new List<String>();
        public MainWindow()
        {
            rand = new Random(Convert.ToInt32(DateTime.Today.Second * DateTime.Today.Millisecond));
            InitializeComponent();
            while(stratlist.Count > 0)
            {
                int element = rand.Next() % stratlist.Count;
                shuffledlist.Add(stratlist.ElementAt(element));
                stratlist.RemoveAt(element);
            }
        }
        private void Reroll_Click(object sender, RoutedEventArgs e)
        {
            if (j >= shuffledlist.Count) j = 0;
            strat.Text = shuffledlist.ElementAt(j);
            j++;
            while (newpickedplayer == lastpickedplayer)
            {
                newpickedplayer = ((rand.Next() % 4) + 1);
            }
            specialplayer.Text = "\r\n\r\nIf the above strat calls for a special player, that player is number " + newpickedplayer.ToString();
            lastpickedplayer = newpickedplayer;
        }
    }
}
