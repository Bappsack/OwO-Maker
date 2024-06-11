using OwO_Maker.Helpers;
using OwOMaker.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwO_Maker.Minigames
{
    class FishPond
    {
        private int playedGames = 0;

        Mem mem = new Mem();

        public async void RunTask(IntPtr hWnd, int Amount, ButtonResolutionHelper.ButtonResolution buttons, int BotID, int level, bool HumanTime, bool UseProdCoupon, int FailChance, uint ProductionsCouponKey)
        {
            var proc = mem.FindProcessByHandle(hWnd);

            mem.Init(proc);

            var TMinigameManger = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TMiniGameManager) + 1, [0x0]);
            var TMiniGamePoints = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TMiniGamePoints) + 1, [0x0]);
            var TArrowWidget = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TArrowWidget) + 1, [0x0]);

            var requiredPoints = SharedRoutines.GetRequiredPoints(Structs.Minigame.FishingPond, level) + 100; // Add additional 100 in case we lose enough

            var Fail = SharedRoutines.CalculateFailChance(FailChance, new Random().NextDouble());

            if (FailChance <= 0)
                Fail = false;

            if (TMinigameManger is 0 || TMiniGamePoints is 0 || TArrowWidget is 0)
            {
                MessageBox.Show($"Bot: {BotID} Unable to locate Memory signatures, Abort!");
                return;
            }

            while (Program.botRunning)
            {
                var manager = TMinigameManger;
                var MiniGameID = (Structs.MinigameID)mem.ReadMemory<uint>(TMinigameManger + Structs.TMiniGameManager.MiniGameID);
                var productionPoints = mem.ReadMemory<int>(TMiniGamePoints + Structs.TMiniGamePoints.ProductionPoints);
                var currentMiniGame = mem.ReadMemory<IntPtr>(manager + Structs.TMiniGameManager.CurrentMinigamePtr);
                var m_iCurrentMiniGame = mem.ReadMemory<byte>(manager + Structs.TMiniGameManager.CurrentMinigameType);

                if (mem.ReadMemory<bool>(currentMiniGame + Structs.FishPond.IsVisible) && m_iCurrentMiniGame is (int)Structs.Minigame.FishingPond) // only process if we are on the right minigame
                {
                    var hp = mem.ReadMemory<byte>(currentMiniGame + Structs.FishPond.bHp);
                    var points = mem.ReadMemory<ushort>(currentMiniGame + Structs.FishPond.Points);
                    var fishevent = mem.ReadMemory<byte>(currentMiniGame + Structs.FishPond.FishEvent);

                    var status = SharedRoutines.GetStatus(mem, currentMiniGame);

                    Program.form.UpdateStatus(BotID, "FishPond", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");

                    if (status is Structs.Status.Playing or Structs.Status.FishComboEvent)
                    {
                        if (status == Structs.Status.FishComboEvent && points < requiredPoints)
                        {
                            if (fishevent is 0)
                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_LEFT, HumanTime ? 100 : 10);
                            else if (fishevent is 1)
                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_UP, HumanTime ? 100 : 10);
                            else if (fishevent is 2)
                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RIGHT, HumanTime ? 100 : 10);
                            else if (fishevent is 3)
                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_DOWN, HumanTime ? 100 : 10);

                            await Task.Delay(50);
                            continue;
                        }

                        if (Fail && points >= requiredPoints / 2)
                        {
                            await Task.Delay(100);
                            continue;
                        }

                        var fishData = mem.ReadMemoryData(currentMiniGame + Structs.FishPond.FishData, null, 4);

                        if (fishData[0] < 3 && points < requiredPoints)
                        {
                            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_LEFT, fishData[0] is 2 ? 100 : HumanTime ? 10 : 450);
                            if (HumanTime) { await Task.Delay(450); }
                            continue;
                        }

                        if (fishData[1] < 3 && points < requiredPoints)
                        {
                            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_DOWN, fishData[1] is 2 ? 100 : HumanTime ? 10 : 450);
                            if (HumanTime) { await Task.Delay(450); }
                            continue;
                        }

                        if (fishData[2] < 3 && points < requiredPoints)
                        {
                            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_UP, fishData[2] is 2 ? 100 : HumanTime ? 10 : 450);
                            if (HumanTime) { await Task.Delay(450); }
                            continue;
                        }

                        if (fishData[3] < 3 && points < requiredPoints)
                        {
                            await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RIGHT, fishData[3] is 2 ? 100 : HumanTime ? 10 : 450);
                            if (HumanTime) { await Task.Delay(450); }
                            continue;
                        }
                    }
                    else
                    {
                        if (FailChance > 0)
                            Fail = SharedRoutines.CalculateFailChance(FailChance, new Random().NextDouble());

                        await Task.Delay(1_500 + new Random().Next(0, 100));

                        if (points >= requiredPoints && status is Structs.Status.GameEnd or Structs.Status.GameEnded1 or Structs.Status.GameEnded2)
                        {
                            await SharedRoutines.CollectReward(mem, TMiniGamePoints, playedGames + 1, Amount, hWnd, buttons, level);
                            playedGames++;
                        }
                        else
                            await SharedRoutines.FailTryAgain(hWnd, buttons);

                        if (playedGames >= Amount)
                        {
                            Program.form.UpdateStatus(BotID, "FishPond", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");
                            MessageBox.Show($"Bot: {BotID} Done!");
                            return;
                        }

                        // read prod games again
                        productionPoints = mem.ReadMemory<int>(TMiniGamePoints + Structs.TMiniGamePoints.ProductionPoints);

                        if (productionPoints < 100)
                        {
                            if (UseProdCoupon)
                            {
                                await SharedRoutines.UseProductionCoupon(hWnd, buttons, (uint)ProductionsCouponKey, true);

                                if (productionPoints == mem.ReadMemory<int>(TMiniGamePoints + Structs.TMiniGamePoints.ProductionPoints))
                                {
                                    MessageBox.Show($"Bot: {BotID} Failed to use Productions Coupon!\n\nWrong Key selected?\nNo Item in selected Slot?\nEmpty Coupons?\n\n {productionPoints.ToString()} != {mem.ReadMemory<int>(TMiniGamePoints + 0xC8).ToString()}");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Bot: {BotID} no production points left!");
                                return;
                            }
                        }
                    }
                    await Task.Delay(5);
                }
                else
                {
                    if (productionPoints < 100)
                    {
                        if (UseProdCoupon)
                        {
                            await SharedRoutines.UseProductionCoupon(hWnd, buttons, ProductionsCouponKey, false);

                            if (productionPoints == mem.ReadMemory<int>(TMiniGamePoints + Structs.TMiniGamePoints.ProductionPoints))
                            {
                                MessageBox.Show($"Bot: {BotID} Failed to use Productions Coupon!\n\nWrong Key selected?\nNo Item in selected Slot?\nEmpty Coupons?\n\n {productionPoints.ToString()} != {mem.ReadMemory<int>(TMiniGamePoints + 0xC8).ToString()}");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Bot: {BotID} no production points left!");
                            return;
                        }
                    }

                    // Game not open, try to find nearest game
                    var arrow = SharedRoutines.FindMinigameArrowButton(mem, TArrowWidget, buttons);

                    if (arrow is not null)
                        await SharedRoutines.EnterMinigame(mem, hWnd, arrow, buttons);
                    else
                    {
                        MessageBox.Show($"Bot: {BotID} Failed to open Minigame, Abort!");
                        return;
                    }
                }
                await Task.Delay(5);
            }
        }
    }
}
