/*
Copyright (c) 2021 Riley Mullett

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
using System.IO;
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
            "TRUST EXERCISE\n\nAll players wear GoPros.\nNo lights and no torches (One UV torch permitted).\nThe special player stays in the van, and guides players through the house over radio, using the map in the van.\nThe other players cannot return to the truck after they’ve entered.\nRecommend bring equipment to the door of the building.",
            "RADIO SILENCE\n\nOnce you have entered the building it's as if a radio jammer has come on.\nYou can communicate as normal but only locally. Only people OUTSIDE may use radio.\nBest On Larger Maps.",
            "LIGHTS OUT\n\nYou may only use candles and lighters.\nNo house lights or torches.\nOne UV torch is permitted.",
            "ANTI VAX\n\nNo sanity pills allowed.\nCannot leave until 3 pieces of evidence are confirmed.\nThe power cannot be turned on, it causes autism",
            "APARTMENT BLOCK\n\nYou all live in a complex and have your own rooms.\nYou may bring whatever you want but it is not to be shared, you aren't frindly neighbors.\nWithout the use of a thermometer everyone picks their own rooms to investigate and is not allowed to stray from them.\nBest on small maps.",
            "THE TALLOCAUST\n\nYou are all short detectives, you must always be crouched.\nNo exceptions, even during hunts.",
            "CAMERA MEN\n\nThe shop was all out of video cameras, you could only buy GoPros.\nIf you wish to get proof of ghost orbs you must use a player as a cameraman\nOnce positioned, the cameraman is not allowed to move no matter what phase the ghost is in.\nThe cameraman is released from duty only by their peers.",
            "WOMEN'S JEANS\n\nYou can only carry one item.\nThis means you may not have space for a torch.",
            "BUDGET CUTS\n\nYour ghost hunt show has hit a rough patch, your producer has run out of money.\nYou can each only bring one item and there are to be no repeated items.\nUnion healthcare permits one container of sanity pills each.",
            "SPEC OPS\n\nYou can only use the radio after spelling your name out in the NATO phonetic alphabet.\nIf you want to talk to another player specifically, you must spell out their name as well.\nName shortenings allowed (for names with more than 1 syllable)",
            "DARK MODE\n\nNo lights.\nNo exceptions.",
            "LONERS\n\nAfter first entering the house, no player may enter a room which another player is occupying.\nNo exceptions, even in a hunt. The van counts as a room.",
            "HAIKU\n\n ---Speak using haiku---\n-Pattern is five seven five-\n ---Example as shown---\n[A haiku is a poem consisting of three lines, the first line containing five syllables, the second seven and the third contianing five again.]",
            "LOST IN TRANSLATION\n\nPlayers may need to write this one down.\nPlayers may only refer to items by the following codewords:\nEMF Reader = The Pager\nSpirit Box = Steven Hawking\nThermometer = The Coziness Checker\nGhost Writing Book = The Homework",
            "'W'\n\nPlayers must always move.\nThey may never let go of the forwards control.\nNo exceptions.",
            "BROKEN TROLLEY WHEEL\n\nPlayers may only ever turn left.\nIf a player needs to turn right, they should turn left until facing the correct direction.",
            "THAT HEALTHY GLOW\n\nNo torches.\nAll players carry a glowstick.",
            "SUPER SMUDGE BROS\n\nAll players enter carrying a thermometer, a lighter, and a smudge stick.\nUpon discovery of the room, all players light up their smudge sticks.\n[Insert your own 420 joke here]",
            "BLAIR WITCH\n\nEveryone must have at least one camera type on them at all times (Head cameras do not count).\nEvery time a hunting phase starts no matter where in the building you are you must turn to the closest wall and stare at it until the phase it over\nWhen a player is killed you must enter every room on the map calling out the ghost's name until the body is found. (best played on smaller maps)",
            "THREE POINT SIX ROENTGEN\n\nOnly one EMF reader is allowed, and is the only item held by the special player (they may also have a flashlight)\nIf the EMF reader displays anything ABOVE level 3, all players must evacuate the building.\nOnce all players are outside the search may resume.",
            "FREEZE!\n\nDuring a hunt, players may not move\nNo exceptions.",
            "TWO MINUTE MADNESS\n\nAll players must wear GoPros.\nOnly one player is allowed inside at a time.\nEvery two minutes the player MUST leave and swaps with another of their choosing.\nWhile inside a player cannot return for items until their time is up.\nBest on small maps",
            "SPIRIT BOX STEVE\n\nThe special player is spirit box steve. \nThey can only communicate to other players using the following words\nKill, Death, Die, Hate, Leave, Hurt, Attack, Catch, Here, Close, Next, Behind, Away, Far, Child, Kid, Adult, Old.\nThey may wish to write these down somewhere.",
            "THE DARK ONE\n\nThe special player is the dark one.\nThey will turn off the lights of any room they enter, and any players who can see them or are near them must turn off their flashlights.\nIf the dark one dies, the player who first spots their corpse becomes the new dark one.",
            "THAT SINKING FEELING\n\nAll sinks must be turned on before investigation can begin.\nPlayers must only carry a torch until this is done.\nUpon finishing, all sinks must be turned off and photographed, regardless of if there is dirty water.\n(Bring at least one photo camera to set aside for this task)",
            "PROP HUNT\n\nThe special player is the saboteur\nThey must scatter the default items around the map, hiding them before other players may enter.\nNo additional default items may be brought along.\nAfter this, the saboteur becomes a team player again but will only help if given a found item by another player.",
            "THICK AS THEIVES\n\nAll players enter the site with a flashlight only (any kind).\nThey may not begin searching for the ghost until every collectable electronic device is stolen.\nStolen devices must be placed in the van.\nWorks best on house maps",
            "DOOR DADDY\n\nThe special player is the 'Door Daddy'.\nThey cannot assist in the search and must try to close the door on every other player, to prevent them from having escape paths.\nThey may also hold doors shut against players in a hunt phase.\nIf a player says \"Door Daddy, Door Daddy, leave me be!\" the Door Daddy must open the door.",
            "EMP!\n\nWhat happened to the power, did a nuke go off in the upper atmosphere?\nPower is off in the house and use of any electronic devices apart from normal flashlights and a single EMF reader is prohibited.",
            "BFFL\n\nYou buddy up with one other player, making two teams of two.\nYou may only communicate to this one other player, and must avoid the other two.\nIf your buddy dies, you can no longer assist in the investigation and must weep at their corpse.\nIf each pair loses a buddy, they may join together. ",
            "COME ON IN!\n\nYou sure are a welcoming bunch.\nOnce a door is open you are not allowed to close it for any reason.\nEver! No exceptions...",
            "YALL NEED JESUS\n\nAll players must remain in the same room as a crucifix at all times.\nTwo players must carry the crucifixes.\nThey may use other items but must hold the crucifix during hunt phases.",
            "SUPERMODEL\n\nThe special player is the supermodel.\nAll other players carry cameras.\nOther players may only use their cameras if the supermodel is in frame.\nNo exceptions.",
            "NYCTOPHOBIA\n\nYou sleep with a nightlight despite your age.\nEvery room you enter you must turn the light on and any electronic device in the room.\nif the power goes out at least two players must go together to turn the power back on.",
            "ENEMY OF MY ENEMY\n\nWhile alive one of your friends (the special player) is in league with the ghost, their job is to try and sabotage the investigation however they can.\nBUT, during an active phase they aren't allowed to hide, why would they the ghost is their friend?\nIf they are killed they must pile as many objects as they can where the ghosts spawns to assist with the investigation.",
            "THE ARMOURER\n\nThe special player is the armourer. The armourer plays as normal (does not just stay in van).\nThey are the only people who may remove items from the van, and give only two items at a time to other players.\nIf a player wishes to take additional items, they must return their equipment to the armourer.\nConsumed items must also be returned.\nIf a player dies, the armourer must collect their dropped gear themselves.\nIf the armourer dies, no additional items may be taken.",
        };
        readonly List<String> shuffledlist = new List<String>();
        public MainWindow()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ExtraStrats.txt");
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string strat = line.Replace('|', '\n');
                    stratlist.Add(strat);
                }
            }
            catch { }
            rand = new Random(Convert.ToInt32(DateTime.Now.Second * DateTime.Now.Millisecond));
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
            specialplayer.Text = "\n\nIf the above strat calls for a special player, that player is number " + newpickedplayer.ToString();
            lastpickedplayer = newpickedplayer;
        }
    }
}
