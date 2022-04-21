using Emby.Naming.Common;
using Emby.Naming.TV;
using Xunit;

namespace Jellyfin.Naming.Tests.TV
{
    public class EpisodeNumberTests
    {
        private readonly NamingOptions _namingOptions = new NamingOptions();

        [Theory]
        [InlineData("Watchmen (2019)/Watchmen 1x03 [WEBDL-720p][EAC3 5.1][h264][-TBS] - She Was Killed by Space Junk.mkv", 3)]
        [InlineData("The Daily Show/The Daily Show 25x22 - [WEBDL-720p][AAC 2.0][x264] Noah Baumbach-TBS.mkv", 22)]
        [InlineData("Castle Rock 2x01 Que el rio siga su curso [WEB-DL HULU 1080p h264 Dual DD5.1 Subs].mkv", 1)]
        [InlineData("After Life 1x06 Episodio 6 [WEB-DL NF 1080p h264 Dual DD 5.1 Sub].mkv", 6)]
        [InlineData("Season 02/S02E03 blah.avi", 3)]
        [InlineData("Season 2/02x03 - 02x04 - 02x15 - Ep Name.mp4", 3)]
        [InlineData("Season 02/02x03 - x04 - x15 - Ep Name.mp4", 3)]
        [InlineData("Season 1/01x02 blah.avi", 2)]
        [InlineData("Season 1/S01x02 blah.avi", 2)]
        [InlineData("Season 1/S01E02 blah.avi", 2)]
        [InlineData("Season 2/Elementary - 02x03-04-15 - Ep Name.mp4", 3)]
        [InlineData("Season 1/S01xE02 blah.avi", 2)]
        [InlineData("Season 1/seriesname S01E02 blah.avi", 2)]
        [InlineData("Season 2/Episode - 16.avi", 16)]
        [InlineData("Season 2/Episode 16.avi", 16)]
        [InlineData("Season 2/Episode 16 - Some Title.avi", 16)]
        [InlineData("Season 2/16 Some Title.avi", 16)]
        [InlineData("Season 2/16 - 12 Some Title.avi", 16)]
        [InlineData("Season 2/7 - 12 Angry Men.avi", 7)]
        [InlineData("Season 1/seriesname 01x02 blah.avi", 2)]
        [InlineData("Season 25/The Simpsons.S25E09.Steal this episode.mp4", 9)]
        [InlineData("Season 1/seriesname S01x02 blah.avi", 2)]
        [InlineData("Season 2/Elementary - 02x03 - 02x04 - 02x15 - Ep Name.mp4", 3)]
        [InlineData("Season 1/seriesname S01xE02 blah.avi", 2)]
        [InlineData("Season 02/Elementary - 02x03 - x04 - x15 - Ep Name.mp4", 3)]
        [InlineData("Season 02/Elementary - 02x03x04x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2/02x03-04-15 - Ep Name.mp4", 3)]
        [InlineData("Season 02/02x03-E15 - Ep Name.mp4", 3)]
        [InlineData("Season 02/Elementary - 02x03-E15 - Ep Name.mp4", 3)]
        [InlineData("Season 1/Elementary - S01E23-E24-E26 - The Woman.mp4", 23)]
        [InlineData("Season 2009/S2009E23-E24-E26 - The Woman.mp4", 23)]
        [InlineData("Season 2009/2009x02 blah.avi", 2)]
        [InlineData("Season 2009/S2009x02 blah.avi", 2)]
        [InlineData("Season 2009/S2009E02 blah.avi", 2)]
        [InlineData("Season 2009/seriesname 2009x02 blah.avi", 2)]
        [InlineData("Season 2009/Elementary - 2009x03x04x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/2009x03x04x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/Elementary - 2009x03-E15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/S2009xE02 blah.avi", 2)]
        [InlineData("Season 2009/Elementary - S2009E23-E24-E26 - The Woman.mp4", 23)]
        [InlineData("Season 2009/seriesname S2009xE02 blah.avi", 2)]
        [InlineData("Season 2009/2009x03-E15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/seriesname S2009E02 blah.avi", 2)]
        [InlineData("Season 2009/2009x03 - 2009x04 - 2009x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/2009x03 - x04 - x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/seriesname S2009x02 blah.avi", 2)]
        [InlineData("Season 2009/Elementary - 2009x03 - 2009x04 - 2009x15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/Elementary - 2009x03-04-15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/2009x03-04-15 - Ep Name.mp4", 3)]
        [InlineData("Season 2009/Elementary - 2009x03 - x04 - x15 - Ep Name.mp4", 3)]
        [InlineData("Season 1/02 - blah-02 a.avi", 2)]
        [InlineData("Season 1/02 - blah.avi", 2)]
        [InlineData("Season 2/02 - blah 14 blah.avi", 2)]
        [InlineData("Season 2/02.avi", 2)]
        [InlineData("Season 2/2. Infestation.avi", 2)]
        [InlineData("The Wonder Years/The.Wonder.Years.S04.PDTV.x264-JCH/The Wonder Years s04e07 Christmas Party NTSC PDTV.avi", 7)]
        [InlineData("Running Man/Running Man S2017E368.mkv", 368)]
        [InlineData("Season 2/[HorribleSubs] Hunter X Hunter - 136 [720p].mkv", 136)] // triple digit episode number
        [InlineData("Log Horizon 2/[HorribleSubs] Log Horizon 2 - 03 [720p].mkv", 3)] // digit in series name
        [InlineData("Season 1/seriesname 05.mkv", 5)] // no hyphen between series name and episode number
        [InlineData("[BBT-RMX] Ranma ½ - 154 [50AC421A].mkv", 154)] // hyphens in the pre-name info, triple digit episode number
        [InlineData("Season 2/Episode 21 - 94 Meetings.mp4", 21)] // Title starts with a number
        [InlineData("/The.Legend.of.Condor.Heroes.2017.V2.web-dl.1080p.h264.aac-hdctv/The.Legend.of.Condor.Heroes.2017.E07.V2.web-dl.1080p.h264.aac-hdctv.mkv", 7)]
        // [InlineData("Case Closed (1996-2007)/Case Closed - 317.mkv", 317)] // triple digit episode number

