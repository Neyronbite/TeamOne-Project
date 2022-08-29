using TeamOneProject.Interfaces;
using TeamOneProject.UI;

namespace TeamOneProject.Classes.Menues
{
    public class MenuAbstraction : IMenu
    {
        public List<IMenu> SubMenues { get; set; }
        public string MenuName { get; set; }

        protected MenuUI menuUI { get; set; }

        public MenuAbstraction(string name)
        {
            MenuName = name;
            SubMenues = new List<IMenu>();
            menuUI = MenuUI.Instance;
        }

        public virtual void Run() { PreLoad(); ShowMenu(); NavigateMenu(); }

        public virtual void ShowMenu()
        {
            menuUI.PrintMenuName(MenuName);
        }
        public virtual void PreLoad() { }

        public virtual void NavigateMenu()
        {
            var list = SubMenues?.Select(s => s.MenuName).ToList();

            if (list?.Count == 0)
            {
                return;
            }

            list.Add("Exit");
            var id = menuUI.MenueNavigation(list);

            if (id == SubMenues.Count)
            {
                return;
            }

            SubMenues[id].Run();
            Run();
        }
    }
}
