using OwO_Maker.Helpers;
using OwOMaker.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwO_Maker.Minigames
{
    class ShootingRange
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

            var requiredPoints = SharedRoutines.GetRequiredPoints(Structs.Minigame.ShootingRange, level);

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

                if (mem.ReadMemory<bool>(currentMiniGame + Structs.ShootingRange.IsVisible) && m_iCurrentMiniGame is (int)Structs.Minigame.ShootingRange) // only process if we are on the right minigame
                {
                    var hp = mem.ReadMemory<byte>(currentMiniGame + Structs.ShootingRange.HP);
                    var points = mem.ReadMemory<ushort>(currentMiniGame + Structs.ShootingRange.Points);
                    var ammo = mem.ReadMemory<byte>(currentMiniGame + Structs.ShootingRange.Ammo);

                    var leftChickenPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.ChickenLeftPaddle, [Structs.TimingShotGame.Data, 0x0], 500);
                    var rightChickenPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.ChickenRightPaddle, [Structs.TimingShotGame.Data, 0x0], 500);
                    var leftBatPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.BatLeftPaddle, [Structs.TimingShotGame.Data, 0x0], 500);
                    var rightBatPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.BatRightPaddle, [Structs.TimingShotGame.Data, 0x0], 500);
                    var leftRoosterPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.RoosterLeftPaddle, [Structs.TimingShotGame.Data, 0x0], 500);
                    var rightRoosterPaddleData = mem.ReadMemoryData(currentMiniGame + Structs.ShootingRange.RoosterRightPaddle, [Structs.TimingShotGame.Data, 0x0], 500);

                    var firstHitBox = mem.ReadMemory<int>(currentMiniGame + Structs.ShootingRange.ChickenLeftPaddle, [Structs.TimingShotGame.Hitbox, 0x0]);

                    var status = SharedRoutines.GetStatus(mem, currentMiniGame);

                    Program.form.UpdateStatus(BotID, "ShootingRange", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");

                    if (status is Structs.Status.Playing)
                    {
                        if (Fail && points >= requiredPoints / 2)
                        {
                            await Task.Delay(100);
                            continue;
                        }

                        for (nint i = 478; i != (478 - firstHitBox); i--)
                        {
                            if (leftChickenPaddleData[i] > 0 || leftBatPaddleData[i] > 0 || leftRoosterPaddleData[i] > 0)
                            {
                                if (points < requiredPoints)
                                {
                                    await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_LEFT, 0);
                                    await Task.Delay(10);
                                    break;
                                }
                            }

                            if (rightChickenPaddleData[i] > 0 || rightBatPaddleData[i] > 0 || rightRoosterPaddleData[i] > 0)
                            {
                                if (points < requiredPoints)
                                {
                                    await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_RIGHT, 0);
                                    await Task.Delay(10);
                                    break;
                                }
                            }

                            if (ammo <= 0)
                            {
                                await BackgroundHelper.SendKey(hWnd, BackgroundHelper.KeyCodes.VK_UP, 0);
                                await Task.Delay(2);
                                break;
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
                            Program.form.UpdateStatus(BotID, "ShootingRange", level, points, productionPoints, UseProdCoupon, HumanTime, $"{playedGames}/{Amount}");
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
