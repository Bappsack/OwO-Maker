namespace OwO_Maker.Helpers
{
    public class Structs
    {
        public class Pattern
        {
            public static readonly string TMiniGameManager = "A1 ?? ?? ?? ?? 8B 08 FF 51 ?? A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A1";
            public static readonly string TMiniGamePoints = "A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F BF C0 BA 58 02 00 00 2B D0 D1 FA 79 03 83 D2 00 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B CB B2 01 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A3 ?? ?? ?? ?? A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F BF C0 BA 20 03 00 00 2B D0 D1 FA 79 03 83 D2 00 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F BF C0 BA 58 02 00 00 2B D0 D1 FA 79 03 83 D2 00 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B CB B2 01 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A3 ?? ?? ?? ?? 8B CB";
            public static readonly string TArrowWidget = "A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F BF C0 BA 20 03 00 00 2B D0 D1 FA 79 03 83 D2 00 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F BF C0 BA 58 02 00 00 2B D0 D1 FA 79 03 83 D2 00 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B CB B2 01 A1 ?? ?? ?? ?? E8 ?? ?? ?? ?? A3 ?? ?? ?? ?? 8B CB";
        }


        public enum Minigame
        {
            StoneQuarry = 0,
            SawMill = 1,
            ShootingRange = 2,
            FishingPond = 3,
            TypeWriter = 4,
            Memory = 5,
        }

        public enum MinigameID
        {
            BasicQuarry = 3117,
            BasicSawMill = 3118,
            BasicShootingRange = 3119,
            BasicFishingPond = 3120,

            GoodQuarry = 3121,
            GoodSawMill = 3122,
            GoodShootingRange = 3123,
            GoodFishingPond = 3124,

            GreatQuarry = 3125,
            GreatSawMill = 3126,
            GreatShootingRange = 3127,
            GreatFishingPond = 3128,

            TypeWriter = 3130,
            Memory = 3131,

            None = 65535,
        }

        public enum Status
        {
            Nothing = 0,
            GameStart = 1,
            Playing = 2,
            GameEnd = 3,
            GameEnded1 = 4,
            GameEnded2 = 5,

            FishComboEvent = 0xFF,
        }

        public enum Arrow
        {
            Top = 0,
            TopRight = 1,
            Right = 2,
            BottomRight = 3,
            Bottom = 4,
            BottomLeft = 5,
            Left = 6,
            TopLeft = 7,
        }

        public class TArrowWidget
        {
            public static nint ArrowJmp1 = 0xFC; // unkwown, vtable?
            public static nint ArrowJmp2 = 0x18; // unkown

            public static nint GapSize = 0xE0;
        }

        public class TMiniGameManager
        {
            public static nint CurrentMinigamePtr = 0xC;
            public static nint CurrentMinigameType = 0x68;
            public static nint StoneQuarryPtr = 0x70;
            public static nint SawMillPtr = 0x74;
            public static nint ShootingRangePtr = 0x78;
            public static nint FishingPondPtr = 0x7C;
            public static nint Minigame_5 = 0x80;
            public static nint Minigame_6 = 0x84;

            public static nint TMiniGameDurability = 0x8C;
            public static nint MiniGameID = 0x358;
        }

        public class TMiniGamePoints
        {
            public static nint ProductionPoints = 0xC8;
        }

        public class TMiniGameDurability
        {
            public static nint Durability = 0x128;
        }

        public class StoneQuarry
        {
            public static nint IsOn = 0x059F;
            public static nint IsVisble = 0x0018;

            public static nint Wiggler1_Left = 0x0F94;
            public static nint Wiggler2_Left = 0x0F98;
            public static nint Wiggler3_Left = 0x0F9C;
            public static nint Wiggler4_Left = 0x0FA0;
            public static nint Wiggler5_Left = 0x0FA4;
            public static nint Wiggler1_Right = 0x0FA8;
            public static nint Wiggler2_Right = 0x0FAC;
            public static nint Wiggler3_Right = 0x0FB0;
            public static nint Wiggler4_Right = 0x0FB4;
            public static nint Wiggler5_Right = 0x0FB8;

            public static nint Status = 0x038;
            public static nint HP = 0xFBC;
            public static nint Points = 0xFC0;
            public static nint ComboBonus = 0xFC4; // > 19 == Active
        }

        public class SawMill
        {
            public static nint IsOn = 0x059F;
            public static nint IsVisible = 0x0018;

            public static nint LeftPaddle = 0x09BC;
            public static nint RightPaddle = 0x09C0;

            public static nint Status = 0x038;
            public static nint Hp = 0x09C4;
            public static nint Points = 0x09C8;
            public static nint Combo = 0x9CC;
        }

        public class ShootingRange
        {
            public static nint IsOn = 0x059F;
            public static nint IsVisible = 0x0018;

            public static nint ChickenRightPaddle = 0x08AC;
            public static nint BatRightPaddle = 0x08B0;
            public static nint RoosterRightPaddle = 0x08B4;
            public static nint ChickenLeftPaddle = 0x08B8;
            public static nint BatLeftPaddle = 0x08BC;
            public static nint RoosterLeftPaddle = 0x08C0;

            public static nint Status = 0x038;
            public static nint HP = 0x08C4;
            public static nint Points = 0x08C8;
            public static nint Ammo = 0x0908;
            public static nint HasMaxAmmo = 0x0908;
        }

        public class FishPond
        {
            public static nint IsOn = 0x059F;
            public static nint IsVisible = 0x0018;

            public static nint Status = 0x0038;
            public static nint FishEvent = 0x176E;
            public static nint FishData = 0x16E8;
            public static nint bHp = 0x16EC;
            public static nint Points = 0x16F0;
        }

        public class TimingShotGame
        {
            public static nint Data = 0x0024;
            public static nint Hitbox = 0x0030;
            public static nint MiniGamePtr = 0x004C;
        }

        public class HitBox
        {
            public static nint First = 0x0;
            public static nint Second = 0x4;
        }
    }
}
