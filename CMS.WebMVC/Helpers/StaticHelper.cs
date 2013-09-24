using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC
{
    public enum TypePathImage
    {
        Small,
        Large,
        Thumbnail
    }

    public static class StaticHelper
    {

        public const double heightLargeImg = 854;
        public const double WidthLargeImg = 1280;

        public const double heightSmallImg = 274;
        public const double WidthSmallImg = 411;

        public const double heightThumbImg = 67;
        public const double WidthThumbImg = 100;


        public static string Create(bool toLower, params string[] values)
        {
            return CreateFriendlyName(toLower, String.Join("-", values));
        }

        /// <summary>
        /// Creates a slug.
        /// References:
        /// http://www.unicode.org/reports/tr15/tr15-34.html
        /// http://meta.stackoverflow.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
        /// http://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486
        /// http://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string
        /// </summary>
        /// <param name="toLower"></param>
        /// <param name="normalised"></param>
        /// <returns></returns>
        public static string CreateFriendlyName(bool toLower, string value)
        {
            if (value == null) return "";

            var normalised = value.Normalize(NormalizationForm.FormKD);

            const int maxlen = 80;
            int len = normalised.Length;
            bool prevDash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = normalised[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }
                    sb.Append(c);
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }
                    // tricky way to convert to lowercase
                    if (toLower)
                        sb.Append((char)(c | 32));
                    else
                        sb.Append(c);
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevDash && sb.Length > 0)
                    {
                        prevDash = true;
                    }
                }
                else
                {
                    string swap = ConvertEdgeCases(c, toLower);

                    if (swap != null)
                    {
                        if (prevDash)
                        {
                            sb.Append('-');
                            prevDash = false;
                        }
                        sb.Append(swap);
                    }
                }

                if (sb.Length == maxlen) break;
            }

            return sb.ToString();
        }

        static string ConvertEdgeCases(char c, bool toLower)
        {
            string swap = null;
            switch (c)
            {
                case 'ı':
                    swap = "i";
                    break;
                case 'ł':
                    swap = "l";
                    break;
                case 'Ł':
                    swap = toLower ? "l" : "L";
                    break;
                case 'đ':
                    swap = "d";
                    break;
                case 'ß':
                    swap = "ss";
                    break;
                case 'ø':
                    swap = "o";
                    break;
                case 'Þ':
                    swap = "th";
                    break;
            }
            return swap;
        }


        public static string SaveFileImage(string pathToSave, HttpPostedFileBase fileImage, string fileNameExpected)
        {
            if (fileImage != null)
            {
                string path = pathToSave; // "/ImageRepoisitory/Users/" + users.GuidId;
                string pathFolder = HttpContext.Current.Server.MapPath(path);
                string fileName = StaticHelper.CreateFriendlyName(true, fileNameExpected) + System.IO.Path.GetExtension(fileImage.FileName);
                if (!System.IO.Directory.Exists(pathFolder))
                    System.IO.Directory.CreateDirectory(pathFolder);
                fileImage.SaveAs(pathFolder + "\\" + fileName);
                return path + "/" + fileName;
            }
            throw new Exception("File image cannot null");
        }

        public static string SaveImage(string pathToSave, TypePathImage TypePath, System.Drawing.Image fileImage, string fileNameExpected, string extension)
        {
            if (fileImage != null)
            {
                string path = pathToSave + "/" + TypePath.ToString(); // "/ImageRepoisitory/Users/" + users.GuidId;
                string pathFolder = HttpContext.Current.Server.MapPath(path);
                string fileName = StaticHelper.CreateFriendlyName(true, fileNameExpected) + extension;
                if (!System.IO.Directory.Exists(pathFolder))
                    System.IO.Directory.CreateDirectory(pathFolder);
                if (!System.IO.File.Exists(pathFolder + "\\" + fileName))
                    System.IO.File.Delete(pathFolder + "\\" + fileName);
                fileImage.Save(pathFolder + "\\" + fileName);
                return path + "/" + fileName;
            }
            throw new Exception("File image cannot null");
        }



        #region BuildNews Group
        public static List<MenuCategory> BuildNewsGroupByMenu(List<News> lstnews)
        {
            List<MenuCategory> allMenuCategory = new List<MenuCategory>();
            foreach (var item in lstnews)
            {
                if (item.MenuId != null && !allMenuCategory.Exists(f => f.GuidId == item.MenuId.Value))
                    allMenuCategory.Add(item.MenuCategory);
            }

            List<MenuCategory> parents = new List<MenuCategory>();
            foreach (var item in allMenuCategory)
            {
                item.ListNews = lstnews.Where(f => f.MenuId == item.GuidId).ToList();
                if (item.ParentId == null)
                {
                    parents.Add(item);
                    continue;
                }
                bool isParent = true;
                foreach (var it in allMenuCategory)
                {
                    if (item.ParentId == it.GuidId)
                    {
                        isParent = false;
                        break;
                    }
                }
                if (isParent)
                    parents.Add(item);
            }

            var listToRef = new List<MenuCategory>(allMenuCategory.Except(parents)).ToList();
            BuildHierarchical(parents, listToRef);
            return parents;
        }


        private static void BuildHierarchical(List<MenuCategory> listItem, List<MenuCategory> listToRef)
        {
            if (listItem.Count() == 0)
                return;

            foreach (var item in listItem)
            {
                item.ListMenuCategory = new List<MenuCategory>();
                item.ListMenuCategory.AddRange(listToRef.Where(f => f.ParentId == item.GuidId).ToList());
                var listRef = listToRef.Except(item.ListMenuCategory.ToList()).ToList();
                if (listToRef.Count > 0)
                    BuildHierarchical(item.ListMenuCategory.ToList(), listRef);
            }
        }
        #endregion
    }
}