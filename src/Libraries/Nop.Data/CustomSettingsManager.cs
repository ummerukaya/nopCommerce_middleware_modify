using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Infrastructure;

namespace Nop.Data
{
    public partial class CustomSettingsManager
    {

        public static bool Test(string filePath = null, bool reloadSettings = false, INopFileProvider fileProvider = null)
        {
            var customSettings = new CustomSettings();

            try
            {
                fileProvider ??= CommonHelper.DefaultFileProvider;
                filePath ??= fileProvider.MapPath(NopDataSettingsDefaults.CustomFilePath);
                var text = fileProvider.ReadAllText(filePath, Encoding.UTF8);

                //get custom settings from the JSON file
                customSettings = JsonConvert.DeserializeObject<CustomSettings>(text);
                if (customSettings.CheckingString == null)
                    return false;
            }

            catch
            {
                return false;
            }

            

            return true;
        }
    }
}
