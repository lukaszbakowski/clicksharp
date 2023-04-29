using ClickSharp.DataLayer;
using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ClickSharp.DataLayer.Entities;
using Microsoft.Data.SqlClient;
using static ClickSharp.Pages.Admin.EditMenu;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace ClickSharp.Components.Test
{
    public class MenuState2
    {
        private DraggableItemData? _dragged;
        private DraggableItemData? _memento;
        private readonly ClickSharpContext? _dbContext;
        public MenuState2(ClickSharpContext dbContext)
        {
            _dbContext = dbContext;
            Container = new DraggableItemData(
                new MenuModel
                {
                    Id = 1,
                    DisplayName = "CONTAINER",
                    Index = 0,
                    PageId = null,
                    ParentId = null
                }
            );
        }
        public bool Success { get; set; } = true;
        public DraggableItemData? Dragged
        {
            get
            {
                return _dragged;
            }
            set
            {
                if (value == null)
                {
                    _dragged = null;
                    _memento = null;
                }
                else
                {
                    _memento = Container;
                    _dragged = value;
                }
            }
        }
        public DraggableItemData Container { get; private set; }

        public event Action? OnChange;

        public void MenuStateHasChanged()
        {
            if (Success == false && _memento != null)
            {
                Container = _memento;
            }
            else
            {
                List<MenuModel> menuList = PopulateWithCurrentState().ToList();
                Container = Container.Init(menuList);
            }

            NotifyStateChanged();
        }
        public async void OnDrop(DraggableItemData NewParent, int targetIndex)
        {
            Console.WriteLine($"new parent will be: {NewParent.MenuData.Id}");
            int error_code = 0;
            try
            {
                error_code--;
                if (_dragged == null)
                    throw new Exception();
                error_code--;
                if (!_dragged.CheckIfDropIsPossible(NewParent.MenuData.Id))
                    throw new Exception();
                error_code--;
                if (_dragged.Parent == null)
                    throw new Exception();

                error_code--;
                _dragged.Parent -= _dragged;

                var oldIndex = _dragged.MenuData.Index;
                _dragged.MenuData.Index = targetIndex;

                if (_dragged.Parent.MenuData.Id == NewParent.MenuData.Id)
                {
                    if (oldIndex < targetIndex)
                    {
                        _dragged.MenuData.Index--;
                    }
                }
                error_code--;
                NewParent += _dragged;

                NewParent.Childrens.FirstOrDefault(x => x.MenuData.Id == _dragged.MenuData.Id).SetParent(NewParent);

                error_code = 1;

            }
            catch
            {
                Console.WriteLine($"ondrop error code: {error_code}");
            }
            finally
            {
                if (error_code == 1)
                {
                    Console.WriteLine($"ondrop success: {error_code}");
                    Dragged = null;
                    if(await SaveChangedState())
                        NotifyStateChanged();
                }
                else if (_memento != null)
                {
                    Container = _memento;
                }
            }
        }
        public async Task<bool> SaveChangedState()
        {
            if (_dbContext == null)
                return await Task.FromResult(false);

            Console.WriteLine("is working");
            List<Menu>? currentMenuList = _dbContext.Menu?.ToList();
            var newMenuList = ToPopulateChangedState(Container);

            if (newMenuList != null && currentMenuList != null)

                await using (var dbTran = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var item in newMenuList)
                        {
                            if(_dbContext.Menu != null)
                            {
                                var itemToUpd = await _dbContext.Menu.FirstOrDefaultAsync(x => x.Id == item.Id);
                                if (itemToUpd != null)
                                {
                                    if(itemToUpd.ParentId != item.ParentId || itemToUpd.Index != item.Index)
                                    {
                                        itemToUpd.ParentId = item.ParentId;
                                        Console.WriteLine(item.ParentId);
                                        itemToUpd.Index = item.Index;
                                        _dbContext.Menu.Update(itemToUpd).State = EntityState.Modified;
                                        Console.WriteLine("anything was to change");
                                    }
                                }
                            }
                        }
                        await _dbContext.SaveChangesAsync();
                        await dbTran.CommitAsync();
                    } catch
                    {
                        await dbTran.RollbackAsync();
                        Console.WriteLine("is rollback");
                    }
                }
            else
                return await Task.FromResult(false);
            //_dbContext.SaveChanges();
            return await Task.FromResult(true);
        }
        public IEnumerable<MenuModel>? ToPopulateChangedState(DraggableItemData next)
        {
            yield return next.MenuData;
            if (next.Childrens == null)
                yield break;

            var draggableItems = next.Childrens;

            foreach (var item in draggableItems)
            {
                var subDraggableItems = ToPopulateChangedState(item);
                if (subDraggableItems != null)
                    foreach (var subItem in subDraggableItems)
                    {
                        yield return subItem;
                    }
            }
        }
        private IEnumerable<MenuModel> PopulateWithCurrentState()
        {
            if (_dbContext != null)
            {
                List<Menu>? menuList = _dbContext.Menu?.ToList();
                if (menuList != null && menuList.Count > 0)
                {
                    foreach (var item in menuList)
                    {
                        if (_dragged == null || _dragged.MenuData?.Id != item.Id)
                        {
                            yield return new MenuModel
                            {
                                Id = item.Id,
                                DisplayName = item.DisplayName,
                                Index = item.Index,
                                PageId = item.PageId,
                                ParentId = item.ParentId
                            };
                        }
                    }
                }
            }
        }
        public async void OnDelete()
        {
            if(Container != null)
                if(Container.Childrens != null)
                    if(Container.Childrens.Exists(x => x.MenuData.Id == 3))
                    {
                        if(_dragged != null)
                        {
                            await _dragged.DeepRelease(Container.Childrens.First(x => x.MenuData.Id == 3));
                            if(_dbContext?.Menu != null)
                            {
                                try
                                {
                                    if(_dragged.Parent != null)
                                        _dragged.Parent -= _dragged;
                                    _dbContext.Menu.RemoveRange(await _dbContext.Menu.FirstAsync(x => x.Id == _dragged.MenuData.Id));
                                    _dragged = null;
                                }
                                catch { }
                            }
                            await SaveChangedState();
                            NotifyStateChanged();
                        }
                    }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
