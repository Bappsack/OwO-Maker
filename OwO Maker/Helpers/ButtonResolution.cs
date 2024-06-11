using System.Drawing;

namespace OwO_Maker.Helpers
{
    public class ButtonResolutionHelper
    {
        public class ButtonResolution
        {
            public Point RewardButton { get; set; }
            public Point TryAgain { get; set; }
            public Point FailedTryAgain { get; set; }
            public Point[] LevelButtons { get; set; }
            public Point GameStart { get; set; }
            public Point[] MinigameArrows { get; set; }
            public Point StartMinigame { get; set; }
            public Point EndMinigame { get; set; }
        }

        public static ButtonResolution GetButtonPositions(string Resolution)
        {
            return Resolution switch
            {
                "1024x768" => new ButtonResolution
                {
                    RewardButton = new Point(640, 438),
                    TryAgain = new Point(462, 466),
                    FailedTryAgain = new Point(381, 434),
                    GameStart = new Point(514, 550),
                    StartMinigame = new Point(405, 541),
                    EndMinigame = new Point(565, 465),

                    MinigameArrows =
                       [
                        new Point(513, 332),
                        new Point(569, 352),
                        new Point(590, 381),
                        new Point(567, 437),
                        new Point(514, 462),
                        new Point(453, 438),
                        new Point(435, 384),
                        new Point(455, 326)
                        ],

                    LevelButtons =
                    [
                        new Point(380,438),
                        new Point(447,438),
                        new Point(513,438),
                        new Point(578,438),
                        new Point(647,438)
                    ],
                },

                "1280x1024" => new ButtonResolution
                {
                    RewardButton = new Point(771, 566),
                    TryAgain = new Point(462, 466),
                    FailedTryAgain = new Point(512, 564),
                    GameStart = new Point(648, 679),
                    StartMinigame = new Point(597, 660),
                    EndMinigame = new Point(696, 591),

                    MinigameArrows =
                    [
                        new Point(640, 458),
                        new Point(698, 477),
                        new Point(721, 509),
                        new Point(700, 567),
                        new Point(643, 589),
                        new Point(583, 567),
                        new Point(562, 511),
                        new Point(584, 457)
                    ],

                    LevelButtons =
                    [
                        new Point(503,565),
                            new Point(571,565),
                            new Point(642,565),
                            new Point(701,565),
                            new Point(771,565)
                    ],
                },

                "1280x800" => new ButtonResolution
                {
                    RewardButton = new Point(772, 453),
                    TryAgain = new Point(587, 481),
                    FailedTryAgain = new Point(515, 451),
                    GameStart = new Point(641, 568),
                    StartMinigame = new Point(600, 549),
                    EndMinigame = new Point(695, 481),

                    MinigameArrows =
                    [
                        new Point(641, 346),
                        new Point(699, 366),
                        new Point(719, 398),
                        new Point(695, 454),
                        new Point(641, 479),
                        new Point(584, 454),
                        new Point(562, 401),
                        new Point(584, 346)
                    ],

                    LevelButtons =
                    [
                    new Point(505,454),
                    new Point(572,453),
                    new Point(638,453),
                    new Point(705,457),
                    new Point(772,454)
                    ],
                },

                "1440x900" => new ButtonResolution
                {
                    RewardButton = new Point(854, 501),
                    TryAgain = new Point(662, 532),
                    FailedTryAgain = new Point(595, 498),
                    GameStart = new Point(707, 615),
                    StartMinigame = new Point(680, 601),
                    EndMinigame = new Point(775, 532),

                    MinigameArrows =
                    [
                        new Point(722,398),
                        new Point(779, 418),
                        new Point(798, 448),
                        new Point(777, 504),
                        new Point(721, 528),
                        new Point(665, 506),
                        new Point(646, 451),
                        new Point(666, 394)
                    ],

                    LevelButtons =
                    [
                    new Point(586,504),
                    new Point(646,503),
                    new Point(716,502),
                    new Point(784,506),
                    new Point(847,504)
                    ],
                },

                "1024x700" => new ButtonResolution
                {
                    RewardButton = new Point(636, 401),
                    TryAgain = new Point(460, 432),
                    FailedTryAgain = new Point(382, 400),
                    GameStart = new Point(513, 514),
                    StartMinigame = new Point(479, 501),
                    EndMinigame = new Point(564, 430),

                    MinigameArrows =
                    [
                        new Point(513, 295),
                        new Point(569, 317),
                        new Point(592, 349),
                        new Point(568, 405),
                        new Point(513, 428),
                        new Point(456, 404),
                        new Point(436, 349),
                        new Point(456, 292)
                    ],

                    LevelButtons =
                    [
                    new Point(376,403),
                    new Point(442,403),
                    new Point(507,406),
                    new Point(581,402),
                    new Point(646,404)
                   ],
                },

                "1680x1050" => new ButtonResolution
                {
                    RewardButton = new Point(972, 573),
                    TryAgain = new Point(789, 606),
                    FailedTryAgain = new Point(709, 575),
                    GameStart = new Point(846, 693),
                    StartMinigame = new Point(738, 673),
                    EndMinigame = new Point(895, 606),

                    MinigameArrows =
                       [
                        new Point(840, 473),
                        new Point(898, 492),
                        new Point(922, 525),
                        new Point(894, 579),
                        new Point(840, 599),
                        new Point(784, 577),
                        new Point(763, 527),
                        new Point(786, 472)
                        ],

                    LevelButtons =
                    [
                    new Point(703,579),
                    new Point(773,579),
                    new Point(838,581),
                    new Point(906,581),
                    new Point(975,580)
                   ],
                },

                // Fullscreen Windowed FullHD
                "1920x1080" => new ButtonResolution
                {
                    RewardButton = new Point(1088, 590),
                    TryAgain = new Point(908, 622),
                    FailedTryAgain = new Point(835, 589),
                    GameStart = new Point(996, 707),
                    StartMinigame = new Point(931, 696),
                    EndMinigame = new Point(1017, 621),

                    MinigameArrows =
                    [
                        new Point(960, 485),
                        new Point(1014, 508),
                        new Point(1038, 539),
                        new Point(1017, 594),
                        new Point(960, 621),
                        new Point(903, 595),
                        new Point(884, 539),
                        new Point(904, 482)
                    ],

                    LevelButtons =
                    [
                    new Point(826,596),
                    new Point(892,593),
                    new Point(962,594),
                    new Point(1024,593),
                    new Point(1090,594)
                    ],
                },

                // Fullscreen Windowed WQHD
                "2560x1440" => new ButtonResolution
                {
                    RewardButton = new Point(1409, 772),
                    TryAgain = new Point(1222, 801),
                    FailedTryAgain = new Point(1148, 770),
                    GameStart = new Point(1280, 889),
                    StartMinigame = new Point(1281, 873),
                    EndMinigame = new Point(1334, 801),

                    MinigameArrows =
                    [
                        new Point(1280, 666),
                        new Point(1337, 686),
                        new Point(1358, 717),
                        new Point(1335, 775),
                        new Point(1281, 798),
                        new Point(1225, 776),
                        new Point(1203, 722),
                        new Point(1223, 663)
                    ],

                    LevelButtons =
                    [
                    new Point(1149,773),
                    new Point(1215,773),
                    new Point(1279,771),
                    new Point(1338,771),
                    new Point(1407,775)
                    ],
                },
                _ => null,
            };
        }
    }
}
