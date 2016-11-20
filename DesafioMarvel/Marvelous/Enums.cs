using System.ComponentModel;

namespace Marvelous
{

    public enum Image
    {

        [Description("50x75px")]
        PortraitSmall,
        [Description("100x150px")]
        PortraitMedium,
        [Description("150x225px")]
        PortraitXlarge,
        [Description("168x252px")]
        PortraitFantastic,
        [Description("300x450px")]
        PortraitUncanny,
        [Description("216x324px")]
        PortraitIncredible,

        [Description("65x45px")]
        StandardSmall,
        [Description("100x100px")]
        StandardMedium,
        [Description("140x140px")]
        StandardLarge,
        [Description("200x200px")]
        StandardXlarge,
        [Description("250x250px")]
        StandardFantastic,
        [Description("180x180px")]
        StandardAmazing,

        [Description("120x90px")]
        LandscapeSmall,
        [Description("175x130px")]
        LandscapeMedium,
        [Description("190x140px")]
        LandscapeLarge,
        [Description("270x200px")]
        LandscapeXlarge,
        [Description("250x156px")]
        LandscapeAmazing,
        [Description("464x261px")]
        LandscapeIncredible,

        [Description("full image, constrained to 500px wide")]
        Detail,
        [Description("no variant descriptor")]
        Full
    }
    public static class EnumExtensions
    {
        public static string ToParameter(this Image image)
        {
            switch (image)
            {
                case Image.PortraitSmall:
                    return "/portrait_small";
                case Image.PortraitMedium:
                    return "/portrait_medium";
                case Image.PortraitXlarge:
                    return "/portrait_xlarge";
                case Image.PortraitFantastic:
                    return "/portrait_fantastic";
                case Image.PortraitUncanny:
                    return "/portrait_uncanny";
                case Image.PortraitIncredible:
                    return "/portrait_incredible";

                case Image.StandardSmall:
                    return "/standard_small";
                case Image.StandardMedium:
                    return "/standard_medium";
                case Image.StandardLarge:
                    return "/standard_large";
                case Image.StandardXlarge:
                    return "/standard_xlarge";
                case Image.StandardFantastic:
                    return "/standard_fantastic";
                case Image.StandardAmazing:
                    return "/standard_amazing";
                case Image.LandscapeSmall:
                    return "/landscape_small";
                case Image.LandscapeMedium:
                    return "/landscape_medium";
                case Image.LandscapeLarge:
                    return "/landscape_large";
                case Image.LandscapeXlarge:
                    return "/landscape_xlarge";
                case Image.LandscapeAmazing:
                    return "/landscape_amazing";
                case Image.LandscapeIncredible:
                    return "/landscape_incredible";

                case Image.Detail:
                    return "/detail";
                default:
                    return "";
            }
        }
    }
}
