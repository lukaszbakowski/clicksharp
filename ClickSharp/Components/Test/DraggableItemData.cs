using ClickSharp.DataLayer;
using ClickSharp.Models.Data;
using ClickSharp.DataLayer;
using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ClickSharp.DataLayer.Entities;
using ClickSharp.Helpers;

namespace ClickSharp.Components.Test
{
    public class DraggableItemData
    {
        public List<DraggableItemData>? Childrens;
        public DraggableItemData? Parent;
        public MenuModel MenuData;
        public DraggableItemData(MenuModel menuData, DraggableItemData? parent = null)
        {
            MenuData = menuData;
            Parent = parent;
        }
        public DraggableItemData Init(List<MenuModel> menuList)
        {
            try
            {
                if (menuList != null && MenuData != null)
                {
                    List<MenuModel> childs = menuList.Where(x => x.ParentId == MenuData.Id).OrderBy(x => x.Index).ToList();

                    if (childs.Count > 0)
                    {
                        Childrens = new();
                        foreach (var child in childs)
                        {
                            var childToPopulate = new DraggableItemData(child, this).Init(menuList);
                            Childrens.Add(childToPopulate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"myError test: {ex.ToString()}");
            }

            return this;
        }
        public bool CheckIfDropIsPossible(int destinationId)
        {
            foreach(var id in GetAllIds().ToList())
            {
                if(id == destinationId)
                {
                    return false;
                }
            }
            return true;
        }
        public IEnumerable<int> GetAllIds()
        {
            yield return MenuData.Id;
            if(Childrens != null)
                foreach(var child in Childrens)
                {
                    foreach(var subChild in child.GetAllIds().ToList())
                    {
                        yield return subChild;
                    }
                }
        }
        public static DraggableItemData operator -(DraggableItemData left, DraggableItemData right)
        {
            try
            {
                if (left.Childrens != null)
                {

                    if (left.Childrens.Contains(right))
                    {
                        if(left.Childrens.Remove(right))
                        {
                            Console.WriteLine("removed success");
                        }
                            
                        left.Childrens = left.Childrens.OrderBy(x => x.MenuData.Index).ToList();
                        for (int i = 0; i < left.Childrens.Count; i++)
                        {
                            left.Childrens[i].MenuData.Index = i;
                        }
                    }
                }
            } catch
            {
                Console.WriteLine("error occured while -");
                throw;
            }
            
            return left;
        }
        public static DraggableItemData operator +(DraggableItemData left, DraggableItemData right)
        {
            try
            {
                if (left.Childrens != null)
                {
                    if (!left.Childrens.Contains(right))
                    {
                        left.Childrens.Add(right);
                    }
                        

                    int currentIndex = left.Childrens.IndexOf(right);

                    if (currentIndex > right.MenuData.Index)
                    {
                        for (int i = currentIndex; i > right.MenuData.Index; i--)
                        {
                            left.Childrens[i - 1].MenuData.Index = i;
                            left.Childrens.Swap(i, i - 1);
                        }
                    }
                }
                else
                {
                    left.Childrens = new()
                    {
                        right
                    };
                }
            } catch
            {
                throw;
            }
            
            return left;
        }
        public bool DeepReplace(DraggableItemData toReplace)
        {
            if(Childrens != null)
            {
                var toRemove = Childrens.FirstOrDefault(x => x.MenuData.Id == toReplace.MenuData.Id);
                if (toRemove != null)
                {
                    Childrens.Remove(toRemove);
                    Childrens.Add(toReplace);
                    return true;
                } else
                {
                    foreach(var child in Childrens)
                    {
                        var result = child.DeepReplace(toReplace);
                        if (result == true)
                            return result;
                    }
                }
            }
            
            return false;
        }
        public async Task DeepRelease(DraggableItemData newParent)
        {
            Parent = newParent;
            if(Parent.Childrens != null)
                if(!Parent.Childrens.Contains(this))
                    Parent.Childrens?.Add(this);
            
            MenuData.ParentId = newParent.MenuData.Id;
            MenuData.Index = 0;


            if(Childrens != null)
            {
                foreach (var child in Childrens)
                {
                    await child.DeepRelease(newParent);
                }
                Childrens.Clear();
            }
            
            await Task.CompletedTask;
        }
    }
}
