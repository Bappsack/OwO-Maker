using OwO_Maker.Helpers;
using OwOMaker.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwO_Maker.Minigames
{
    class StoneQuarry
    {
        private bool IsGameFinished = false;
        private int playedGames = 0;

        Mem mem = new Mem();

        public async void RunTask(IntPtr hWnd, int Amount, ButtonResolutionHelper.ButtonResolution buttons, int BotID, int level, bool HumanTime, bool UseProdCoupon, int FailChance, uint ProductionsCouponKey)
        {
            var proc = mem.FindProcessByHandle(hWnd);

            mem.Init(proc);

            var TMinigameManger = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TMiniGameManager) + 1, [0x0]);
            var TMiniGamePoints = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TMiniGamePoints) + 1, [0x0]);
            var TArrowWidget = mem.ReadMemory<IntPtr>(mem.FindPattern(Structs.Pattern.TArrowWidget) + 1, [0x0]);

            var requiredPoints = SharedRoutines.GetRequiredPoints(Structs.Minigame.StoneQuarry, level);

            var Fail = SharedRoutines.CalculateFailChance(FailChance, new Random().NextDouble());

            if (FailChance <= 0)
                Fail = false;

            if (TMinigameManger is 0 || TMiniGamePoints is 0 || TArrowWidget is 0)
            {
                Program.form.RemoveBotFromList(BotID);
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

                if (mem.ReadMemory<bool>(currentMiniGame + Structs.StoneQuarry.IsVisble) && m_iCurrentMiniGame is (int)Structs.Minigame.StoneQuarry) // only process if we are on the right minigame
                {
                    var hp = mem.ReadMemory<byte>(currentMiniGame + Structs.StoneQuarry.HP);
                    var points = mem.ReadMemory<ushort>(currentMiniGame + Structs.StoneQuarry.Points);

                    var yellowWigglerrightData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler1_Left, [Structs.TimingShotGame.Data, 0x0], 300);
                    var greenWigglerrightData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler2_Left, [Structs.TimingShotGame.Data, 0x0], 300);
                    var redWigglerrightData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler3_Left, [Structs.TimingShotGame.Data, 0x0], 300);
                    var blueWigglerrightData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler4_Left, [Structs.TimingShotGame.Data, 0x0], 300);
                    var purpleWigglerrightData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler5_Left, [Structs.TimingShotGame.Data, 0x0], 300);

                    var yellowWigglerleftData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler1_Right, [Structs.TimingShotGame.Data, 0x0], 300);
                    var greenWigglerleftData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler2_Right, [Structs.TimingShotGame.Data, 0x0], 300);
                    var redWigglerleftData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler3_Right, [Structs.TimingShotGame.Data, 0x0], 300);
                    var blueWigglerleftData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler4_Right, [Structs.TimingShotGame.Data, 0x0], 300);
                    var purpleWigglerleftData = mem.ReadMemoryData(currentMiniGame + Structs.StoneQuarry.Wiggler5_Right, [Structs.TimingShotGame.Data, 0x0], 300);

                    var firstHitBox = mem.ReadMemory<int>(currentMiniGame + Structs.StoneQuarry.Wiggler1_Left, [Structs.TimingShotGame.Hitbox, 0x0]);

                    var status = SharedRoutines.GetStatus(mem, currentMiniGame);

                    Program.form.UpdateStatus(BotID, "StoneQuarry", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");

                    if (status is Structs.Status.Playing)
                    {
                        if (Fail && points >= requiredPoints / 2)
                        {
                            await Task.Delay(100);
                            continue;
                        }

                        if (points < requiredPoints)
                        {
                            for (nint i = 238; i != (238 - firstHitBox); i--)
                            {
                                if (yellowWigglerleftData[i] != 0 || greenWigglerleftData[i] != 0 || redWigglerleftData[i] != 0 || blueWigglerleftData[i] != 0 || purpleWigglerleftData[i] != 0)
                                {
                                    await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_LEFT, 0);
                                    break;
                                }

                                else if (yellowWigglerrightData[i] != 0 || greenWigglerrightData[i] != 0 || redWigglerrightData[i] != 0 || blueWigglerrightData[i] != 0 || purpleWigglerrightData[i] != 0)
                                {
                                    await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RIGHT, 0);
                                    break;
                                }

                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_UP, 0);
                                await Task.Delay(2);
                            }
                        }
                    }
                    else
                    {
                        if (FailChance > 0)
                            Fail = SharedRoutines.CalculateFailChance(FailChance, new Random().NextDouble());

                        await Task.Delay(1_000 + new Random().Next(0, 100));

                        if (points >= requiredPoints && status is Structs.Status.GameEnd or Structs.Status.GameEnded1 or Structs.Status.GameEnded2)
                        {
                            await SharedRoutines.CollectReward(mem, TMiniGamePoints, playedGames + 1, Amount, hWnd, buttons, level);
                            playedGames++;
                        }
                        else
                            await SharedRoutines.FailTryAgain(hWnd, buttons);

                        if (playedGames >= Amount)
                        {
                            Program.form.UpdateStatus(BotID, "StoneQuarry", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");
                            Program.form.RemoveBotFromList(BotID);
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
                                    Program.form.RemoveBotFromList(BotID);
                                    MessageBox.Show($"Bot: {BotID} Failed to use Productions Coupon!\n\nWrong Key selected?\nNo Item in selected Slot?\nEmpty Coupons?\n\n {productionPoints.ToString()} != {mem.ReadMemory<int>(TMiniGamePoints + 0xC8).ToString()}");
                                    return;
                                }
                            }
                            else
                            {
                                Program.form.RemoveBotFromList(BotID);
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
                                Program.form.RemoveBotFromList(BotID);
                                MessageBox.Show($"Bot: {BotID} Failed to use Productions Coupon!\n\nWrong Key selected?\nNo Item in selected Slot?\nEmpty Coupons?\n\n {productionPoints.ToString()} != {mem.ReadMemory<int>(TMiniGamePoints + 0xC8).ToString()}");
                                return;
                            }
                        }
                        else
                        {
                            Program.form.RemoveBotFromList(BotID);
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
                        Program.form.RemoveBotFromList(BotID);
                        MessageBox.Show($"Bot: {BotID} Failed to open Minigame, Abort!");
                        return;
                    }
                }
                await Task.Delay(5);
            }
        }
    }
}
