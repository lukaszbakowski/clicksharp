using ClickSharp.DataLayer;
using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ClickSharp.DataLayer.Entities;
using Microsoft.Data.SqlClient;

namespace ClickSharp.Components
{
    public class MenuBase : ComponentBase //, IDisposable
    {
        protected static bool _LOCK = false;
        protected static bool _FIRED_METHOD = false;
        protected bool IsBlock = true;
        protected int? IsActive;
        protected static List<MenuModel> elementsToDrag = new List<MenuModel>();
        protected List<MenuModel> items = new List<MenuModel>();
        public static MenuModel? DragItem { get; set; }
        protected void OnDragEnter(int index)
        {
            //Console.WriteLine(index.ToString());
            IsActive = index;
            StateHasChanged();
        }
        protected void OnDragLeave()
        {
            IsActive = null;
            StateHasChanged();
        }
        protected async void OnDrop(int targetIndex)
        {
            _FIRED_METHOD = true;
            if (DragItem != null)
            {
                bool hasChanged = false;
                if (items.Exists(x => x.Id == DragItem.Id))
                {
                    if (targetIndex != DragItem.Index && targetIndex != DragItem.Index + 1)
                    {
                        if (DragItem.Index > targetIndex)
                        {
                            for (int i = DragItem.Index; i > targetIndex; i--)
                            {
                                items[i].Index = i - 1;
                                items[i - 1].Index = i;
                                items.Swap(i, i - 1);
                            }
                        }
                        else if (DragItem.Index < targetIndex)
                        {
                            targetIndex--;
                            for (int i = DragItem.Index; i < targetIndex; i++)
                            {
                                items[i].Index = i + 1;
                                items[i + 1].Index = i;
                                items.Swap(i, i + 1);
                            }
                        }
                        hasChanged = true;
                    }
                }
                else if (elementsToDrag.Exists(x => x.Id == DragItem.Id))
                {
                    foreach (var item in items)
                    {
                        if (item.Index >= targetIndex)
                        {
                            item.Index++;
                        }
                    }
                    elementsToDrag.Remove(DragItem);
                    DragItem.Index = targetIndex;
                    DragItem.ParentId = MenuData?.Id;

                    items.Add(DragItem);
                    hasChanged = true;
                }
                else if(MenuData != null)
                {
                    List<Menu>? dbMenuList = dbContext?.Menu?.FromSqlRaw(@"
                    IF @parentId=@childId
                    BEGIN
                        SELECT * FROM menu where id = @parentId
                    END
                    ELSE
                    BEGIN
                        ;WITH generation AS (
                            SELECT Id
                            ,[Index]
                            ,ParentId
                            ,PageId
                            ,DisplayName,
                                0 AS generation_number
                            FROM menu
                            WHERE ParentId = @parentId
 
                        UNION ALL
 
                            SELECT child.Id
                            ,child.[Index]
                            ,child.ParentId
                            ,child.PageId
                            ,child.DisplayName,
                                generation_number+1 AS generation_number
                            FROM menu child
                            JOIN generation g
                              ON g.id = child.ParentId
                        )
                        SELECT Id
                            ,[Index]
                            ,ParentId
                            ,PageId
                            ,DisplayName
                        FROM generation
                        WHERE Id = @childId
                    END",
                        new SqlParameter
                        {
                            ParameterName = "childId",
                            DbType = System.Data.DbType.Int32,
                            Value = MenuData.Id
                        }, new SqlParameter
                        {
                            ParameterName = "parentId",
                            DbType = System.Data.DbType.Int32,
                            Value = DragItem.Id
                        }
                    ).ToList();
                    if (dbMenuList != null && dbMenuList.Count == 0)
                    {
                        DragItem.ParentId = MenuData?.Id;
                        DragItem.Index = items.Count;
                        items.Add(DragItem);
                        if (DragItem.Index > targetIndex)
                        {
                            for (int i = DragItem.Index; i > targetIndex; i--)
                            {
                                items[i].Index = i - 1;
                                items[i - 1].Index = i;
                                items.Swap(i, i - 1);
                            }
                        }
                        //Console.WriteLine("parent: " + DragItem.Id.ToString());
                        //Console.WriteLine("child:" + MenuData.Id.ToString());
                        hasChanged = true;
                    }
                }
                if(hasChanged)
                {
                    foreach (var item in elementsToDrag)
                    {
                        Menu? dbMenuItem = dbContext?.Menu?.FirstOrDefault(x => x.Id == item.Id);
                        if (dbMenuItem != null)
                        {
                            dbMenuItem.Index = item.Index;
                        }
                    }
                    foreach (var item in items)
                    {
                        Menu? dbMenuItem = dbContext?.Menu?.FirstOrDefault(x => x.Id == item.Id);
                        if (dbMenuItem != null)
                        {
                            dbMenuItem.Index = item.Index;
                            dbMenuItem.ParentId = item.ParentId;
                        }
                    }
                    if(dbContext != null)
                        await dbContext.SaveChangesAsync();
                }
            }
            DragItem = null;
            IsActive = null;
            _LOCK = false;
            _FIRED_METHOD = false;
            //ReloadItems();
            StateHasChanged();
            await Task.CompletedTask;
        }

