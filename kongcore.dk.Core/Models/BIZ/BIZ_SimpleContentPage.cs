using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_SimpleContentPage
    {
        public DTO_SimpleContentPage ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_SimpleContentPage dto = new DTO_SimpleContentPage(current);

            dto.bodyHeader = helper.GetValue(current, "bodyHeader");
            dto.bodyText = helper.GetValue(current, "bodyText").RichStrip();

            dto.contactEmployee1 = helper.GetValue(current, "contactEmployee1").FormatEmailAdvanced();
            dto.contactEmployee2 = helper.GetValue(current, "contactEmployee2").FormatEmailAdvanced();

            return dto;
        }
    }
}
