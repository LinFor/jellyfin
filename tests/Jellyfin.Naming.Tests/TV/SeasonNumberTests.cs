using Emby.Naming.Common;
using Emby.Naming.TV;
using Xunit;

namespace Jellyfin.Naming.Tests.TV
{
    public class SeasonNumberTests
    {
        private readonly EpisodeResolver _resolver = new EpisodeResolver(new NamingOptions());

        [Theory]
        [InlineData("The Daily Show/The Daily Show 25x22 - [WEBDL-720p][AAC 2.0][x264] Noah Baumbach-TBS.mkv", 25)]
        [InlineData("/Show/Season 02/S02E03 blah.avi", 2)]
        [InlineData("Season 1/seriesname S01x02 blah.avi", 1)]
        [InlineData("Season 1/S01x02 blah.avi", 1)]
        [InlineData("Season 1/seriesname S01xE02 blah.avi", 1)]
        [InlineData("Season 1/01x02 blah.avi", 1)]
        [InlineData("Season 1/S01E02 blah.avi", 1)]
        [InlineData("Season 1/S01xE02 blah.avi", 1)]
        [InlineData("Season 1/seriesname 01x02 blah.avi", 1)]
        [InlineData("Season 1/seriesname S01E02 blah.avi", 1)]
        [InlineData("Season 2/Elementary - 02x03 - 02x04 - 02x15 - Ep Name.mp4", 2)]
        [InlineData("Season 2/02x03 - 02x04 - 02x15 - Ep Name.mp4", 2)]
        [InlineData("Season 2/02x03-04-15 - Ep Name.mp4", 2)]
        [InlineData("Season 2/Elementary - 02x03-04-15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/02x03-E15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/Elementary - 02x03-E15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/02x03 - x04 - x15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/Elementary - 02x03 - x04 - x15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/02x03x04x15 - Ep Name.mp4", 2)]
        [InlineData("Season 02/Elementary - 02x03x04x15 - Ep Name.mp4", 2)]
        [InlineData("Season 1/Elementary - S01E23-E24-E26 - The Woman.mp4", 1)]
        [InlineData("Season 1/S01E23-E24-E26 - The Woman.mp4", 1)]
        [InlineData("Season 25/The Simpsons.S25E09.Steal this episode.mp4", 25)]
        [InlineData("The Simpsons/The Simpsons.S25E09.Steal this episode.mp4", 25)]
        [InlineData("2016/Season s2016e1.mp4", 2016)]
        [InlineData("2016/Season 2016x1.mp4", 2016)]
        [InlineData("Season 2009/2009x02 blah.avi", 2009)]
        [InlineData("Season 2009/S2009x02 blah.avi", 2009)]
        [InlineData("Season 2009/S2009E02 blah.avi", 2009)]
        [InlineData("Season 2009/S2009xE02 blah.avi", 2009)]
        [InlineData("Season 2009/seriesname 2009x02 blah.avi", 2009)]
        [InlineData("Season 2009/seriesname S2009x02 blah.avi", 2009)]
        [InlineData("Season 2009/seriesname S2009E02 blah.avi", 2009)]
        [InlineData("Season 2009/Elementary - 2009x03 - 2009x04 - 2009x15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/2009x03 - 2009x04 - 2009x15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/2009x03-04-15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/Elementary - 2009x03 - x04 - x15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/2009x03x04x15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/Elementary - 2009x03x04x15 - Ep Name.mp4", 2009)]
        [InlineData("Season 2009/Elementary - S2009E23-E24-E26 - The Woman.mp4", 2009)]
        [InlineData("Season 2009/S2009E23-E24-E26 - The Woman.mp4", 2009)]
        [InlineData("Series/1-12 - The Woman.mp4", 1)]
        [InlineData(@"Running Man/Running Man S2017E368.mkv", 2017)]
        [InlineData(@"Case Closed (1996-2007)/Case Closed - 317.mkv", 3)]

        // Usenet: year than season number
        [InlineData("Shadow.and.Bone.2021.S01.WEB-DL.1080p-Kyle/Shadow.and.Bone.2021.S01E02.WEB-DL.1080p-Kyle.mkv", 1)]

        // Usenet: Season number than year
        [InlineData("Zhuki.S02.2021.WEB-DL.1080p/06.Zhuki.S02.2021.WEB-DL.1080p.mkv", 2)]
        [InlineData("Atiye.s01.2019.L2.WEBRip1080p/Atiye.e04.2019.L2.WEBRip1080p.mp4", 1)]
        [InlineData("IP.Pirogova.S04.2021.WEB-DL.(1080p)/IP.Pirogova.s04e03.2021.WEB-DL.(1080p).mkv", 4)]

        // Usenet: Season number only
        [InlineData("Gde.logika.S07.WEB-DL.1080.25Kuzmich/Gde.logika.S07.E02.WEB-DL.1080.25Kuzmich.mkv", 7)]
        [InlineData("The.Girlfriend.Experience.S01.HDTV.1080p.FocusStudio/The.Girlfriend.Experience.S01E07.HDTV.1080p.FocusStudio.mkv", 1)]

        // Usenet: Year only
        [InlineData("Zhuki.2019.WEB-DL.(1080p).Getty/Zhuki.e17.Film.o.seriale.2019.WEB-DL.(1080p).Getty.mkv", null)]
        [InlineData("Mediator.2021.WEB-DL.(1080p).Getty/Mediator.e04.2021.WEB-DL.(1080p).Getty.mkv", null)]
        [InlineData("V.aktivnom.poiske.2021.WEB-DL.1080p/07.V.aktivnom.poiske.2021.WEB-DL.1080p.mkv", null)]
        [InlineData("Chto.Gde.Kogda.Vesennjaja serija.Igr.2021.HDTV(1080i).25Kuzmich/04.Chto.Gde.Kogda.Vesennjaja serija.Igr.2021.HDTV(1080i).25Kuzmich.ts", null)]
        [InlineData("MosGaz.2012.WEB-DL.(1080p).lunkin/MosGaz.07.serya.WEB-DL.(1080p).by.lunkin.mkv", null)]

        // TODO: [InlineData(@"Seinfeld/Seinfeld 0807 The Checks.avi", 8)]
        public void GetSeasonNumberFromEpisodeFileTest(string path, int? expected)
        {
            var result = _resolver.Resolve(path, false);

            Assert.Equal(expected, result?.SeasonNumber);
        }
    }
}