        protected virtual void ReloadItems() { }

        [Inject]
        protected ClickSharpContext dbContext { get; set; }
        [Inject]
        protected MenuState _state { get; set; }
        [Parameter]
        public MenuModel? MenuData { get; set; }
        [Parameter]
        public MenuModel? Item { get; set; }
        [Parameter]
        public EventCallback<MenuModel> ItemChanged { get; set; }
        protected async Task DragStarted()
        {
            _LOCK = true;
            Item = MenuData;
            //ParentOnDragStarted();
            //IsBlock = false;
            await ItemChanged.InvokeAsync(Item);
        }
        protected async Task ParentRefresh()
        {
            
            while(_LOCK)
            {
                if (_FIRED_METHOD)
                    await Task.Delay(300);
                else _LOCK = false;
            }
            if (dbContext.Menu != null && MenuData != null)
            {

                List<Menu>? menu = dbContext.Menu.Where(x => x.ParentId == MenuData.Id).OrderBy(x => x.Index).ToList();
                if (menu != null && menu.Count > 0)
                {
                    for (int i = 0; i < menu.Count; i++)
                    {
                        menu[i].Index = i;
                    }
                    await dbContext.SaveChangesAsync();

                    //Console.WriteLine("parentID: " + MenuData.Id);
                } else if (menu != null && menu.Count == 0)
                {
                    items.Clear();
                }
            }
            await _state.MenuStateHasChanged();

            if (MenuData == null)
                return;
            if (_state.Items.ContainsKey(MenuData.Id))
            {
                //Console.WriteLine("do i runnnnnnnnnnnnnnnnnnnn");
                items = _state.Items[MenuData.Id];
            }

            await Task.CompletedTask;
        }
        [Parameter]
        public EventCallback ExecuteParentRefreshFromChild { get; set; }

        protected async Task OnDragEnded()
        {
            //IsBlock = true;
            //await _state.MenuStateHasChanged();
            items.Clear();
            await ExecuteParentRefreshFromChild.InvokeAsync();
            //if(_state.Items.ContainsKey(MenuData.Id))
            //items = _state.Items[MenuData.Id];
            StateHasChanged();
        }
        //protected async Task ParentRefreshOnStart()
        //{
        //    if(DragItem != null)
        //    {
        //        items.Remove(DragItem);
        //        StateHasChanged();
        //    }
        //    //Console.WriteLine("testtetststststs");
        //    await Task.CompletedTask;
        //}

        //[Parameter]
        //public EventCallback ExecuteParentRemoveChild { get; set; }

        //protected async Task ParentOnDragStarted()
        //{
        //    await ExecuteParentRemoveChild.InvokeAsync();
        //}
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    _state.OnChange += StateHasChanged;
        //}
        //public void Dispose()
        //{
        //    _state.OnChange -= StateHasChanged;
        //}
    }
}
