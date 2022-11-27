using kongcore.dk.Core.Common;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Models.BIZ
{
    class BIZ_SkillsMain
    {
        public List<Skill> GetSkills(ContentHelper helper)
        {
            List<IPublishedContent> skills = new List<IPublishedContent>();

            skills = helper.NodesType(helper._CurrentRoot(), "skillsItem");//.OrderByDescending(x => x.CreateDate).ToList();
            //dto.skills = helper.GetItems(selection, null, "skillTitle", "skillContent", null);

            if (skills.IsNull())
                throw new Exception();

            List<Skill> items = new List<Skill>();
            foreach (var item in skills)
            {
                string title = helper.GetValue(item, "skillTitle");
                string content = helper.GetValue(item, "skillContent").RichStrip();

                items.Add(new Skill() { title = title, content = content });
            }

            return items;
        }

        public DTO_SkillsMain ToDTO(ContentHelper helper)
        {
            IPublishedContent root = helper._Root();
            IPublishedContent current = helper._CurrentRoot();

            DTO_SkillsMain dto = new DTO_SkillsMain(current);

            dto.skillsTitle = helper.GetValue(current, "skillsTitle");
            dto.skillsBodyText = helper.GetValue(current, "skillsBodyText").FormatParagraph();
            BIZ_SkillsMain biz_skill = new BIZ_SkillsMain();
            dto.skills = biz_skill.GetSkills(helper);

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
