namespace TeamOneProject.Interfaces
{
    public interface IMenu
    {
        public string MenuName { get; set; }
        public void Run();
        public List<IMenu> SubMenues { get; set; }
        public void ShowMenu();
        public void PreLoad();
        public void NavigateMenu();
    }
}