        // Usenet: year than season number
        [InlineData("Shadow.and.Bone.2021.S01.WEB-DL.1080p-Kyle/Shadow.and.Bone.2021.S01E02.WEB-DL.1080p-Kyle.mkv", 2)]

        // Usenet: Season number than year
        [InlineData("Zhuki.S02.2021.WEB-DL.1080p/06.Zhuki.S02.2021.WEB-DL.1080p.mkv", 6)]
        [InlineData("Atiye.s01.2019.L2.WEBRip1080p/Atiye.e04.2019.L2.WEBRip1080p.mp4", 4)]
        [InlineData("IP.Pirogova.S04.2021.WEB-DL.(1080p)/IP.Pirogova.s04e03.2021.WEB-DL.(1080p).mkv", 3)]

        // Usenet: Season number only
        [InlineData("Gde.logika.S07.WEB-DL.1080.25Kuzmich/Gde.logika.S07.E05.WEB-DL.1080.25Kuzmich.mkv", 5)]
        [InlineData("The.Girlfriend.Experience.S01.HDTV.1080p.FocusStudio/The.Girlfriend.Experience.S01E07.HDTV.1080p.FocusStudio.mkv", 7)]

        // Usenet: Year only
        [InlineData("Zhuki.2019.WEB-DL.(1080p).Getty/Zhuki.e17.Film.o.seriale.2019.WEB-DL.(1080p).Getty.mkv", 17)]
        [InlineData("Mediator.2021.WEB-DL.(1080p).Getty/Mediator.e04.2021.WEB-DL.(1080p).Getty.mkv", 4)]
        [InlineData("V.aktivnom.poiske.2021.WEB-DL.1080p/07.V.aktivnom.poiske.2021.WEB-DL.1080p.mkv", 7)]
        [InlineData("Chto.Gde.Kogda.Vesennjaja serija.Igr.2021.HDTV(1080i).25Kuzmich/04.Chto.Gde.Kogda.Vesennjaja serija.Igr.2021.HDTV(1080i).25Kuzmich.ts", 4)]
        [InlineData("MosGaz.2012.WEB-DL.(1080p).lunkin/MosGaz.07.serya.WEB-DL.(1080p).by.lunkin.mkv", 7)]

        // TODO: [InlineData("Case Closed (1996-2007)/Case Closed - 317.mkv", 317)] // triple digit episode number
        // TODO: [InlineData("Season 2/16 12 Some Title.avi", 16)]
        // TODO: [InlineData("Season 4/Uchuu.Senkan.Yamato.2199.E03.avi", 3)]
        // TODO: [InlineData("Season 2/7 12 Angry Men.avi", 7)]
        // TODO: [InlineData("Season 02/02x03x04x15 - Ep Name.mp4", 2)]
        public void GetEpisodeNumberFromFileTest(string path, int? expected)
        {
            var result = new EpisodePathParser(_namingOptions)
                .Parse(path, false);

            Assert.Equal(expected, result.EpisodeNumber);
        }
    }
}
