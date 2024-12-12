using MelonLoader;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TestMod
{
    public static class BuildInfo
    {
        public const string Name = "TestMod"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Mod for Testing"; // Description for the Mod.  (Set as null if none)
        public const string Author = "TestAuthor"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class TestMod : MelonMod
    {
        private static DisplayTimeFlux displayTimeFlux;
        private Rect windowRect = new Rect(100, 100, 450, 450);
        public static bool Test;
        private static string YesNo(bool input) => input ? "Enabled" : "Disabled";
        private static void DrawButton(string label, Action onClick) { if (GUILayout.Button(label, GUILayout.Height(30))) { onClick?.Invoke(); } }
        private static bool DrawToggle(bool toggleValue, string label) { return GUILayout.Toggle(toggleValue, label, GUILayout.Height(30)); }

        public override void OnApplicationStart()
        {
            LoadInstance();
        }
        public static void LoadInstance()
        {
            displayTimeFlux = GameObject.FindObjectOfType<DisplayTimeFlux>();
            MelonLogger.Msg("Loaded Instance");
        }
        public override void OnGUI()
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MakeBackgroundTexture(Color.black, 0.5f));
            windowRect = GUI.Window(0, windowRect, DrawWindow, "Revolution Idle Cheat");
        }
        private void DrawWindow(int id)
        {
            GUILayout.Space(10);

            DrawButton("Quit",                            () => Mods.Quit());
            DrawButton("Give Souls",                      () => Mods.GiveSouls());
            DrawButton("Give Start Pack",                 () => Mods.GiveStartPack());
            DrawButton("Remove Ads",                      () => Mods.RemoveAds());
            DrawButton("Give Max Boost",                  () => Mods.GiveMaxBoost());
            DrawButton("ReSet Max Boost",                 () => Mods.ReSetMaxBoost());
            DrawButton("Unlock All Achievement",          () => Mods.UnlockAllAchievement());
            DrawButton("Give Revolution Score",           () => Mods.GiveRevolutionScore());
            DrawButton("Give Infinity IP",                () => Mods.GiveInfinityIP());
            DrawButton("Test",                            () => Mods.Test());

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
        private Texture2D MakeBackgroundTexture(Color color, float alpha)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color(color.r, color.g, color.b, alpha));
            texture.Apply();
            return texture;
        }
        public class Mods
        {
            public static int MaxBoostMode = 2;
            public static BigDouble ValueOfBigDouble = GameController.data.income * GameController.data.score * GameController.inventory.BoostIncome;
            public static void Quit()
            {
                MelonLogger.Msg("Exiting the game...");
                Application.Quit();
            }
            public static void GiveSouls()
            {
                LoadInstance();
                GameController.inventory.BuySouls(999999999);
            }
            public static void GiveStartPack()
            {
                LoadInstance();
                GameController.inventory.startPack1 = true;
                GameController.inventory.BuySouls(GameController.inventory.StartPack1Souls);
                GameController.inventory.boostStepTFGain = Math.Max(GameController.inventory.boostStepTFGain, 2);
                GameController.inventory.boostStepIncome = Math.Max(GameController.inventory.boostStepIncome, 2);
                GameController.inventory.boostStepMult = Math.Max(GameController.inventory.boostStepMult, 2);
                GameController.inventory.boostStepLaps = Math.Max(GameController.inventory.boostStepLaps, 2);
                if (!GameController.inventory.HasSkin(RevolutionSkin.Orbital))
                {
                    GameController.inventory.skins.Add(RevolutionSkin.Orbital);
                    GameController.data.skin = RevolutionSkin.Orbital;
                }
            }
            public static void RemoveAds()
            {
                LoadInstance();
                GameController.inventory.noAds = true;
            }
            public static void GiveMaxBoost()
            {
                LoadInstance();
                if (MaxBoostMode == 1)
                {
                    GameController.inventory.boostStepTFGain   = Math.Max(GameController.inventory.boostStepTFGain,   999999999);
                    GameController.inventory.boostStepIncome   = Math.Max(GameController.inventory.boostStepIncome,   999999999);
                    GameController.inventory.boostStepMult     = Math.Max(GameController.inventory.boostStepMult,     999999999);
                    GameController.inventory.boostStepLaps     = Math.Max(GameController.inventory.boostStepLaps,     999999999);
                    GameController.inventory.boostAscPower     = Math.Max(GameController.inventory.boostAscPower,     999999999);
                    GameController.inventory.boostStepDPGain   = Math.Max(GameController.inventory.boostStepDPGain,   999999999);
                    GameController.inventory.boostStepEPGain   = Math.Max(GameController.inventory.boostStepEPGain,   999999999);
                    GameController.inventory.boostStepGenMult  = Math.Max(GameController.inventory.boostStepGenMult,  999999999);
                    GameController.inventory.boostStepInfGain  = Math.Max(GameController.inventory.boostStepInfGain,  999999999);
                    GameController.inventory.boostStepIPGain   = Math.Max(GameController.inventory.boostStepIPGain,   999999999);
                    GameController.inventory.boostStepPExp     = Math.Max(GameController.inventory.boostStepPExp,     999999999);
                    GameController.inventory.boostStepPMult    = Math.Max(GameController.inventory.boostStepPMult,    999999999);
                    GameController.inventory.boostStepStardust = Math.Max(GameController.inventory.boostStepStardust, 999999999);
                }
                else if (MaxBoostMode == 2)
                {
                    GameController.inventory.boostStepTFGain   = 999999999;
                    GameController.inventory.boostStepIncome   = 999999999;
                    GameController.inventory.boostStepMult     = 999999999;
                    GameController.inventory.boostStepLaps     = 999999999;
                    GameController.inventory.boostAscPower     = 999999999;
                    GameController.inventory.boostStepDPGain   = 999999999;
                    GameController.inventory.boostStepEPGain   = 999999999;
                    GameController.inventory.boostStepGenMult  = 999999999;
                    GameController.inventory.boostStepInfGain  = 999999999;
                    GameController.inventory.boostStepIPGain   = 999999999;
                    GameController.inventory.boostStepPExp     = 999999999;
                    GameController.inventory.boostStepPMult    = 999999999;
                    GameController.inventory.boostStepStardust = 999999999;
                }
            }
            public static void ReSetMaxBoost()
            {
                LoadInstance();
                GameController.inventory.boostStepTFGain   = 2;
                GameController.inventory.boostStepIncome   = 2;
                GameController.inventory.boostStepMult     = 2;
                GameController.inventory.boostStepLaps     = 2;
                GameController.inventory.boostAscPower     = 2;
                GameController.inventory.boostStepDPGain   = 2;
                GameController.inventory.boostStepEPGain   = 2;
                GameController.inventory.boostStepGenMult  = 2;
                GameController.inventory.boostStepInfGain  = 2;
                GameController.inventory.boostStepIPGain   = 2;
                GameController.inventory.boostStepPExp     = 2;
                GameController.inventory.boostStepPMult    = 2;
                GameController.inventory.boostStepStardust = 2;
            }
            public static void UnlockAllAchievement()
            {
                LoadInstance();
                for (int i = 1; i <= 150; i++)
                {
                    GameController.data.UnlockAchievement(i);
                }
            }
            public static void GiveInfinityIP()
            {
                LoadInstance();
                GameController.data.infinity.IP       += ValueOfBigDouble;
                GameController.data.infinity.infs     += ValueOfBigDouble;
                GameController.data.infinity.genMult  += ValueOfBigDouble;
                GameController.data.infinity.genPower += ValueOfBigDouble;
            }
            public static void GiveRevolutionScore()
            {
                LoadInstance();
                GameController.data.income         += ValueOfBigDouble;
                GameController.data.score          += ValueOfBigDouble;
                GameController.data.scorePromotion += ValueOfBigDouble;
                GameController.data.scoreInfinity  += ValueOfBigDouble;
                GameController.data.scoreEternity  += ValueOfBigDouble;
                GameController.data.scoreUnity     += ValueOfBigDouble;
                GameController.data.scoreEquality  += ValueOfBigDouble;
            }
            public static void Test()
            {
                LoadInstance();
                int num = 999999;
                displayTimeFlux.SetSpeed(num);
                displayTimeFlux.Controller.thisSpeed = num;
            }
            // GameController.data.timeFlux
            // DisplayTimeFlux.SetSpeed
        }
    }
}