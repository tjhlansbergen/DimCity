using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DimCity;

public interface ITextureService
{
    public void Load(ContentManager content);
    public Texture2D GetTile(string key);
    public Texture2D GetTile(int index);
    public int CountTiles();

}

public class TextureService : ITextureService
{
    private Game _game;
    private Dictionary<string, Texture2D> _tileTextures;

    public TextureService(Game game)
    {
        _game = game;
    }

    public void Load(ContentManager contentManager)
    {
        var tileFolder = "Tiles";
        _tileTextures = new DirectoryInfo(Path.Join(contentManager.RootDirectory, tileFolder))
                        .GetFiles("*.png")
                        .Select(file => Path.GetFileNameWithoutExtension(file.Name))
                        .ToDictionary(fileName => fileName, fileName => contentManager.Load<Texture2D>(Path.Join(tileFolder, fileName)));
    }

    public Texture2D GetTile(string key)
    {
        return _tileTextures[key];
    }

    public Texture2D GetTile(int index)
    {
        return _tileTextures.ElementAt(index).Value;
    }

    public int CountTiles()
    {
        return _tileTextures.Count();
    }
}