using System;
using System.Xml.Linq;

namespace kongcore.dk.Core._Statics
{
    public class SettingsHelper
    {
        public class Basic
        {
            public static string SITENAME()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string SITENAME_SHORT()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME_SHORT")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string SITENAME_SHORT_CAP()
            {
                string str = SITENAME_SHORT();
                return ("" + str[0]).ToUpper() + str.Substring(1);
            }

            public static string SITENAME_SHORT_UP()
            {
                string str = SITENAME_SHORT();
                return str.ToUpper();
            }

            public static string SITE_NAME_FULL()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "SITE_NAME_FULL")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string EMAIL_ADMIN()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_ADMIN")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string EMAIL_MAIL()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_MAIL")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string EMAIL_NO_REPLY()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_NO_REPLY")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            //public static string EMAIL_TEST()
            //{
            //    var xdoc = XElement.Load(StaticsHelper.Root + "App_Data\\settings.xml");
            //    var group = xdoc.Elements("basic");

            //    foreach (XElement elem in group.Descendants())
            //    {
            //        if (elem.Name == "setting" && elem.Attribute("name").Value == "EMAIL_TEST")
            //        {
            //            return elem.Value;
            //        }
            //    }
            //    throw new Exception("A-OK, Check.");
            //}

            public static string STREET()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "STREET")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string AREA()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "AREA")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string TOWN()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "TOWN")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string PHONE()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "PHONE")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }

            public static string CVR()
            {
                var xdoc = XElement.Load(StaticsHelper.Root + "settings\\settings.xml");
                var group = xdoc.Elements("basic");

                foreach (XElement elem in group.Descendants())
                {
                    if (elem.Name == "setting" && elem.Attribute("name").Value == "CVR")
                    {
                        return elem.Value;
                    }
                }
                throw new Exception("A-OK, Check.");
            }
        }
    }
}
