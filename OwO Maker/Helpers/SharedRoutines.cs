using OwOMaker.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using static OwO_Maker.Helpers.ButtonResolutionHelper;
using static OwO_Maker.Helpers.Structs;

namespace OwO_Maker.Helpers
{
    public static class SharedRoutines
    {
        public static bool CalculateFailChance(int failChance, double rand)
        {
            double chance = failChance / 100.0;

            return chance > rand;
        }

        public static async Task CollectReward(Mem mem, IntPtr TMiniGamePoints, int playedGames, int Amount, IntPtr hWnd, ButtonResolution buttons, int Level)
        {
            await BackgroundHelper.SendClick(hWnd, buttons.RewardButton.X, buttons.RewardButton.Y, 250);
            await Task.Delay(500 + new Random().Next(0, 100));
            await BackgroundHelper.SendClick(hWnd, buttons.LevelButtons[Level - 1].X, buttons.LevelButtons[Level - 1].Y, 250);
            await Task.Delay(500 + new Random().Next(0, 100));

            // Reward Coupon
            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RETURN, 250);
            await Task.Delay(500 + new Random().Next(0, 100));

            if (mem.ReadMemory<int>(TMiniGamePoints + Structs.TMiniGamePoints.ProductionPoints) >= 100 && playedGames < Amount)
            {
                await BackgroundHelper.SendClick(hWnd, buttons.TryAgain.X, buttons.TryAgain.Y, 250);
                await Task.Delay(500 + new Random().Next(0, 100));

                await BackgroundHelper.SendClick(hWnd, buttons.GameStart.X, buttons.GameStart.Y, 250);
                await Task.Delay(1_500 + new Random().Next(0, 100));
            }
        }

        public static async Task FailTryAgain(IntPtr hWnd, ButtonResolution buttons)
        {
            await BackgroundHelper.SendClick(hWnd, buttons.FailedTryAgain.X, buttons.FailedTryAgain.Y, 250);
            await Task.Delay(1000 + new Random().Next(0, 100));
            await BackgroundHelper.SendClick(hWnd, buttons.GameStart.X, buttons.GameStart.Y, 250);
            await Task.Delay(1000 + new Random().Next(0, 100));
        }

        // TODO check MinigameID to ensure we are going into the right Minigame
        // as for now the first arrow will be clicked clockwise
        // List<Point> contains all visible arrows
        // simple loop of entering game, checking MinigameID, if wrong return and continue
        public static async Task EnterMinigame(Mem mem, IntPtr hWnd, List<Point?> arrow, ButtonResolution buttons)
        {
            await BackgroundHelper.SendClick(hWnd, arrow[0].Value.X, arrow[0].Value.Y, 250);
            await Task.Delay(500 + new Random().Next(0, 100));
            await BackgroundHelper.SendClick(hWnd, arrow[0].Value.X, arrow[0].Value.Y + 40, 250);
            await Task.Delay(500 + new Random().Next(0, 100));
            await BackgroundHelper.SendClick(hWnd, buttons.StartMinigame.X, buttons.StartMinigame.Y, 250);
            await Task.Delay(500 + new Random().Next(0, 100));
        }

        public static async Task UseProductionCoupon(IntPtr hWnd, ButtonResolution buttons, uint ProductionsCouponKey, bool ExitGame)
        {
            if (ExitGame)
                await BackgroundHelper.SendClick(hWnd, buttons.EndMinigame.X, buttons.EndMinigame.Y, 500);

            await Task.Delay(1_500 + new Random().Next(0, 100));
            await BackgroundHelper.SendKey(hWnd, (BackgroundHelper.KeyCodes)ProductionsCouponKey, 500);
            await Task.Delay(500 + new Random().Next(0, 100));
            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RETURN, 500);
            await Task.Delay(500 + new Random().Next(0, 100));
            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_ESCAPE, 500); // just in case
            await Task.Delay(500 + new Random().Next(0, 100));
        }

        public static Status GetStatus(Mem mem, IntPtr ptr) => (Status)mem.ReadMemory<int>(ptr + FishPond.Status);

        public static int GetRequiredPoints(Minigame minigame, int level)
        {
            return minigame switch
            {
                Minigame.StoneQuarry => level switch
                {
                    1 => 1000,
                    2 => 5000,
                    3 => 8000,
                    4 => 12000,
                    _ => 16000,
                },
                Minigame.SawMill => level switch
                {
                    1 => 1000,
                    2 => 5000,
                    3 => 10000,
                    4 => 14000,
                    _ => 20000,
                },
                Minigame.ShootingRange => level switch
                {
                    1 => 1000,
                    2 => 4000,
                    3 => 8000,
                    4 => 15000,
                    _ => 25000,
                },
                Minigame.FishingPond => level switch
                {
                    1 => 1000,
                    2 => 4000,
                    3 => 8000,
                    4 => 12000,
                    _ => 20000,
                },
                _ => 0,
            };
        }

        public static List<Point?> FindMinigameArrowButton(Mem mem, IntPtr TArrowWidget, ButtonResolution buttons)
        {
            var result = new List<Point?>();

            for (var i = 0; i < 8; i++)
                if (mem.ReadMemory<byte>(TArrowWidget + Structs.TArrowWidget.ArrowJmp1, [Structs.TArrowWidget.ArrowJmp2 + (i * Structs.TArrowWidget.GapSize)]) == 1)
                    result.Add(buttons.MinigameArrows[i]);

            if (result.Count > 0)
                return result;

            return null;
        }

        public static bool IsMinigameUseWidgetVisible(Mem mem, IntPtr TArrowWidget)
        {
            return mem.ReadMemory<byte>(TArrowWidget + Structs.TArrowWidget.ArrowJmp1, [Structs.TArrowWidget.ArrowJmp2 + (8 * Structs.TArrowWidget.GapSize)]) == 1;
        }
    }
}
