using ClickSharp.DataLayer;
using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ClickSharp.DataLayer.Entities;
using Microsoft.Data.SqlClient;
using static ClickSharp.Pages.Admin.EditMenu;

namespace ClickSharp.Components
{
    public class MenuState
    {
        private readonly ClickSharpContext? _dbContext;
        public MenuState(ClickSharpContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public List<MenuModel> Items { get; private set; } = new List<MenuModel>();
        //public List<MenuModel> ElementsToDrag { get; private set; } = new List<MenuModel>();
        public Dictionary<int, List<MenuModel>> Items { get; private set; } = new Dictionary<int, List<MenuModel>>();

        public event Action? OnChange;

        public async Task MenuStateHasChanged()
        {
            Items.Clear();
            //ElementsToDrag.Clear();

            //List<Menu>? menu = _dbContext?.Menu?.Where(x => x.ParentId == 0 || x.ParentId == null).OrderBy(x => x.Index).ToList();

            //if (menu != null)
            //{
            //    menu.RemoveAll(x => x.Id == 0);
            //    foreach (var item in menu)
            //    {
            //        if (item.ParentId == 0)
            //        {
            //            Items.Add(
            //                new MenuModel
            //                {
            //                    Id = item.Id,
            //                    Index = item.Index,
            //                    DisplayName = item.DisplayName,
            //                    PageId = item.PageId,
            //                    ParentId = item.ParentId
            //                });
            //        }
            //        else
            //        {

            //            ElementsToDrag.Add(
            //                new MenuModel
            //                {
            //                    Id = item.Id,
            //                    Index = item.Index,
            //                    DisplayName = item.DisplayName,
            //                    PageId = item.PageId,
            //                    ParentId = item.ParentId
            //                }
            //                );
            //        }
            //    }
            //}
            //menu = null;
            //menu = _dbContext?.Menu?.Where(x => x.ParentId == 0 || x.ParentId == null).OrderBy(x => x.Index).ToList();
            if(_dbContext != null && _dbContext.Menu != null)
            {
                List<Menu>? menu = await _dbContext.Menu.OrderBy(x => x.Id).ToListAsync();

                if (menu != null)
                {
                    Items.Add(-1, new List<MenuModel>());
                    foreach (var item in menu)
                    {
                        if (item.ParentId == null && item.Id != 1)
                            Items[-1].Add(item);

                        Items.Add(item.Id, new List<MenuModel>());

                        List<Menu>? subMenu = await _dbContext.Menu.Where(x => x.ParentId == item.Id).OrderBy(x => x.Index).ToListAsync();
                        if (subMenu != null)
                        {
                            foreach (var subItem in subMenu)
                            {
                                Items[item.Id].Add(subItem);
                            }
                        }
                    }
                }
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
