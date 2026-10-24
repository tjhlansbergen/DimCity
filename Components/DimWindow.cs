using System.Numerics;
using Raylib_cs;


internal class DimWindow
{
    internal int Width { get; private set; }
    internal int Height { get; private set; }

    internal DimCity? City { get; private set; }

    internal Console console = new();

    internal DimMap? map;
    internal DimConsole consolePanel;
    internal DimControls controlsPanel;
    internal DimSelection selectionPanel;

    internal DimPopup? popup;

    internal static readonly int fontSize = 20;

    private static Font notoSansMono;
    private const int horizon = 150;

    private readonly Timer timer;

    internal DimWindow(bool fullscreen)
    {
        Raylib.InitWindow(1600, 900, "DimCity");
        if (fullscreen)
        {
            Raylib.ToggleFullscreen();
        }

        // this needs to happen after the window is initialized
        notoSansMono = Raylib.LoadFontEx("resources/NotoSansMono-Regular.ttf", fontSize, null, 0);

        Width = Raylib.GetScreenWidth();
        Height = Raylib.GetScreenHeight();

        var margin = 6;
        var bottomPanelSize = new Vector2(Width / 2 - margin - (margin / 2), horizon - margin);

        consolePanel = new DimConsole(new Rect
        {
            position = new Vector2(Width / 2 + margin / 2, Height - horizon),
            size = bottomPanelSize,
        }, console, notoSansMono);

        controlsPanel = new DimControls(new Rect
        {
            position = new Vector2(margin, Height - horizon),
            size = bottomPanelSize,
        }, console, notoSansMono, SelectionChanged);

        selectionPanel = new DimSelection(new Rect
        {
            position = new Vector2(margin, Height - horizon - 32 - margin),
            size = new Vector2(32, 32),
        }, console);

        timer = new Timer(TimerElapsed, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));

        console.Write(string.Empty);
        console.Write("Welcome to DimCity!");
        console.Write("Click and hold to pan around, scroll to zoom.");

        GetCityFromUser();
    }

    public void ShowPopup(string title, SortedSet<string> items, Action<string> callback)
    {
        var del = new Action<string>((result) =>
        {
            popup = null;
            callback.Invoke(result);
        });

        popup = new DimPopup(new Rect
        {
            position = new Vector2(Width / 2 - 200, Height / 2 - 100),
            size = new Vector2(400, 100 + items.Count * (notoSansMono.BaseSize + 6) + 12)
        }, console, notoSansMono, title, items, del);

    }

    private void GetCityFromUser()
    {
        ShowPopup("Select City",
        [
            "Amsterdim",
            "Bosten",
            "Chicano",
            "Dimver",
            "Elhambra",
            "San Francesca"
        ], (city) =>
        {
            LoadCity(city);
        });
    }

    private void LoadCity(string cityName)
    {
        console.Write($"Loading city {cityName}...", debug: true);

        var loadResult = Persistance.Load(cityName);
        City = loadResult.Item1;
        console.Write(loadResult.Item2);

        map = new DimMap(new Rect
        {
            position = new Vector2(0, 0),
            size = new Vector2(Width, Height),
        }, console, City);
    }

    internal void Draw()
    {
        while (!Raylib.WindowShouldClose())
        {
            DispatchInput();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Colors.Background);


            map?.Draw();
            controlsPanel.Draw();
            consolePanel.Draw();
            selectionPanel.Draw();

            popup?.Draw();

            Raylib.EndDrawing();
        }

        // shutdown
        console.Write("Goodbye!");
        TimerElapsed(null);  // save before exit
        timer.Dispose();

        Raylib.CloseWindow();
    }

    private void DispatchInput()
    {
        // zooming
        var scrollDelta = Raylib.GetMouseWheelMove();
        if (scrollDelta > 0) map?.ZoomIn();
        if (scrollDelta < 0) map?.ZoomOut();

        // single click
        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            if (popup != null && popup.Bounds.Contains(Raylib.GetMousePosition()))
            {
                popup.Click(Raylib.GetMousePosition());
                return;
            }

            map?.CommitBuild();

            if (Raylib.GetMouseY() > Height - horizon && Raylib.GetMouseX() < Width / 2)    // inside controls panel
            {
                controlsPanel?.Click(Raylib.GetMousePosition());
            }
            if (Raylib.GetMouseY() > Height - horizon && Raylib.GetMouseX() > Width / 2)    // inside console panel
            {
                consolePanel?.Click(Raylib.GetMousePosition());
            }
        }

        // click 'n drag
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (Raylib.GetMouseY() < Height - horizon)  // inside map
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    BeginMapBuildIfNeeded();
                }

                HandleMapInput();
            }
        }
    }

    private void BeginMapBuildIfNeeded()
    {
        var type = selectionPanel.SelectedBuildingType;
        if (type is BuildingTypes.BuildingType.Select or BuildingTypes.BuildingType.Bulldoze) return;

        map?.BeginBuild(type, Raylib.GetMousePosition());
    }

    private void HandleMapInput()
    {
        switch (selectionPanel.SelectedBuildingType)
        {
            case BuildingTypes.BuildingType.Select:
                map?.Offset += Raylib.GetMouseDelta();
                break;
            case BuildingTypes.BuildingType.Bulldoze:
                map?.Bulldoze(Raylib.GetMousePosition());
                break;
            default:
                map?.Build(selectionPanel.SelectedBuildingType, Raylib.GetMousePosition());
                break;
        }
    }

    private void SelectionChanged(BuildingTypes.BuildingType type)
    {
        // reset previous selection
        controlsPanel.Tabs.Where(tab => tab.Key != controlsPanel.Menu.SelectedLabel).ToList().ForEach(tab => tab.Value.SelectedIndex = null);

        // set new selection
        selectionPanel.SelectedBuildingType = type;

        console.Write($"Selected {type}");
    }

    private void TimerElapsed(object? _)
    {
        if (City == null) return;

        var p = Persistance.Store(City);
        console.Write($"Auto-saved to {p}", debug: true);
    }
}