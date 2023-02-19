using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace ClickSharp.Components
{
    public class MenuBase : ComponentBase
    {
        protected int? IsActive;
        protected static List<MenuModel> elementsToDrag = new List<MenuModel>();
        protected List<MenuModel> items = new List<MenuModel>();
        protected MenuModel? _item { get; set; }
        protected void OnDragEnter(int index)
        {
            Console.WriteLine(index.ToString());
            IsActive = index;
            StateHasChanged();
        }
        protected void OnDragLeave()
        {
            IsActive = null;
            StateHasChanged();
        }
        protected void OnDrop(int targetIndex)
        {
            if (_item != null)
            {
                if (items.Exists(x => x.Id == _item.Id))
                {
                    if (targetIndex != _item.Index && targetIndex != _item.Index + 1)
                    {
                        if (_item.Index > targetIndex)
                        {
                            for (int i = _item.Index; i > targetIndex; i--)
                            {
                                items[i].Index = i - 1;
                                items[i - 1].Index = i;
                                items.Swap(i, i - 1);
                            }
                        }
                        else if (_item.Index < targetIndex)
                        {
                            targetIndex--;
                            for (int i = _item.Index; i < targetIndex; i++)
                            {
                                items[i].Index = i + 1;
                                items[i + 1].Index = i;
                                items.Swap(i, i + 1);
                            }
                        }
                        //items.FirstOrDefault(x => x.Id == item.Id).Index = targetIndex;

                    }
                }
                else if (elementsToDrag.Exists(x => x.Id == _item.Id))
                {
                    elementsToDrag.Remove(_item);
                    _item.Index = targetIndex;
                    items.Insert(targetIndex, _item);
                }
            }
            _item = null;
            IsActive = null;
            StateHasChanged();
        }

        [Parameter]
        public MenuModel? MenuData { get; set; }
        [Parameter]
        public MenuModel? Item { get; set; }
        [Parameter]
        public EventCallback<MenuModel> ItemChanged { get; set; }
        protected Task DragStarted()
        {
            Item = MenuData;
            return ItemChanged.InvokeAsync(Item);
        }
    }
}
