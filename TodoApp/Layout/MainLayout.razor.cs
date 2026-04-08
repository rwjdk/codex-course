namespace TodoApp.Layout;

public partial class MainLayout
{
    private bool DrawerOpen { get; set; } = true;

    private void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }
}