using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC.Areas.Admin
{
    public class NewsGroupModel
    {
       
        public static List<MenuCategory> BuildModel(List<News> lstnews)
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


    }
}
