using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MainMenuEnhanced.Assets;

// this assetloader code was made by Gemini since I was too lazy
public static class AssetLoader
{
    private static string ImagePath;

    public static Sprite LoadExternalSprite()
    {
        string[] extensions = { ".png", ".jpg", ".jpeg" };
        
        foreach (string ext in extensions)
        {
            string tempPath = Path.Combine(CustomPaths.ModFolder, "CustomBG" + ext);
            if (File.Exists(tempPath))
            {
                ImagePath = tempPath;
                break;
            }
        }
        
        if (ImagePath.IsNullOrWhiteSpace())
        {
            MainMenuEnhancedPlugin.LogSource.LogWarning($"External image not found. Using default background.");
            return null;
        }

        try
        {
            byte[] fileData = File.ReadAllBytes(ImagePath);
            
            Texture2D texture = new Texture2D(2, 2);
            
            if (ImageConversion.LoadImage(texture, fileData))
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        catch (Exception e)
        {
            MainMenuEnhancedPlugin.LogSource.LogError($"Failed to load external sprite: {e.Message}");
        }

        return null;
    }
    public static GameObject LoadAsset(string bundleName, string assetName)
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        string? resourceName = asm.GetManifestResourceNames()
            .FirstOrDefault(name => name.Contains(bundleName));

        if (resourceName == null)
        {
            MainMenuEnhancedPlugin.LogSource.LogError($"Could not find {bundleName} in DLL");
            return null;
        }

        using (Stream s = asm.GetManifestResourceStream(resourceName))
        {
            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, buffer.Length);

            AssetBundle bundle = AssetBundle.LoadFromMemory(buffer);

            if (bundle == null)
            {
                MainMenuEnhancedPlugin.LogSource.LogError("Failed to load bundle from memory");
                return null;
            }

            var asset = bundle.LoadAsset(assetName, Il2CppInterop.Runtime.Il2CppType.Of<GameObject>());
            GameObject prefab = asset.Cast<GameObject>();
            bundle.Unload(false);
            return prefab;
        }
    }
}
