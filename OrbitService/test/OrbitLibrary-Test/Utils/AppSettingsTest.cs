using OrbitLibrary_Test.TestUtils;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrbitLibrary_Test.Utils
{
    public class AppSettingsTest
    {
        private AppSettings cut;
        public AppSettingsTest()
        {
            cut = new AppSettings();
        }
        [Fact]
        public void ShouldReturnAListOfAppSettings()
        {
            string encryptedJson = "AfocEf3NEBSmW1LqnauOr+hlbKNJrDAgfkme7PrtkxY8/Ckqw2d1VDg4/DMwHXJiLYNP9vlwX5QsLKRmc0zzhy3c9BoDS0q67mLuMDJHME24AqWMAh9zX0o/CafuhtYWNvG8ZpnzkLWGly3el0NxpSbfeh0MkNVEzN94ftbOVCjdvi3t0j+tbv+qnDKkN34O9sQnh5MwwepdswTO/xJ9etJsqCmza0tq0ycpgPepmRBLAfs3SRkN9Q048ftLzmiOpOIKlRbNDDIMyderZe+ssnqVPtwTK7aC0NV0g/3jQ7mH353NBAQng+PoE2ty++MbPOeYIm5xzuIr4eAJIlSSxxRsu+d14xaFwYJxwZqT1sBdemCr5HzkJKwCZWo6WmoVhPTBtCBgrH4j1puWv5aIMwHuWRTzfCNEg/O/ZwU2MYxWq7Gf/eubSmYCkU+jTqqVF5eorJJWi3fP/w6WRapEeKuh+NH2kpsPihCaNh2k/q8Y05ozHDlQRQ==";
            string json = new SettingsCrypto().Decrypt(encryptedJson);

            List<AppSettings> config = JsonConvert.DeserializeObject<List<AppSettings>>(json);
            Assert.NotNull(config);
            Assert.IsType<List<AppSettings>>(config);
            Assert.True(config.Count > 0);
        }
    }
}
