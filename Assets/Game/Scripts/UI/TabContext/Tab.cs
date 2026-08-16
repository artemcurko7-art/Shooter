namespace Game.Scripts.UI.TabContext
{
    public  class Tab 
    {
        public Tab(TabView[] views)
        {
            Views = views;
        }
        
        public TabView[] Views { get; }
    }
}