using UnityEngine;
using System.IO;
using BepInEx;
using HarmonyLib;
using BepInEx;
using UnityEngine.Rendering;
using Object = System.Object;

namespace MainMenuEnhanced;

public static class SpriteExporter
{
    public static void Export(Sprite sprite, string fileName)
    {
        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);
        tex.SetPixels(pixels);
        tex.Apply();

       // byte[] bytes = UnityEngine.ImageConversion.EncodeToPNG(tex);
        string path = Path.Combine(Paths.PluginPath, fileName + ".png");
        // File.WriteAllBytes(path, bytes);
        UnityEngine.Object.Destroy(tex);
    }
}