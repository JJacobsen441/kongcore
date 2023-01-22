using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DTOs;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_Procedure
    {
        public DTO_Procedure ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper.Root();
            IPublishedContent current = helper.RootCurrent();

            DTO_Procedure dto = new DTO_Procedure(current);

            dto.procedureHeader = helper.GetValue(current, "procedureHeader");
            dto.procedureBodyText = helper.GetValue(current, "procedureBodyText").RichStrip();
            dto.procedureMail = helper.GetValue(current, "procedureMail").FormatEmailSimple();
            dto.procedureText = helper.GetValue(current, "procedureText");

            IPublishedContent block1Node = helper.NodeType(root, "block1");
            dto.block1header = helper.GetPropertyValue(block1Node, "block1Header");
            dto.block1text = helper.GetPropertyValue(block1Node, "block1Text").FormatParagraph();
            dto.block1buttontext = helper.GetPropertyValue(block1Node, "block1ButtonText");

            IPublishedContent block3Node = helper.NodeType(root, "block2");
            dto.block2header = helper.GetPropertyValue(block3Node, "block2Header");
            dto.block2text = helper.GetPropertyValue(block3Node, "block2Text").FormatParagraph();
            dto.block2buttontext = helper.GetPropertyValue(block3Node, "block2ButtonText");

            return dto;
        }
    }
}
