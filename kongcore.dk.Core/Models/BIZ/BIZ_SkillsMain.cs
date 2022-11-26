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
    }
}
